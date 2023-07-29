using Resources.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonTest
{
    public class AmbientContextTest
    {
        [Fact]
        public void should_echo_the_inputted_text_to_the_console()
        {
            // Arrange (make the console write to a StringBuilder 
            // instead of the actual console) 
            var expectedText = "Hello World!" + Environment.NewLine;

            var sb = new StringBuilder();
            using (var writer = new StringWriter(sb))
            {
                Console.SetOut(writer);
                //Act
                AmbientContext.Current.WriteSomething("Hello World!");
            }

            //Assert
            var actualText = sb.ToString();
            Assert.Equal(expectedText, actualText);
        }
    }
}
