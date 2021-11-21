using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading;
using Serilog;

namespace Calabonga.Microservice.TemplateBuilder
{
    public class ReplaceService
    {
        public void Replace(ConcurrentBag<FileData> results, TemplateOptions templateOptions, CancellationToken token)
        {
            foreach (var option in templateOptions.PatternDefinitions)
            {
                foreach (var item in results.Where(x => x.FileExtension.Equals(option.FileExtension)))
                {
                    Log.Logger.Verbose($"BEFORE REPLACE {Path.GetFileName(item.FileName)}");
                    Log.Logger.Verbose(item.Content);
                    if (string.IsNullOrEmpty(item.Replaced))
                    {
                        var replaced = item.Content.Replace(option.SearchTextForReplace, option.ReplaceTextWith);
                        item.Replaced = replaced;
                    }
                    else
                    {
                        var replaced = item.Replaced.Replace(option.SearchTextForReplace, option.ReplaceTextWith);
                        item.Replaced = replaced;
                    }
                    Log.Logger.Verbose($"AFTER REPLACE {Path.GetFileName(item.FileName)}");
                    Log.Logger.Verbose(item.Replaced);
                }
            }
        }
    }
}
