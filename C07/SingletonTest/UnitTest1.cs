using Singleton;

namespace SingletonTest
{
    public class MySingletonTest
    {
        [Fact]
        public void Create_should_always_return_the_same_instance()
        {
            var first = MySingleton.Create();
            var second = MySingleton.Create();
            Assert.Same(first, second);
        }

        [Fact]
        public void Create_should_always_return_the_same_instance_Simple()
        {
            var first = MySimpleSingleton.Instance;
            var second = MySimpleSingleton.Instance;
            Assert.Same(first, second);
        }
    }
}