namespace Greeter
{
    public class ExternalGreeterAdapter : IGreeter
    {
        private readonly ExternalGreeter _adaptee;
        public ExternalGreeterAdapter(ExternalGreeter adaptee)
        {
            _adaptee = adaptee;
        }
    }
}
