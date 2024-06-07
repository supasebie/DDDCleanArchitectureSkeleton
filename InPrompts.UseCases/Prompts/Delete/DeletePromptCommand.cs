using Ardalis.Result;
using Ardalis.SharedKernel;

namespace InPrompts.UseCases;

public record DeletePromptCommand(int PromptId) : ICommand<Result>;
