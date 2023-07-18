using System.Text.Json;
using System.Text.Json.Serialization;
public static class MapJsonSerializationEnpoints
{
    public static void MapJsonSerializationEndpoints(
        this IEndpointRouteBuilder app)
    {
        #region JSON Serialization


        var jsonGroup = app
            .MapGroup("JSON-serialization")
            .WithTags("JSON Serialization Endpoints")
            .WithOpenApi()
        ;

        jsonGroup.MapGet(
            "kebab-person/",
            () => new
            {
                FirstName = "John",
                LastName = "Doe"
            }
        );

        var kebabSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            PropertyNamingPolicy = JsonNamingPolicy.KebabCaseLower
        };

        jsonGroup.MapGet("kebab-person2/", () => TypedResults.Json(new
        {
            FirstName = "John",
            LastName = "Doe"
        }, kebabSerializerOptions));

        var enumSerializer = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        enumSerializer.Converters.Add(new JsonStringEnumConverter());

        jsonGroup.MapGet(
            "enum-as-string/",
            () => TypedResults.Json(new
            {
                FirstName = "John",
                LastName = "Doe",
                Rating = Rating.Good,
            }, enumSerializer)
        );

        #endregion

    }
}

public enum Rating
{
    Bad = 0,
    Ok,
    Good,
    Amazing
}