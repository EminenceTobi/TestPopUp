using CrediPayPublic.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CrediPayPublic.Views.PaymentLink.Request
{
    public class PaymentLinkRequest
    {
        public string PageName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string AdditionalInformation { get; set; } = string.Empty;
        public string Alias { get; set; } = string.Empty;
        public string SendNotification { get; set; } = string.Empty;
        public PaymentLinkType PaymentLinkType { get; set; }
        public bool CollectCustomerPhone { get; set; }
        public List<SplitPaymentOptions> SplitPaymentOptions { get; set; }
    }
    public class SplitPaymentOptions
    {
        public Guid SubAccountId { get; set; }
        public SplitOptions SplitOptions { get; set; }
    }
    public class SplitOptions
    {
        public ChargeType ChargeType { get; set; } 
        public decimal Value { get; set; }

    }
}
