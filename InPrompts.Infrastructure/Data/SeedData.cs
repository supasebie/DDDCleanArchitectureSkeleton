using InPrompts.Core;
using Microsoft.EntityFrameworkCore;

namespace InPrompts.Infrastructure;

public static class SeedData
{
    public static readonly Prompt Prompt1 = new("Sebastian");
    public static readonly Prompt Prompt2 = new("Amelia");
    public static readonly Prompt Prompt3 = new("Soren");
    public static readonly Prompt Prompt4 = new("Evangaline");
    public static readonly Prompt Prompt5 = new("William");

    public static async Task InitializeAsync(AppDbContext dbContext)
    {
        if (await dbContext.Prompts.AnyAsync()) return; // DB has been seeded

        await PopulateTestDataAsync(dbContext);
    }

    public static async Task PopulateTestDataAsync(AppDbContext dbContext)
    {
        dbContext.Prompts.AddRange([Prompt1, Prompt2]);
        await dbContext.SaveChangesAsync();
    }
}
