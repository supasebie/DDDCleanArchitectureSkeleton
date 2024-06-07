using Ardalis.SharedKernel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InPrompts.Core;

/// <summary>
/// NOTE: Internal because PromptDeleted is also marked as internal.
/// </summary>
internal class PromptDeletedHandler : INotificationHandler<PromptDeletedEvent>
{
  private readonly IRepository<Prompt> _repository;
  private readonly ILogger<PromptDeletedHandler> _logger;

  public PromptDeletedHandler(IRepository<Prompt> repository,
    ILogger<PromptDeletedHandler> logger)
  {
    _repository = repository;
    _logger = logger;
  }

  public async Task Handle(PromptDeletedEvent domainEvent, CancellationToken cancellationToken)
  {
    _logger.LogInformation("Removing deleted Prompt {PromptId} from DB...", domainEvent.PromptId);
    // Perform eventual consistency removal of Prompts from projects when one is deleted

    var promptSpec = new PromptByIdSpec(domainEvent.PromptId);
    var prompts = await _repository.ListAsync(promptSpec, cancellationToken);

    foreach (var prompt in prompts)
    {
      await _repository.DeleteAsync(prompt, cancellationToken);
    }
  }
}
