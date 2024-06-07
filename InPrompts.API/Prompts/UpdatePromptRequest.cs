using System.ComponentModel.DataAnnotations;

namespace InPrompts.API.Prompts;

public class UpdatePromptRequest
{
  public const string Route = "/Prompts/{PromptId:int}";
  public static string BuildRoute(int PromptId) => Route.Replace("{PromptId:int}", PromptId.ToString());

  public int PromptId { get; set; }

  [Required]
  public int Id { get; set; }
  [Required]
  public string? Text { get; set; }
}
