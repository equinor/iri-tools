/*
Copyright 2022 Equinor ASA

This program is free software: you can redistribute it and/or modify it under the terms of version 3 of the GNU Lesser General Public License as published by the Free Software Foundation.

This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with this program. If not, see <https://www.gnu.org/licenses/>.
 */

using System.Text.Json.Serialization;

namespace IriTools;

/// <summary>
/// Useful for other IRIs because a fragment is not part of a URI, and URI.Equals ignores the fragment.
/// A URI Reference includes the fragment
/// </summary>
[Serializable]
[JsonConverter(typeof(IriReferenceConverter))]
public class IriReference : IEquatable<IriReference>, IComparable<IriReference>, IComparable
{
    public Uri uri { get; set; }

    public static implicit operator IriReference(Uri uri) => new(uri);
    public static implicit operator IriReference(string uri) => new(uri);
    public static implicit operator Uri(IriReference r) => r.uri;

    /// <summary>
    /// Escapes the segments as data segments and appends them to the base IRI
    /// </summary>
    /// <param name="baseIri">The base part of the IRI, f.ex. https://example.com/</param>
    /// <param name="segments">Any number of segments, f.ex. a(1), b and c</param>
    /// <returns>The escaped combined IRI, f.ex. https://example.com/a%281%29/b/c</returns>
    public static IriReference FromDataSegments(IriReference baseIri, params string[] segments)
    {
        var escapedSegments = segments.Select(v => Uri.EscapeDataString(v.Trim())).ToArray();
        var path = string.Join("/", escapedSegments);
        return new Uri(baseIri.uri, path);
    }

    bool IEquatable<IriReference>.Equals(IriReference? other) =>
        other != null && (ReferenceEquals(this, other) || ToString().Equals(other.ToString()));

    public int CompareTo(IriReference? other)
        =>
            other switch
            {
                null => 1,
                _ => string.Compare(ToString(), other.ToString(), StringComparison.Ordinal)
            };

    public int CompareTo(object? obj)
    =>
        obj switch
        {
            null => 1,
            IriReference iri => string.Compare(ToString(), iri.ToString(), StringComparison.Ordinal),
            _ => throw new ArgumentException("Object is not an IriReference")
        };


    public override bool Equals(object? other) =>
        other != null && (ReferenceEquals(this, other) || (other is IriReference iri && ToString().Equals(iri.ToString())));

    public override string ToString() => uri.ToString();




    /// <summary>
    /// Cannot use Uri.getHashCode since that ignores the fragment
    /// </summary>
    public override int GetHashCode() => ToString().GetHashCode();

    [JsonConstructor]
    public IriReference(Uri uri)
    {
        this.uri = uri;
    }

    public IriReference(string uriString)
    {
        uri = new Uri(uriString);
    }
}
