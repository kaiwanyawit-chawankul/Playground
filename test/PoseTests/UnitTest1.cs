using Xunit;
using System;
using Pose;
using FluentAssertions;

namespace PoseTests;

public class App
{
    public DateTime GetNow()
    {
        return DateTime.Now;
    }
}

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        Shim dateTimeShim = Shim.Replace(() => DateTime.Now).With(() => new DateTime(2004, 4, 4));

        var result = DateTime.MinValue;
        PoseContext.Isolate(
            () =>
            {
                var sut = new App();
                result = sut.GetNow();

                //result.Should().Be(new DateTime(2004, 4, 4));
            },
            dateTimeShim
        );
        Assert.Equal(new DateTime(2004, 4, 4), result);
    }
}
