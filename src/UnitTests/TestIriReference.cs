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
        iriRef1.Should().NotBe(iriRef2);
        iriRef1.uri.Equals(iriRef2.uri).Should().BeTrue();
    }

    [Fact]
    public void IriReference__Implements__IComparableTyped()
    {
        IComparable<IriReference> iriRef1 = new IriReference("https://example.com/id#1");
        var iriRef2 = new IriReference("https://example.com/id#2");
        iriRef1.CompareTo(iriRef2).Should().Be(-1);
    }

    [Fact]
    public void IriReference__Implements__IComparableObject()
    {
        IComparable iriRef1 = new IriReference("https://example.com/id#1");
        object iriRef2 = new IriReference("https://example.com/id#2");
        iriRef1.CompareTo(iriRef2).Should().Be(-1);
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

    [Fact]
    public void Should_Deserialize_UriString_To_IriReference()
    {
        // Arrange
        var expectedIriReference = new IriReference("https://example.com/");

        // Act
        var iriRef = JsonSerializer.Deserialize<IriReference>("\"https://example.com/\"");

        // Assert
        iriRef.Should().NotBeNull();
        iriRef.Should().Be(expectedIriReference);
    }

    [Fact]
    public void Segments__Are__Escaped()
    {
        // Arrange
        var baseIri = new IriReference("https://example.com/");

        // Act
        var newIri = IriReference.FromDataSegments(baseIri, "a(1)", "b", "c");

        // Assert
        newIri.Should().NotBeNull();
        newIri.ToString().Should().Be("https://example.com/a%281%29/b/c");
    }
}