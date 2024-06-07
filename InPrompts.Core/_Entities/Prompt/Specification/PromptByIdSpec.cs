using Ardalis.Specification;

namespace InPrompts.Core;

public class PromptByIdSpec : Specification<Prompt>
{
    public PromptByIdSpec(int PromptId)
    {
        Query
            .Where(Prompt => Prompt.Id == PromptId);
    }
}