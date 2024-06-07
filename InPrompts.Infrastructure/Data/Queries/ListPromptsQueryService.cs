using InPrompts.UseCases;
using Microsoft.EntityFrameworkCore;

namespace InPrompts.Infrastructure;

public class ListPromptsQueryService : IListPromptsQueryService
{
    // You can use EF, Dapper, SqlClient, etc. for queries
    private readonly AppDbContext _db;

    public ListPromptsQueryService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<PromptDTO>> ListAsync()
    {
        var result = await _db.Prompts.FromSqlRaw("SELECT Id, Text FROM Prompts") // don't fetch other big columns
          .Select(c => new PromptDTO(c.Id, c.Text!, c.Views ?? 0))
          .ToListAsync();

        // var prompt = new PromptDTO(1, "Test");
        // var result = new List<PromptDTO> { prompt };

        return await Task.FromResult(result);
    }
}