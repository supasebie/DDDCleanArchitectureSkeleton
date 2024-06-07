namespace InPrompts.API.Prompts;

public class GetPromptByIdRequest
{
  public const string Route = "/Prompts/{PromptId:int}";
  public static string BuildRoute(int PromptId) => Route.Replace("{PromptId:int}", PromptId.ToString());

  public int PromptId { get; set; }
}
