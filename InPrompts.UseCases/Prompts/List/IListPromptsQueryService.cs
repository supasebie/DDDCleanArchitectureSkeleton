namespace InPrompts.UseCases;

/// <summary>
/// Represents a service that will actually fetch the necessary data
/// Typically implemented in Infrastructure
/// </summary>
public interface IListPromptsQueryService
{
    Task<IEnumerable<PromptDTO>> ListAsync();
}