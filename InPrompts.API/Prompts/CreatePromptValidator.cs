using FastEndpoints;
using FluentValidation;
using InPrompts.Infrastructure;

namespace InPrompts.API.Prompts;


/// <summary>
/// See: https://fast-endpoints.com/docs/validation
/// </summary>
public class CreatePromptValidator : Validator<CreatePromptRequest>
{
    public CreatePromptValidator()
    {
        RuleFor(x => x.Text)
          .NotEmpty()
          .WithMessage("Text is required.")
          .MinimumLength(2)
          .MaximumLength(DataSchemaConstants.DEFAULT_TEXT_LENGTH);
    }
}