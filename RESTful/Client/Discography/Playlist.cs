using System.Diagnostics;

namespace restservice.Discography
{
    [DebuggerDisplay("{Name} (PlaylistId = {PlaylistId})")]
    public class Playlist
    {
        public int PlaylistId { get; set; }
        public string Name { get; set; }
    }
}
