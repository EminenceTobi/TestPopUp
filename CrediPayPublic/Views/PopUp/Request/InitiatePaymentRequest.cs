using CrediPayPublic.Models.Enum;
using CrediPayPublic.Views.PaymentLink.Request;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace CrediPayPublic.Views.PopUp.Request
{
    public class InitiatePaymentRequest
    {
        [Required]
        public decimal Amount { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;

        [Required]
        [MinLength(12)]
        [MaxLength(20)]
        public string PaymentReference { get; set; } = string.Empty;
        [Required]
        public string PaymentChannel { get; set; } = string.Empty; //{transfer, card, qrcode, ussd, account}
        public string CurrencyCode { get; set; } = "NGN"; // {NGN}
        public string PaymentRoute { get; set; } = "pop_up"; //{direct, pop_up, link}
        public Guid? LinkId { get; set; } = default!;
        public CardHolderInfo CardHolderInfo { get; set; }
        public List<SplitPaymentOptions> SplitPaymentOptions { get; set; }
    }
    public class CardHolderInfo
    {
        [Required]
        public string CardNumber { get; set; }
        [Required]
        [StringLength(2)]
        public string ExpiryMonth { get; set; }
        [Required]
        [StringLength(4)]
        public string ExpiryYear { get; set; }

        [Required]
        [StringLength(4)]
        public string Cvv { get; set; }
    }
}
