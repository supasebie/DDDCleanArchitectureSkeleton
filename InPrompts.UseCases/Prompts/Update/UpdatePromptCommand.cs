using Ardalis.Result;
using Ardalis.SharedKernel;

namespace InPrompts.UseCases;

public record UpdatePromptCommand(int PromptId, string Text) : ICommand<Result<PromptDTO>>;
