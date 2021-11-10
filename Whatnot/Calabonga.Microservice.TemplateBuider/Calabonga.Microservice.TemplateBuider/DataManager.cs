using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Calabonga.Microservice.TemplateBuilder
{
    public class DataManager
    {
        private readonly SemaphoreSlim _gate = new(1);
        private readonly TemplateOptions _templateOptions;
        private readonly FileService _fileService;
        private readonly ReadWriteService _readWriteService;
        private readonly ReplaceService _replaceService;
        private readonly ConcurrentBag<FileData> _results = new();

        public DataManager(
            TemplateOptions templateOptions,
            FileService fileService,
            ReadWriteService readWriteService,
            ReplaceService replaceService)
        {
            _templateOptions = templateOptions;
            _fileService = fileService;
            _readWriteService = readWriteService;
            _replaceService = replaceService;
        }

        public async Task BuildAsync()
        {
            var items = LoadItemsFromRootPath();
            Console.WriteLine($"Total {items.Count} items found");

            var cancellationTokenSource = new CancellationTokenSource();
            var tasks = CreateTasks(items, cancellationTokenSource.Token).ToArray();
            await Task.WhenAll(tasks);
            _replaceService.Replace(_results, _templateOptions, cancellationTokenSource.Token);
            Console.WriteLine($"Processing done ({_results.Sum(x=>x.Length)} bytes)");

            foreach (var result in _results)
            {
                await _readWriteService.SaveDataAsync(result.FileName, result.Replaced, cancellationTokenSource.Token);
            }
        }

        private IEnumerable<Task> CreateTasks(List<string> items, CancellationToken cancellationToken = default)
        {
            return items.Select(item => LoadFileDataAsync(item, cancellationToken));
        }

        private async Task LoadFileDataAsync(string fileNameWithPath, CancellationToken cancellationToken = default)
        {
            try
            {
                await _gate.WaitAsync(cancellationToken);
                var data = await _readWriteService.LoadDataAsync(fileNameWithPath, cancellationToken);
                _results.Add(new FileData
                {
                    FileName = fileNameWithPath,
                    FileExtension = Path.GetExtension(fileNameWithPath),
                    Content = data,
                    Length = data.Length
                });
                
                _gate.Release();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private List<string> LoadItemsFromRootPath()
        {
            var items = _fileService.ProcessFolder(_templateOptions.RootDirectoryPath, _templateOptions);
            if (items.Length == 0)
            {
                Console.WriteLine("Nothing to replace");
                return new();
            }

            return items.ToList();
        }
    }

    public class FileData
    {
        public string Content { get; set; } = null!;

        public string FileName { get; set; } = null!;

        public string FileExtension { get; set; } = null!;

        public int Length { get; set; }

        public string Replaced { get; set; } = null!;
    }
}
