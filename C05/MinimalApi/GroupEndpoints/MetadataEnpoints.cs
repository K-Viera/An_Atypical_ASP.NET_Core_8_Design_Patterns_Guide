using Microsoft.OpenApi.Any;
public static class metadataEndpoints
{
    public static void MapMetadataEndpoints(this IEndpointRouteBuilder app)
    {
        #region Metadata

        var metadataGroup = app
            .MapGroup("minimal-endpoint-metadata")
            .WithTags("Metadata Endpoints")
            .WithOpenApi()
        ;


        const string NamedEndpointName = "Named Endpoint";
        metadataGroup
            .MapGet("with-name", () => $"Endpoint with name '{NamedEndpointName}'.")
            .WithName(NamedEndpointName)
            .WithOpenApi(operation =>
            {
                operation.Description = "An endpoint that returns its name.";
                operation.Summary = $"Endpoint named '{NamedEndpointName}'.";
                operation.Deprecated = true;
                return operation;
            });

        metadataGroup
            .MapGet("url-of-named-endpoint/{endpointName?}", (string? endpointName, LinkGenerator linker) =>
            {
                var name = endpointName ?? NamedEndpointName;
                return new
                {
                    name,
                    uri = linker.GetPathByName(name)
                };
            })
            .WithDescription("Return the URL of the specified named endpoint.")
            .WithOpenApi(operation =>
            {
                var endpointName = operation.Parameters[0];
                endpointName.Description = "The name of the endpoint to get the URL for.";
                endpointName.AllowEmptyValue = true;
                endpointName.Example = new OpenApiString(NamedEndpointName);
                return operation;
            });

        metadataGroup
            .MapGet("excluded-from-open-api", () => { })
            .ExcludeFromDescription()
        ;
        #endregion
    }
}