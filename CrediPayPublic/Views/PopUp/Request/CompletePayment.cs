using CrediPayPublic.Models.Enum;

namespace CrediPayPublic.Views.PopUp.Request
{
    public class CompletePaymentRequest
    {
        public string PaymentReference { get; set; } = string.Empty;
        public string PaymentChannel { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;

    }
}
