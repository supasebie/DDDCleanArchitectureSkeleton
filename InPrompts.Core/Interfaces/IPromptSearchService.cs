using Ardalis.Result;

namespace InPrompts.Core;

public interface IPromptSearchService
{
    Task<Result<Prompt>> GetMostPopularPromptsAsync(int count);
    Task<Result<List<Prompt>>> GetPromptsWithThisManyViewsAsync(int count);
}