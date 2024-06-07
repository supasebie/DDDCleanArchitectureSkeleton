using InPrompts.API.Prompts;

namespace InPrompts.API;

public class ListPopularPromptsResponse
{
    public ListPopularPromptsResponse(List<PromptRecord> popularPrompts)
    {
        PopularPrompts = popularPrompts;
    }

    public List<PromptRecord> PopularPrompts { get; set; }
}


