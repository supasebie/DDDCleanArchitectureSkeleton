namespace InPrompts.API.Prompts;
public class UpdatePromptResponse
{
  public UpdatePromptResponse(PromptRecord prompt)
  {
    Prompt = prompt;
  }
  public PromptRecord Prompt { get; set; }
}
