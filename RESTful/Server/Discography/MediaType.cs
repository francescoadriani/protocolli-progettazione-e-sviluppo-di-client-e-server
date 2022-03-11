using System.Collections.Generic;
using System.Diagnostics;

namespace AudioLibraryServerRESTful.Discography
{
    [DebuggerDisplay("{Name} (MediaTypeId = {MediaTypeId})")]
    public class MediaType
    {
        public Link<long> ID { get; set; }
        public string Name { get; set; }
        public List<Link<long>> TracksList { get; set; } = new List<Link<long>>();
    }
}
