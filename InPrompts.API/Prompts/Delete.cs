using FastEndpoints;
using Ardalis.Result;
using MediatR;
using InPrompts.UseCases;

namespace InPrompts.API.Prompts;

/// <summary>
/// Delete a Prompt.
/// </summary>
/// <remarks>
/// Delete a Prompt by providing a valid integer id.
/// </remarks>
public class Delete : Endpoint<DeletePromptRequest>
{
  private readonly IMediator _mediator;

  public Delete(IMediator mediator)
  {
    _mediator = mediator;
  }

  public override void Configure()
  {
    Delete(DeletePromptRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(
    DeletePromptRequest request,
    CancellationToken cancellationToken)
  {
    var command = new DeletePromptCommand(request.PromptId);

    var result = await _mediator.Send(command);

    if (result.Status == ResultStatus.NotFound)
    {
      await SendNotFoundAsync(cancellationToken);
      return;
    }

    if (result.IsSuccess)
    {
      await SendNoContentAsync(cancellationToken);
    };
    // TODO: Handle other issues as needed
  }
}
