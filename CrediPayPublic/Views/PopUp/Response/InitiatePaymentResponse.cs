namespace CrediPayPublic.Views.PopUp.Response
{
    public class InitiatePaymentResponse
    {
        public decimal Amount { get; set; }
        public string PaymentReference { get; set; } = string.Empty;
        public string TransactionReference { get; set; } = string.Empty;
        public string CurrencyCode { get; set; } = string.Empty; //{NGN}
        public InitiatePaymentAccountInfo AccountInfo { get; set; } = new InitiatePaymentAccountInfo();
    }
    public class InitiatePaymentAccountInfo
    {
        public string AccountName { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public string BankName { get; set; } = string.Empty;
    }
}
