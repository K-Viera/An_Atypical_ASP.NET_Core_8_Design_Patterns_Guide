namespace Greeter;
public interface IGreeter
{
    string Greeting();
}

public class ExternalGreeter
{
    public string GreetByName(string name)
    {
        return $"Adaptee says: hi {name}!";
    }
}