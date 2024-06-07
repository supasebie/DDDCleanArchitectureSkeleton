using Ardalis.Result;
using Ardalis.SharedKernel;
using InPrompts.Core;

namespace InPrompts.UseCases;

public class DeletePromptHandler : ICommandHandler<DeletePromptCommand, Result>
{
    private readonly IDeletePromptService _deletePromptService;

    public DeletePromptHandler(IDeletePromptService deletePromptService)
    {
        _deletePromptService = deletePromptService;
    }

    public async Task<Result> Handle(DeletePromptCommand request, CancellationToken cancellationToken)
    {
        // This Approach: Keep Domain Events in the Domain Model / Core project; this becomes a pass-through
        return await _deletePromptService.DeletePrompt(request.PromptId);

        // Another Approach: Do the real work here including dispatching domain events - change the event from internal to public
        // Ardalis prefers using the service so that "domain event" behavior remains in the domain model / core project
        // var aggregateToDelete = await _repository.GetByIdAsync(request.PromptId);
        // if (aggregateToDelete == null) return Result.NotFound();

        // await _repository.DeleteAsync(aggregateToDelete);
        // var domainEvent = new PromptDeletedEvent(request.PromptId);
        // await _mediator.Publish(domainEvent);
        // return Result.Success();
    }
}

