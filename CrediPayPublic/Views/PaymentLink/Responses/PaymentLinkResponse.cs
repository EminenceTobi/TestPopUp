using System.Collections.Generic;
using System;
using CrediPayPublic.Views.Shared.Responses;
using CrediPayPublic.Models.Enum;

namespace CrediPayPublic.Views.PaymentLink.Responses
{
    public class PaymentLinkInfo
    {
        public Guid Id { get; set; }
        public string SendNotification { get; set; } = string.Empty;
        public bool CollectCustomerPhone { get; set; } = false;
        public decimal Amount { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string PageName { get; set; }
        public string AdditionalInformation { get; set; }
        public string Alias { get; set; }
        public string BusinessName { get; set; }
        public bool IsPublished { get; set; }
        public LinkStatus LinkStatus { get; set; }
        public PaymentLinkType PaymentLinkType { get; set; }
        public DateTime DateCreated { get; set; }
    }

    public class PaymentLinkResponse
    {
        public List<PaymentLinkInfo> PaymentLinkInfo { get; set; } = new List<PaymentLinkInfo>();
        public PaginationInfo PageInfo { get; set; } = new PaginationInfo();
    }
}
