using System.Collections.Concurrent;
using System.Linq;
using System.Threading;

namespace Calabonga.Microservice.TemplateBuilder
{
    public class ReplaceService
    {
        public void Replace(
            ConcurrentBag<FileData> results,
            TemplateOptions templateOptions,
            CancellationToken token)
        {
            foreach (var option in templateOptions.PatternDefinitions)
            {
                foreach (var item in results.Where(x=>x.FileExtension.Equals(option.FileExtension)))
                {
                    var replaced = item.Content.Replace(option.SearchTextForReplace, option.ReplaceTextWith);
                    item.Replaced = replaced;
                }
            }
        }
    }
}
