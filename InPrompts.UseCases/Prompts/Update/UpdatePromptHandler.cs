using Ardalis.SharedKernel;
using InPrompts.Core;
using Ardalis.Result;

namespace InPrompts.UseCases;

public class UpdatePromptHandler : ICommandHandler<UpdatePromptCommand, Result<PromptDTO>>
{
    private readonly IRepository<Prompt> _repository;

    public UpdatePromptHandler(IRepository<Prompt> repository)
    {
        _repository = repository;
    }

    public async Task<Result<PromptDTO>> Handle(UpdatePromptCommand request, CancellationToken cancellationToken)
    {
        var existingPrompt = await _repository.GetByIdAsync(request.PromptId, cancellationToken);
        if (existingPrompt == null)
        {
            return Result.NotFound();
        }

        existingPrompt.UpdateText(request.Text);

        await _repository.UpdateAsync(existingPrompt, cancellationToken);

        return Result.Success(new PromptDTO(existingPrompt.Id, existingPrompt.Text!));
    }

}
