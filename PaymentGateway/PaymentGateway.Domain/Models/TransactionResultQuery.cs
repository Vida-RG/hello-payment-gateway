﻿namespace PaymentGateway.Domain.Models
{
    public class TransactionResultQuery
    { 
        public PaymentStatusCode Code { get; set; }
        public string BankId { get; set; }
        public string PaymentId { get; set; }
        Transaction TransactionDetails { get; set; }
    }
}
