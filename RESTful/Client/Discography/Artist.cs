using System.Diagnostics;

namespace restservice.Discography
{
    [DebuggerDisplay("{Name} (ArtistId = {ArtistId})")]
    public class Artist
    {
        public int ArtistId { get; set; }
        public string Name { get; set; }
    }
}
