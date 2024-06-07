using Ardalis.SharedKernel;

namespace InPrompts.Core;

/// <summary>
/// A domain event that is dispatched whenever a prompt is deleted.
/// The DeletePromptService is used to dispatch this event.
/// </summary>
internal class PromptDeletedEvent : DomainEventBase
{
    public int PromptId { get; set; }

    public PromptDeletedEvent(int promptId)
    {
        PromptId = promptId;
    }
}
