using Microsoft.AspNetCore.Mvc;

namespace InPrompts.API;

public class ListPopularPromptsRequest
{
    public const string Route = "/Prompts/Popular/{Count}";
    public static string BuildRoute(int Count) => Route.Replace("{Count:int}", Count.ToString());

    [FromRoute]
    public int Count { get; set; }

    //[FromQuery]
    //public string? Count { get; set; }

}
