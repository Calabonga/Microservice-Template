using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Calabonga.Microservice.TemplateBuilder
{
    public class FileService
    {
        private readonly List<string> _files;
        public FileService()
        {

            _files = new List<string>();
        }

        public string[] ProcessFolder(string path, TemplateOptions options)
        {
            foreach (var pattern in options.PatternDefinitions.DistinctBy(x => x.FileExtension))
            {
                var files = Directory.GetFiles(path, $"*{pattern.FileExtension}", SearchOption.AllDirectories);
                if (files.Any())
                {
                    _files.AddRange(files);
                }
            }

            return _files.ToArray();
        }
    }
}
