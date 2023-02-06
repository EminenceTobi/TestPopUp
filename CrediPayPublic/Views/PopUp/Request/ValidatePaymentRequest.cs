using CrediPayPublic.Views.PaymentLink.Request;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CrediPayPublic.Views.PopUp.Request
{
    public class ValidatePaymentRequest
    {
        [Required]
        [Range(0.01, 999999999)]
        public decimal Amount { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public List<SplitPaymentOptions> SplitPaymentOptions { get; set; }
    }
}
