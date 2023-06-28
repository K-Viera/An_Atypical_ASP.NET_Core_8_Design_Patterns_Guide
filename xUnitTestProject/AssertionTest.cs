using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xUnitTestProject
{
    public class AssertionTest
    {
        [Fact]
        public void Exploring_xUnit_assertions()
        {
            Assert.Equal(expected: 2, actual: 2);
        }
        [Fact]
        public void Exploring_xUnit_assertions_NotEqual()
        {
            Assert.NotEqual(expected: 2, actual: 1);
        }
        [Fact]
        public void Exploring_xUnit_assertions_Same()
        {
            object obj1 = new MyClass { Name = "Object 1" };
            object obj3 = obj1;
            Assert.Same(obj1, obj3);

        }
        [Fact]
        public void Exploring_xUnit_assertions_NotSame()
        {
            object obj1 = new MyClass { Name = "Object 1" };
            object obj2 = new MyClass { Name = "Object 1" };
            object obj3 = obj1;
            Assert.NotSame(obj2, obj3);
        }
        [Fact]
        public void Exploring_xUnit_assertions_Equal()
        {
            object obj1 = new MyClass { Name = "Object 1" };
            object obj2 = new MyClass { Name = "Object 1" };
            Assert.Equal(obj1, obj2);
        }
        [Fact]
        public void Exploring_xUnit_assertions_Null()
        {
            object? obj4 = default(MyClass);
            Assert.Null(obj4);
        }
        [Fact]
        public void Exploring_xUnit_assertions_NotNull()
        {
            object obj1 = new MyClass { Name = "Object 1" };
            object obj3 = obj1;
            Assert.NotNull(obj3);
        }
        [Fact]
        public void Exploring_xUnit_assertions_IsType()
        {
            object obj1 = new MyClass { Name = "Object 1" };
            var instanceOfMyClass = Assert.IsType<MyClass>(obj1);
            Assert.Equal(expected: "Object 1", actual: instanceOfMyClass.Name);
        }
        [Fact]
        public void Exploring_xUnit_assertions_Exceptions()
        {
            var exception = Assert.Throws<SomeCustomException>(
                testCode: () => OperationThatThrows("Toto"));

            Assert.Equal(expected: "Toto", actual: exception.Name);

            static void OperationThatThrows(string name)
            {
                throw new SomeCustomException { Name = name };
            }
        }
        private record class MyClass
        {
            public string? Name { get; set; }
        }

        private class SomeCustomException : Exception
        {
            public string? Name { get; set; }
        }
    }

}
