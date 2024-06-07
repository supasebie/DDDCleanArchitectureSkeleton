using FastEndpoints;
using FluentValidation;

namespace InPrompts.API.Prompts;

/// <summary>
/// See: https://fast-endpoints.com/docs/validation
/// </summary>
public class GetPromptValidator : Validator<GetPromptByIdRequest>
{
  public GetPromptValidator()
  {
    RuleFor(x => x.PromptId)
      .GreaterThan(0);
  }
}
