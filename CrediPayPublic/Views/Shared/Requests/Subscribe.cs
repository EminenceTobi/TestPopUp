namespace CrediPayPublic.Views.Shared.Requests
{
    public class SubscribePayload
    {
        public string Endpoint { get; set; }
        public string P256dh { get; set; }
        public string Auth { get; set; }
    }

    public class ClientPushSubscription
    {
        public string Client { get; set; }
        public WebPush.PushSubscription Subscriber { get; set; }
    }

    public class PushNotificationMessge
    {
        public string Client { get; set; }
        public string Message { get; set; }
    }
}
