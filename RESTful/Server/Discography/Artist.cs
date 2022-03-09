using System.Diagnostics;

namespace restservice.Discography
{
    [DebuggerDisplay("{Name} (ArtistId = {ArtistId})")]
    public class Artist
    {
        public Link<long> ID { get; set; }
        public string Name { get; set; }
    }
}
