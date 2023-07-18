using Microsoft.AspNetCore.Http.HttpResults;
public static class OutputEndpoints
{
    public static void MapOutputEndpoints(this IEndpointRouteBuilder app)
    {
        #region Outputs

        app.MapGet(
            "minimal-endpoint-output-coordinate/",
            () => new Coordinate
            {
                Latitude = 43.653225,
                Longitude = -79.383186
            }
        );

        app.MapGet("minimal-endpoint-output-coordinate-ok1/", () =>
            Results.Ok(new Coordinate
            {
                Latitude = 43.653225,
                Longitude = -79.383186
            })
        );

        app.MapGet("minimal-endpoint-output-coordinate-ok2/", () =>
            TypedResults.Ok(new Coordinate
            {
                Latitude = 43.653225,
                Longitude = -79.383186
            })
        );

        app.MapGet(
            "multiple-TypedResults/",
            Results<Ok, Conflict> ()
                => Random.Shared.Next(0, 100) % 2 == 0
                    ? TypedResults.Ok()
                    : TypedResults.Conflict()
        );

        app.MapGet(
            "multiple-TypedResults-delegate/{number}",
            (int number) => MultipleResultsDelegate(number)
        );

        Results<Ok, Conflict> MultipleResultsDelegate(int number)
        {
            return number % 2 == 0
                ? TypedResults.Ok()
                : TypedResults.Conflict();
        }

        #endregion

    }
}