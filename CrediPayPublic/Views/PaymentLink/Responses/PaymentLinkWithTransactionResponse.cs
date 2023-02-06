using CrediPayPublic.Models.Enum;
using CrediPayPublic.Views.Shared.Responses;
using System.Collections.Generic;
using System;

namespace CrediPayPublic.Views.PaymentLink.Responses
{
    public class PaymentLinkWithTransactionResponse
    {
        public PaymentLinkInfo PaymentLinkInfo { get; set; } = new PaymentLinkInfo();
        public List<PaymentLinkTransaction> PaymentLinkTransaction { get; set; } = new List<PaymentLinkTransaction>();
        public PaginationInfo PageInfo { get; set; } = new PaginationInfo();
    }
    public class PaymentLinkTransaction
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string SourceAccountNumber { get; set; }
        public string SourceAccountName { get; set; }
        public string SourceBankName { get; set; }
        public PaymentChannel Channel { get; set; }
        public decimal Amount { get; set; }
        public PaymentRoute PaymentRoute { get; set; }
        public decimal PayableAmount { get; set; }
        public decimal Charge { get; set; }
        public string CurrencyCode { get; set; }
        public string TrasactionReference { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionStatus Status { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
