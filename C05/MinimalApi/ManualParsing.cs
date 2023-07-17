using System.Diagnostics.CodeAnalysis;


public class Coordinate : IParsable<Coordinate>
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public static Coordinate Parse(string value, IFormatProvider? provider)
    {
        if (TryParse(value, provider, out var result))
        {
            return result;
        }
        throw new ArgumentException("Cannot parse the value into a Coordinate.", nameof(value));
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Coordinate result)
    {
        var segments = s?.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        if (segments?.Length == 2)
        {
            var latitudeIsValid = double.TryParse(segments[0], out var latitude);
            var longitudeIsValid = double.TryParse(segments[1], out var longitude);

            if (latitudeIsValid && longitudeIsValid)
            {
                result = new()
                {
                    Latitude = latitude,
                    Longitude = longitude
                };

                return true;
            }
        }
        result = null;
        return false;
    }
}