using Ardalis.Result;
using Ardalis.SharedKernel;
using InPrompts.Core;
using MediatR;

namespace InPrompts.UseCases;

public class ListPopularPromptsHandler : IQueryHandler<ListPopularPromptsQuery, Result<IEnumerable<PromptDTO>>>
{
    private readonly IPromptSearchService _promptSearchService;

    public ListPopularPromptsHandler(IPromptSearchService promptSearchService)
    {
        _promptSearchService = promptSearchService;
    }

    public async Task<Result<IEnumerable<PromptDTO>>> Handle(ListPopularPromptsQuery request, CancellationToken cancellationToken)
    {
        // This Approach: Keep Domain Events in the Domain Model / Core project; this becomes a pass-through
        // return await _promptSearchService.GetPromptsWithThisManyViewsAsync(request.Count).ToResult();
        var prompts = await _promptSearchService.GetPromptsWithThisManyViewsAsync(request.count);
        Console.WriteLine(request.count);
        if (prompts == null) return Result.NotFound();
        var results = prompts.Value.Select(p => new PromptDTO(p.Id, p.Text!));
        return Result.Success(results);

        // Another Approach: Do the real work here including dispatching domain events - change the event from internal to public
        // Ardalis prefers using the service so that "domain event" behavior remains in the domain model / core project
        // var aggregateToDelete = await _repository.GetByIdAsync(request.ContributorId);
        // if (aggregateToDelete == null) return Result.NotFound();

        // await _repository.DeleteAsync(aggregateToDelete);
        // var domainEvent = new ContributorDeletedEvent(request.ContributorId);
        // await _mediator.Publish(domainEvent);
        // return Result.Success();
    }

}


