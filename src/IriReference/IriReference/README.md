# IriReference
This is a C# wrapper around the Uri class to make it work for IRI References. 

The major difference between Uri and IriReference is that the "fragment", the part after the `#` is included in equality treatment. 
This is crucial when using Uris with RDF. 