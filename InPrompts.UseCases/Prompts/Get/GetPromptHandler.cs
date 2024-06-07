using Ardalis.Result;
using Ardalis.SharedKernel;
using InPrompts.Core;

namespace InPrompts.UseCases;

/// <summary>
/// Queries don't necessarily need to use repository methods, but they can if it's convenient
/// </summary>
public class GetPromptHandler : IQueryHandler<GetPromptQuery, Result<PromptDTO>>
{
    private readonly IReadRepository<Prompt> _repository;

    public GetPromptHandler(IReadRepository<Prompt> repository)
    {
        _repository = repository;
    }

    public async Task<Result<PromptDTO>> Handle(GetPromptQuery request, CancellationToken cancellationToken)
    {
        var spec = new PromptByIdSpec(request.PromptId);
        var entity = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
        if (entity == null) return Result.NotFound();

        return new PromptDTO(entity.Id, entity.Text!);
    }
}
