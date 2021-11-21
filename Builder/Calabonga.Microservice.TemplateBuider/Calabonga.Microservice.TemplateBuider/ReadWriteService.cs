using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Calabonga.Microservice.TemplateBuilder
{
    public class ReadWriteService
    {
        public Task<string> LoadDataAsync(string fileNameWithPath, CancellationToken cancellationToken = default)
        {
            try
            {
                return File.ReadAllTextAsync(fileNameWithPath, Encoding.UTF8, cancellationToken);

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return Task.FromResult(string.Empty);
            }
        }

        public Task SaveDataAsync(string fileNameWithPath, string? content, CancellationToken cancellationToken = default)
        {
            try
            {
                return File.WriteAllTextAsync(fileNameWithPath, content, Encoding.UTF8, cancellationToken);

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return Task.FromResult(string.Empty);
            }
        }
    }
}
