using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AudioLibraryServerRESTful.Discography
{
    [DebuggerDisplay("{Title} (AlbumId = {AlbumId})")]
    public class Album
    {
        public Link<long> ID;
        public String Title { get; set; }
        public Link<long> Artist { get; set; }
        public List<Link<long>> TracksList { get; set; } = new List<Link<long>>();
    }
}