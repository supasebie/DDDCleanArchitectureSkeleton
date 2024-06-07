using System.Collections.Immutable;
using System.Linq;
using FastEndpoints;
using InPrompts.UseCases;
using MediatR;

namespace InPrompts.API.Prompts;

/// <summary>
/// Lists popular prompts by view count.
/// </summary>
/// <remarks>
/// Lists all prompts that have over int X amount of views
/// </remarks>
public class ListPopularPrompts : Endpoint<ListPopularPromptsRequest, ListPopularPromptsResponse>
{
    private readonly IMediator _mediator;

    public ListPopularPrompts(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get(ListPopularPromptsRequest.Route);
        AllowAnonymous();
    }

    public override async Task HandleAsync(ListPopularPromptsRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ListPopularPromptsQuery(request.Count));

        if (result.IsSuccess)
        {
            Response = new ListPopularPromptsResponse(result.Value.Select(p => new PromptRecord(p.Id, p.Text, p.Views)).ToList());
        }
    }
}