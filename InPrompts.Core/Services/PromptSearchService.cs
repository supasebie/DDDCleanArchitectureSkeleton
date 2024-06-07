using Ardalis.Result;
using Ardalis.SharedKernel;

namespace InPrompts.Core;

public class PromptSearchService : IPromptSearchService
{
    private readonly IRepository<Prompt> _repository;

    public PromptSearchService(IRepository<Prompt> repository)
    {
        _repository = repository;
    }

    public Task<Result<Prompt>> GetMostPopularPromptsAsync(int count)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<List<Prompt>>> GetPromptsWithThisManyViewsAsync(int count)
    {
        if (count <= 0)
        {
            var errors = new List<ValidationError>
          {
            new() { Identifier = nameof(count), ErrorMessage = $"{nameof(count)} is required." }
          };

            return Result<List<Prompt>>.Invalid(errors);
        }

        var PromptSpec = new PromptByViewsSpec(count);
        var Prompts = await _repository.ListAsync(PromptSpec);

        // TODO: Optionally use Ardalis.GuardClauses Guard.Against.NotFound and catch
        if (Prompts == null)
        {
            return Result<List<Prompt>>.NotFound();
        }

        return new Result<List<Prompt>>(Prompts);
    }

    // public async Task<Result<List<Prompt>>> GetAllIncompleteItemsAsync(int PromptId, string searchString)
    // {
    //     if (string.IsNullOrEmpty(searchString))
    //     {
    //         var errors = new List<ValidationError>
    //   {
    //     new() { Identifier = nameof(searchString), ErrorMessage = $"{nameof(searchString)} is required." }
    //   };

    //         return Result<List<Prompt>>.Invalid(errors);
    //     }

    //     var PromptSpec = new PromptByIdWithItemsSpec(PromptId);
    //     var Prompt = await _repository.FirstOrDefaultAsync(PromptSpec);

    //     // TODO: Optionally use Ardalis.GuardClauses Guard.Against.NotFound and catch
    //     if (Prompt == null)
    //     {
    //         return Result<List<ToDoItem>>.NotFound();
    //     }

    //     var incompleteSpec = new IncompleteItemsSearchSpec(searchString);
    //     try
    //     {
    //         var items = incompleteSpec.Evaluate(Prompt.Items).ToList();

    //         return new Result<List<ToDoItem>>(items);
    //     }
    //     catch (Exception ex)
    //     {
    //         // TODO: Log details here
    //         return Result<List<ToDoItem>>.Error(new[] { ex.Message });
    //     }
    // }

    // public async Task<Result<ToDoItem>> GetNextIncompleteItemAsync(int PromptId)
    // {
    //     var PromptSpec = new PromptByIdWithItemsSpec(PromptId);
    //     var Prompt = await _repository.FirstOrDefaultAsync(PromptSpec);
    //     if (Prompt == null)
    //     {
    //         return Result<ToDoItem>.NotFound();
    //     }

    //     var incompleteSpec = new IncompleteItemsSpec();
    //     var items = incompleteSpec.Evaluate(Prompt.Items).ToList();
    //     if (!items.Any())
    //     {
    //         return Result<ToDoItem>.NotFound();
    //     }

    //     return new Result<ToDoItem>(items.First());
    // }
}
