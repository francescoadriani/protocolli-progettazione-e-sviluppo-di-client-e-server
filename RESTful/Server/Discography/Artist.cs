using System.Collections.Generic;
using System.Diagnostics;

namespace AudioLibraryServerRESTful.Discography
{
    [DebuggerDisplay("{Name} (ArtistId = {ArtistId})")]
    public class Artist
    {
        public Link<long> ID { get; set; }
        public string Name { get; set; }
        public List<Link<long>> AlbumsList { get; set; } = new List<Link<long>>();
    }
}
