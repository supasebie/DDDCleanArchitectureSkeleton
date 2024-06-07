using FastEndpoints;
using FluentValidation;

namespace InPrompts.API.Prompts;

/// <summary>
/// See: https://fast-endpoints.com/docs/validation
/// </summary>
public class DeletePromptValidator : Validator<DeletePromptRequest>
{
  public DeletePromptValidator()
  {
    RuleFor(x => x.PromptId)
      .GreaterThan(0);
  }
}
