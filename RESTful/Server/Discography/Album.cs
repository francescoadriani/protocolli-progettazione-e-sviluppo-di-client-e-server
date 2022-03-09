using System;
using System.Diagnostics;

namespace restservice.Discography
{
    [DebuggerDisplay("{Title} (AlbumId = {AlbumId})")]
    public class Album
    {
        public long ID;
        public String Title { get; set; }
        public Link<long> Artist { get; set; }
    }
}