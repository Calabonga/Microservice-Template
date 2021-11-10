using System.Collections.Generic;

namespace Calabonga.Microservice.TemplateBuilder;

public class MicroserviceTemplates
{
    public List<TemplateOptions> TemplateOptions { get; set; } = null!;
}

public class TemplateOptions
{
    public List<PatternDefinition> PatternDefinitions { get; set; } = new();

    public string RootDirectoryPath { get; set; } = null!;

    public string ProjectName { get; set; } = null!;
}