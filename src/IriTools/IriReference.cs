/*
Copyright 2022 Equinor ASA

This program is free software: you can redistribute it and/or modify it under the terms of version 3 of the GNU Lesser General Public License as published by the Free Software Foundation.

This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with this program. If not, see <https://www.gnu.org/licenses/>.
 */

namespace IriTools;

/// <summary>
/// Useful for other IRIs because a fragment is not part of a URI, and URI.Equals ignores the fragment.
/// A URI Reference includes the fragment
/// </summary>
[Serializable]
public class IriReference : IEquatable<IriReference>
{
    public Uri uri { get; set; }

    public static implicit operator IriReference(Uri uri) => new(uri);
    public static implicit operator IriReference(string uri) => new(uri);
    public static implicit operator Uri(IriReference r) => r.uri;


    bool IEquatable<IriReference>.Equals(IriReference? other) =>
        other != null && (ReferenceEquals(this, other) || ToString().Equals(other.ToString()));

    public override bool Equals(object? other) =>
        other != null && (ReferenceEquals(this, other) || (other is IriReference iri && ToString().Equals(iri.ToString())));

    public override string ToString() => uri.ToString();



    /// <summary>
    /// Cannot use Uri.getHashCode since that ignores the fragment
    /// </summary>
    public override int GetHashCode() => ToString().GetHashCode();

    public IriReference(Uri uri)
    {
        this.uri = uri;
    }
    public IriReference(string uriString)
    {
        uri = new Uri(uriString);
    }
}