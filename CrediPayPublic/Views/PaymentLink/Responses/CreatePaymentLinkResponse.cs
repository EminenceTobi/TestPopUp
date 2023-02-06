using CrediPayPublic.Models.Enum;
using CrediPayPublic.Views.PaymentLink.Request;
using System.Collections.Generic;

namespace CrediPayPublic.Views.PaymentLink.Responses
{
    public class CreatePaymentLinkResponse
    {
        public string PageName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string AdditionalInformation { get; set; } = string.Empty;
        public string Alias { get; set; } = string.Empty;
        public string SendNotificationTo { get; set; } = string.Empty;
        public PaymentLinkType PaymentLinkType { get; set; }
        public bool CollectCustomerPhone { get; set; }
        public List<SplitPaymentOptions> SplitPaymentOptions { get; set; }
    }
}
