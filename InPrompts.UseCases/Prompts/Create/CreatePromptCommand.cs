using Ardalis.Result;
using Ardalis.SharedKernel;

namespace InPrompts.UseCases;

/// <summary>
/// Create a new Prompt.
/// </summary>
/// <param name="Text"></param>
public record CreatePromptCommand(string Text) : ICommand<Result<int>>;