using Ardalis.Specification;

namespace InPrompts.Core;

public class PromptByViewsSpec : Specification<Prompt>
{
    public PromptByViewsSpec(int Count)
    {
        Query
            .Where(Prompt => Prompt.Views > Count);
    }
}