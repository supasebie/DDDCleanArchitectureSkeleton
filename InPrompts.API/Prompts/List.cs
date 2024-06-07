using FastEndpoints;
using InPrompts.UseCases;
using MediatR;

namespace InPrompts.API.Prompts;

/// <summary>
/// List all Prompts
/// </summary>
/// <remarks>
/// List all Prompts - returns a PromptListResponse containing the Prompts.
/// NOTE: In DEV always returns a FAKE set of 2 Prompts
/// </remarks>
public class List : EndpointWithoutRequest<ListPromptResponse>
{
  private readonly IMediator _mediator;

  public List(IMediator mediator)
  {
    _mediator = mediator;
  }

  public override void Configure()
  {
    Get("/Prompts");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
    var result = await _mediator.Send(new ListPromptsQuery(null, null));

    if (result.IsSuccess)
    {
      Response = new ListPromptResponse
      {
        Prompts = result.Value.Select(c => new PromptRecord(c.Id, c.Text, c.Views)).ToList()
      };
    }
  }
}
