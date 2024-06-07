using System.ComponentModel.DataAnnotations;

namespace InPrompts.API.Prompts;

public class CreatePromptRequest
{
    public const string Route = "/Prompts";

    [Required]
    public string? Text { get; set; }
}