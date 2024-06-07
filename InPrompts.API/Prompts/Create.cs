using FastEndpoints;
using InPrompts.UseCases;
using MediatR;

namespace InPrompts.API.Prompts;

/// <summary>
/// Create a new Prompt
/// </summary>
/// <remarks>
/// Creates a new Prompt given some text.
/// </remarks>
public class Create : Endpoint<CreatePromptRequest, CreatePromptResponse>
{
    private readonly IMediator _mediator;

    public Create(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post(CreatePromptRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            // XML Docs are used by default but are overridden by these properties:
            //s.Summary = "Create a new Prompt.";
            //s.Description = "Create a new Prompt. Valid text is required.";
            s.ExampleRequest = new CreatePromptRequest { Text = "Prompt Text" };
        });
    }

    public override async Task HandleAsync(
      CreatePromptRequest request,
      CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreatePromptCommand(request.Text!));

        if (result.IsSuccess)
        {
            Response = new CreatePromptResponse(result.Value, request.Text!);
            return;
        }
        // TODO: Handle other cases as necessary
    }
}