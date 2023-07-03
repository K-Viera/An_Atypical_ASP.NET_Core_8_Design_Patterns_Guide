using LiskovSubstitution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C03Test
{
    public class LiskovSubstitutionTest
    {
        public static TheoryData<SuperClass> InstancesThatThrowsSuperExceptions = new TheoryData<SuperClass>()
    {
        new SuperClass(),
        new SubClassOk(),
        new SubClassBreak(),
    };

        [Theory]
        [MemberData(nameof(InstancesThatThrowsSuperExceptions))]
        public void Test_method_name(SuperClass sut)
        {
            try
            {
                sut.Do();
            }
            catch (SuperException ex)
            {
                // Some code 
            }
        }

        [Theory]
        [MemberData(nameof(InstancesThatThrowsSuperExceptions))]
        public void Test_method_nameS2(SuperClass sut)
        {
            var value = 5;
            var result = sut.IsValid(value);
            Console.WriteLine($"Do something with {result}");
        }

        [Theory]
        [MemberData(nameof(InstancesThatThrowsSuperExceptions))]
        public void Test_method_nameS3(SuperClass sut)
        {
            var value = 5;
            var result = sut.Do(value);
            Console.WriteLine($"Do something with {result.Value}");
        }

        // Other classes, like SuperClass, SubClassOk,
        // and SubClassBreak
    }
}
