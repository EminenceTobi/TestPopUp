namespace CrediPayPublic.Models.Enum
{
    public enum PreferenceSettings
    {
        Accept_Payment_via_Bank = 1,
        Accept_Payment_via_Card,
        Accept_Payment_via_QR,
        Accept_Payment_via_USSD,
        Accept_Payment_via_Transfer,
        Send_Transaction_Receipt_to_Me,
        Send_Transaction_Receipt_to_Customer,
        Send_Transfer_Receipts_to_Me,
        Send_Transfer_Receipts_to_Customer,
        Confirm_Transfer_Before_Sending,
        Send_OTP_via_Email,
        Send_Daily_Issue_Reports_to_Me,
        Webhook_Events_For_Expiring_Card,
        Completed_Subscription_Alerts,
        Cancelled_Subscription_Alerts,
        Send_Expiring_Card_Alerts_to_Me,
        Send_Expiring_Card_Alerts_to_Customer,
    }
}