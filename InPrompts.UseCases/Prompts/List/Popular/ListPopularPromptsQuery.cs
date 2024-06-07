using Ardalis.Result;
using Ardalis.SharedKernel;

namespace InPrompts.UseCases;

public record ListPopularPromptsQuery(int count) : IQuery<Result<IEnumerable<PromptDTO>>>
{
    public int Count { get; set; }
}