using System.Diagnostics;

namespace restservice.Discography
{
    [DebuggerDisplay("{Name} (MediaTypeId = {MediaTypeId})")]
    public class MediaType
    {
        public int MediaTypeId { get; set; }
        public string Name { get; set; }
    }
}
