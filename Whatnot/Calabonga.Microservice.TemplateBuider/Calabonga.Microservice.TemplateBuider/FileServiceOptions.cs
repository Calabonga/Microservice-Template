namespace Calabonga.Microservice.TemplateBuilder
{
    public class PatternDefinition
    {
        public string FileExtension { get; set; } = null!;

        public string ContentType { get; set; } = null!;

        public string SearchTextForReplace { get; set; } = null!;

        public string ReplaceTextWith { get; set; } = null!;
    }
}
