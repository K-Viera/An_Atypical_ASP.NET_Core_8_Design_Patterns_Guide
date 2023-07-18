using System.Reflection;

public class Person : IBindableFromHttpContext<Person>
{
    public string? Name { get; set; }
    public DateOnly Birthday { get; set; }

    public static ValueTask<Person?> BindAsync(
        HttpContext context,
        ParameterInfo parameter)
    {
        var name = context.Request.Query["name"].Single();

        var birthdayIsValid = DateOnly.TryParse(
            context.Request.Query["birthday"],
            out var birthday
        );

        if (name is not null && birthdayIsValid)
        {
            var person = new Person()
            {
                Name = name,
                Birthday = birthday
            };

            return ValueTask.FromResult(person)!;
        }

        return ValueTask.FromResult(default(Person));
    }
}