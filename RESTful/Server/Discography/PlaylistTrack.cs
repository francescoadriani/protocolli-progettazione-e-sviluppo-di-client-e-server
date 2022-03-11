using System.Diagnostics;
using System.Runtime.Serialization;

namespace AudioLibraryServerRESTful.Discography
{
    [DebuggerDisplay("PlaylistId = {PlaylistId}, TrackId = {TrackId}")]
    public class PlaylistTrack
    {
        public int PlaylistId { get; set; }
        public int TrackId { get; set; }
        public Playlist Playlist { get; set; }
        public Track Track { get; set; }
    }
}
