using Microsoft.AspNetCore.Mvc;
public static class InputEndpoints
{

    public static void MapInputsEndpoints(this IEndpointRouteBuilder app)
    {

        #region Inputs

        app.MapGet("minimal-endpoint-inline", () => "GET!");

        app.MapGet(
            "minimal-endpoint-input-route-implicit/{id}",
            (int id) => $"The id was {id}."
        );

        app.MapGet(
            "minimal-endpoint-input-route-explicit/{id}",
            ([FromRoute] int id) => $"The id was {id}."
        );

        app.MapGet(
           "minimal-endpoint-input-HttpContext/",
           (HttpContext context)
               => context.Response.WriteAsync("HttpContext!")
        );

        app.MapGet(
           "minimal-endpoint-input-HttpResponse/",
           (HttpResponse response)
               => response.WriteAsync("HttpResponse!")
        );

        app.MapGet(
           "minimal-endpoint-input-Coordinate/",
           (Coordinate coordinate) => coordinate
        );

        app.MapGet(
            "minimal-endpoint-input-Person/",
            (Person person) => person
        );
        app.MapGet(
            "minimal-endpoint-input-Person2/",
            ([AsParameters] Person2 person) => person
        );

        #endregion
    }
}