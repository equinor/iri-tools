using IriTools;
using FluentAssertions;

namespace TestIriReference;

public class TestIriReference
{
    [Fact]
    public void Iris__ShouldTakeAnchorIntoAccount()
    {
        var IriRef1 = new IriReference("https://example.com/id#1");
        var IriRef2 = new IriReference("https://example.com/id#2");
        IriRef1.Should().NotBeEquivalentTo(IriRef2);
        IriRef1.uri.Equals(IriRef2.uri).Should().BeTrue();
    }
}