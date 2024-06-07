
using Ardalis.Result;
using Ardalis.SharedKernel;
using InPrompts.Core;

namespace InPrompts.UseCases;

public class CreatePromptHandler : ICommandHandler<CreatePromptCommand, Result<int>>
{
    private readonly IRepository<Prompt> _repository;

    public CreatePromptHandler(IRepository<Prompt> repository)
    {
        _repository = repository;
    }

    public async Task<Result<int>> Handle(CreatePromptCommand request,
      CancellationToken cancellationToken)
    {
        var newPrompt = new Prompt(request.Text);
        var createdItem = await _repository.AddAsync(newPrompt, cancellationToken);

        return createdItem.Id;
    }
}