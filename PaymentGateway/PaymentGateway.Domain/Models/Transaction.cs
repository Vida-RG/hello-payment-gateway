using System;

namespace PaymentGateway.Domain.Models
{
    public class Transaction
    {
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public DateTime ExperyDate { get; set; }
        public int? CVVNumber { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyISO4217Code { get; set; }
    }
}
