namespace InPrompts.API.Prompts;


public class CreatePromptResponse
{
    public CreatePromptResponse(int id, string text)
    {
        Id = id;
        Text = text;
    }
    public int Id { get; set; }
    public string Text { get; set; }
}
