using System;
using System.Diagnostics;

namespace AudioLibraryServerRESTful.Discography
{
    [DebuggerDisplay("InvoiceId = {InvoiceId}")]
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string BillingAddress { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingCountry { get; set; }
        public string BillingPostalCode { get; set; }
        public Decimal Total { get; set; }
        public Customer Customer { get; set; }
    }
}
