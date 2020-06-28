using System;

namespace InvoicesApi
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public DateTime IssueDate { get; set; }
        public decimal Amount { get; set; }
        public string Number { get; set; }
        public int ClientId { get; set; }
    }
}