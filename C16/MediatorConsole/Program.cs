// See https://aka.ms/new-console-template for more information
using Mediator;
using MediatorConsole;

Console.WriteLine("Hello, World!");

var (millerWriter, miller) = Helper.CreateConcreteColleague("Miller");
var (orazioWriter, orazio) = Helper.CreateConcreteColleague("Orazio");
var (fletcherWriter, fletcher) = Helper.CreateConcreteColleague("Fletcher");
var mediator = new ConcreteMediator(miller, orazio, fletcher);

mediator.Send(new Message(
    Sender: miller,
    Content: "Hey everyone!"
));
mediator.Send(new Message(
    Sender: orazio,
    Content: "What's up Miller?"
));
mediator.Send(new Message(
    Sender: fletcher,
    Content: "Hey Miller!"
));

Console.WriteLine(millerWriter.Output.ToString());
Console.WriteLine(orazioWriter.Output.ToString());
Console.WriteLine(fletcherWriter.Output.ToString());