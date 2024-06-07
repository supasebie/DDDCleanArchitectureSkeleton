using InPrompts.UseCases;

namespace InPrompts.Infrastructure;

public class FakeListPromptsQueryService : IListPromptsQueryService
{
    public Task<IEnumerable<PromptDTO>> ListAsync()
    {
        var result = new List<PromptDTO>() { new PromptDTO(1, "Amelia"), new PromptDTO(2, "Evangaline") };
        return Task.FromResult(result.AsEnumerable());
    }
}