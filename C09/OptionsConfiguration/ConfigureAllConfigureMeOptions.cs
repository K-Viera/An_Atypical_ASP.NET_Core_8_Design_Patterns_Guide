using Microsoft.Extensions.Options;

namespace OptionsConfiguration;

public class ConfigureAllConfigureMeOptions : IConfigureNamedOptions<ConfigureMeOptions>,IPostConfigureOptions<ConfigureMeOptions>
{
    public void Configure(string? name, ConfigureMeOptions options)
    {
        options.Lines = options.Lines.Append($"ConfigureAll:Configure name: {name}");
        if (name != Options.DefaultName)
        {
            options.Lines = options.Lines.Append($"ConfigureAll:Configure Not Default: {name}");
        }
    }

    public void Configure(ConfigureMeOptions options) => Configure(Options.DefaultName, options);

    public void PostConfigure(string? name, ConfigureMeOptions options) 
    {
        options.Lines = options.Lines.Append($"ConfigureAll:PostConfigure name: {name}");
    }
}
