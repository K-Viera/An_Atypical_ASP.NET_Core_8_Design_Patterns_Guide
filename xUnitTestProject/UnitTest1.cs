namespace xUnitTestProject;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var expectedValue = 2;

        var actualValue = 2;

        Assert.Equal(expectedValue, actualValue);
    }
}