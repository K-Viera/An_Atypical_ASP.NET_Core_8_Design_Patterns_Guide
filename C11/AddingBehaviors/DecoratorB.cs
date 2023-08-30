namespace AddingBehaviors
{
    public class DecoratorB : IComponent
    {
        private readonly IComponent _component;
        public DecoratorB(IComponent component)
        {
            _component = component;
        }
        public string Operation()
        {
            var result = _component.Operation();
            return $"<DecoratorB>{result}</DecoratorB>";
        }
    }
}
