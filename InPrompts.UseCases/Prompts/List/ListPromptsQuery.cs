using Ardalis.Result;
using Ardalis.SharedKernel;

namespace InPrompts.UseCases;

public record ListPromptsQuery(int? Skip, int? Take) : IQuery<Result<IEnumerable<PromptDTO>>>;