using System.Diagnostics;

namespace restservice.Discography
{
    [DebuggerDisplay("{Name} (GenreId = {GenreId})")]
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; }
    }
}
