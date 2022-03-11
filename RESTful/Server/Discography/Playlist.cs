using System.Diagnostics;

namespace AudioLibraryServerRESTful.Discography
{
    [DebuggerDisplay("{Name} (PlaylistId = {PlaylistId})")]
    public class Playlist
    {
        public int PlaylistId { get; set; }
        public string Name { get; set; }
    }
}
