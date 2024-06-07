using Ardalis.Result;
using Ardalis.SharedKernel;

namespace InPrompts.UseCases;

public record GetPromptQuery(int PromptId) : IQuery<Result<PromptDTO>>;
