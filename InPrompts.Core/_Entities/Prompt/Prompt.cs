using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace InPrompts.Core;

public class Prompt(string text) : EntityBase, IAggregateRoot
{
    // Example of validating primary constructor inputs
    // See: https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/primary-constructors#initialize-base-class
    public int AppUserId { get; set; }
    public int? InReplyToPromptId { get; set; }
    public int? InReplyToAppUserId { get; set; }
    public int? RePromptCount { get; set; }
    public int? Views { get; set; }
    public int? FavoriteCount { get; set; }
    public string? Language { get; set; } = default!;
    public string? Country { get; set; } = default!;
    public string? Text { get; set; } = Guard.Against.NullOrEmpty(text, nameof(text));
    public DateTime? CreatedAt { get; set; }
    public string? ImageUrl { get; set; } = default!;

    public void UpdateText(string newText)
    {
        Text = Guard.Against.NullOrEmpty(newText, nameof(newText));
    }
}

public class PhoneNumber : ValueObject
{
    public string CountryCode { get; private set; } = string.Empty;
    public string Number { get; private set; } = string.Empty;
    public string? Extension { get; private set; } = string.Empty;

    public PhoneNumber(string countryCode,
      string number,
      string? extension)
    {
        CountryCode = countryCode;
        Number = number;
        Extension = extension;
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return CountryCode;
        yield return Number;
        yield return Extension ?? String.Empty;
    }
}