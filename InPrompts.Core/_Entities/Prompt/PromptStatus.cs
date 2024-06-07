using Ardalis.SmartEnum;

namespace InPrompts.Core;

public class PromptStatus : SmartEnum<PromptStatus>
{
    public static readonly PromptStatus FrontPage = new(nameof(FrontPage), 1);
    public static readonly PromptStatus NotSet = new(nameof(NotSet), 2);

    protected PromptStatus(string name, int value) : base(name, value) { }
}