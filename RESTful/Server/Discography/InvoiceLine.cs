using System.Diagnostics;

namespace AudioLibraryServerRESTful.Discography
{
    [DebuggerDisplay("InvoiceLineId = {InvoiceLineId}")]
    public class InvoiceLine
    {
        public int InvoiceLineId { get; set; }
        public int CustomerId { get; set; }
        public int TrackId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public Customer Customer { get; set; }
        public Track Track { get; set; }
    }
}
