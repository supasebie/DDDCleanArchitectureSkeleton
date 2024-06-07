using FastEndpoints;
using FluentValidation;
using InPrompts.Infrastructure;

namespace InPrompts.API.Prompts;

/// <summary>
/// See: https://fast-endpoints.com/docs/validation
/// </summary>
public class UpdatePromptValidator : Validator<UpdatePromptRequest>
{
  public UpdatePromptValidator()
  {
    RuleFor(x => x.Text)
      .NotEmpty()
      .WithMessage("Name is required.")
      .MinimumLength(2)
      .MaximumLength(DataSchemaConstants.DEFAULT_TEXT_LENGTH);
    RuleFor(x => x.PromptId)
      .Must((args, PromptId) => args.Id == PromptId)
      .WithMessage("Route and body Ids must match; cannot update Id of an existing resource.");
  }
}
