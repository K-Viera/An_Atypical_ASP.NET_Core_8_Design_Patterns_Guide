namespace AddingBehaviors
{
    public class DecoratorA : IComponent
    {
        private readonly IComponent _component;
        public DecoratorA(IComponent component)
        {
            _component = component;
        }
        public string Operation()
        {
            var result = _component.Operation();
            return $"<DecoratorA>{result}</DecoratorA>";
        }
    }
}
