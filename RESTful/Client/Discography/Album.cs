using System.Diagnostics;

namespace restservice.Discography
{
    [DebuggerDisplay("{Title} (AlbumId = {AlbumId})")]
    public class Album
    {
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
    }
}