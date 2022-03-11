using System.Diagnostics;
using System.Runtime.Serialization;

namespace AudioLibraryServerRESTful.Discography
{
    [DebuggerDisplay("{Name} (TrackId = {TrackId})")]
    public class Track
    {
        public Link<long> ID { get; set; }
        public string Name { get; set; }
        public string Composer { get; set; }
        public long Milliseconds { get; set; }
        public long Bytes { get; set; }
        public decimal UnitPrice { get; set; }
        public Link<long> Album { get; set; }
        public Link<long> MediaType { get; set; }
        public Link<long> Genre { get; set; }
    }

    public class TrackContainer
    {
        public Track track;
    }
}
