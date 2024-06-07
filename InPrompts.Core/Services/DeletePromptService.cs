using Microsoft.Extensions.Logging;
using Ardalis.SharedKernel;
using Ardalis.Result;
using MediatR;

namespace InPrompts.Core;

public class DeletePromptService : IDeletePromptService
{
  private readonly IRepository<Prompt> _repository;
  private readonly IMediator _mediator;
  private readonly ILogger<DeletePromptService> _logger;

  public DeletePromptService(IRepository<Prompt> repository,
    IMediator mediator,
    ILogger<DeletePromptService> logger)
  {
    _repository = repository;
    _mediator = mediator;
    _logger = logger;
  }

  public async Task<Result> DeletePrompt(int PromptId)
  {
    _logger.LogInformation("Deleting Prompt {PromptId}", PromptId);
    var aggregateToDelete = await _repository.GetByIdAsync(PromptId);
    if (aggregateToDelete == null) return Result.NotFound();

    await _repository.DeleteAsync(aggregateToDelete);
    var domainEvent = new PromptDeletedEvent(PromptId);
    await _mediator.Publish(domainEvent);
    return Result.Success();
  }
}