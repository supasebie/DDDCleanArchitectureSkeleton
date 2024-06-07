using Ardalis.Result;
using FastEndpoints;
using InPrompts.UseCases;
using MediatR;

namespace InPrompts.API.Prompts;

/// <summary>
/// Get a Prompt by integer ID.
/// </summary>
/// <remarks>
/// Takes a positive integer ID and returns a matching Prompt record.
/// </remarks>
public class GetById : Endpoint<GetPromptByIdRequest, PromptRecord>
{
  private readonly IMediator _mediator;

  public GetById(IMediator mediator)
  {
    _mediator = mediator;
  }

  public override void Configure()
  {
    Get(GetPromptByIdRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(GetPromptByIdRequest request,
    CancellationToken cancellationToken)
  {
    var command = new GetPromptQuery(request.PromptId);

    var result = await _mediator.Send(command);

    if (result.Status == ResultStatus.NotFound)
    {
      await SendNotFoundAsync(cancellationToken);
      return;
    }

    if (result.IsSuccess)
    {
      Response = new PromptRecord(result.Value.Id, result.Value.Text, result.Value.Views);
    }
  }
}
