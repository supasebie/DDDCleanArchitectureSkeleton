using Ardalis.Result;
using Ardalis.SharedKernel;

namespace InPrompts.UseCases;

public class ListPromptsHandler : IQueryHandler<ListPromptsQuery, Result<IEnumerable<PromptDTO>>>
{
    private readonly IListPromptsQueryService _query;

    public ListPromptsHandler(IListPromptsQueryService query)
    {
        _query = query;
    }

    public async Task<Result<IEnumerable<PromptDTO>>> Handle(ListPromptsQuery request, CancellationToken cancellationToken)
    {
        var result = await _query.ListAsync();

        return Result.Success(result);
    }
}