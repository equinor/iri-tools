using IriTools;
using FluentAssertions;

namespace TestIriReference;

public class TestIriReference
{
    [Fact]
    public void Iris__ShouldTakeAnchorIntoAccount()
    {
        var iriRef1 = new IriReference("https://example.com/id#1");
        var iriRef2 = new IriReference("https://example.com/id#2");
        iriRef1.Should().NotBe(iriRef2);
        iriRef1.uri.Equals(iriRef2.uri).Should().BeTrue();
    }
    
    [Fact]
    public void Iris__ShouldBeEqual()
    {
        var iriRef1 = new IriReference("https://example.com/id");
        var iriRef2 = new IriReference("https://example.com/id");
        iriRef1.Should().Be(iriRef2);
    }
    
    [Fact]
    public void Iris__Should__ImplementIComparable()
    {
        IComparable<IriReference> iriRef1 = new IriReference("https://example.com/id");
        var iriRef2 = new IriReference("https://example.com/id");
        iriRef1.CompareTo(iriRef2).Should().Be(0);
    }
}