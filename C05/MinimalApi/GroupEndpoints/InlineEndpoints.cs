public static class InlineGroupEndpoints
{
    public static IEndpointRouteBuilder MapInlineGroupEndpoints(
     this IEndpointRouteBuilder app)
    {
        #region inline

        var inlineGroup = app
            .MapGroup("inline-filter")
            .WithTags("Leveraging endpoint filters")
            .WithOpenApi()
        ;

        inlineGroup
            .MapGet("basic", () => { })
            .AddEndpointFilter((context, next) =>
            {
                return next(context);
            });

        inlineGroup.MapGet("good-rating/{rating}", (Rating rating) =>
                TypedResults.Ok(new { Rating = rating }))
            .AddEndpointFilter(async (context, next) =>
            {
                var rating = context.GetArgument<Rating>(0);

                if (rating == Rating.Bad)
                {
                    return TypedResults.Problem(
                        detail: "This endpoint is biased and only accepts positive ratings.",
                        statusCode: StatusCodes.Status400BadRequest
                    );
                }

                return await next(context);
            });

        //we can also extract this filter logic into a separate class 
        //and use it in multiple endpoints or groups


        app.MapGet("good-rating/{rating}", (Rating rating)
            => TypedResults.Ok(new { Rating = rating }))
            .AddEndpointFilter<GoodRatingFilter>()
        ;

        var filterGroup = app
            .MapGroup("filter group")
            .WithTags("Filter Group Example")
            .WithOpenApi()
            .AddEndpointFilter<GoodRatingFilter>();
        ;

        filterGroup
        .MapGet("good-rating/{rating}", (Rating rating)
            => TypedResults.Ok(new { Rating = rating }))
        ;

        filterGroup
        .MapGet("good-rating2/{rating}&{review}", (Rating rating, string review)
            => TypedResults.Ok(new { Rating = rating, Review = review }))
        ;

        inlineGroup.MapGet("endpoint-filter-factory", () => "RAW")
            .AddEndpointFilterFactory((filterFactoryContext, next) =>
            {
                // Building RequestDelegate code here.
                var logger = filterFactoryContext.ApplicationServices
                    .GetRequiredService<ILoggerFactory>()
                    .CreateLogger("endpoint-filter-factory");

                logger.LogInformation("Code that runs when ASP.NET Core builds the RequestDelegate");

                // Returns the EndpointFilterDelegate ASP.NET Core executes as part of the pipeline.
                return async invocationContext =>
                {
                    logger.LogInformation("Code that ASP.NET Core executes as part of the pipeline");
                    // Filter code here
                    return await next(invocationContext);
                };
            });

            return app;
        #endregion
    }
    public class GoodRatingFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(
            EndpointFilterInvocationContext context,
            EndpointFilterDelegate next)
        {
            var rating = context.GetArgument<Rating>(0);

            if (rating == Rating.Bad)
            {
                return TypedResults.Problem(
                    detail: "This endpoint is biased and only accepts positive ratings.",
                    statusCode: StatusCodes.Status400BadRequest
                );
            }

            return await next(context);
        }
    }
}