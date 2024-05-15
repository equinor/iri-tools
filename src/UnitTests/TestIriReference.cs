using System.Text.Json;
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
        iriRef1.Should().NotBeEquivalentTo(iriRef2);
        iriRef1.uri.Equals(iriRef2.uri).Should().BeTrue();
    }


    [Fact]
    public void Should_Deserialize_Json_To_IriReference()
    {
        // Arrange
        var expectedIriReference = new IriReference("https://example.com/");
        var iriJson = JsonSerializer.Serialize(expectedIriReference);

        // Act
        var iriRef = JsonSerializer.Deserialize<IriReference>(iriJson);

        // Assert
        iriRef.Should().NotBeNull();
        iriRef.Should().Be(expectedIriReference);
    }
}