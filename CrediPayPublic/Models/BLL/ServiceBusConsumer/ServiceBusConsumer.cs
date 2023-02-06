using CrediPayPublic.Models.BLL.Redis;
using CrediPayPublic.Views.Shared.Requests;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebPush;

namespace CrediPayPublic.Models.BLL.ServiceBusConsumer
{
    public interface IServiceBusConsumer
    {
        void RegisterOnMessageHandlerAndReceiveMessages();

        Task CloseQueueAsync();
    }

    public class ServiceBusConsumer : IServiceBusConsumer
    {
        private readonly IConfiguration _configuration;
        private readonly SubscriptionClient _subClient;
        private readonly ILogger _logger;
        private readonly IServiceScopeFactory _serviceProvider;

        public ServiceBusConsumer(
            IConfiguration configuration,
            ILogger<ServiceBusConsumer> logger,
            // IProvidusVirtualAccountService providusVirtualAccountService,
            IServiceScopeFactory serviceProvider)
        {
            _configuration = configuration;
            _logger = logger;
            // _providusVirtualAccountService = providusVirtualAccountService;
            _subClient = new SubscriptionClient(
                  _configuration.GetConnectionString("SBConn"), "pushnotification", "web");
            _serviceProvider = serviceProvider;
        }

        public void RegisterOnMessageHandlerAndReceiveMessages()
        {
            // Configure the message handler options in terms of exception handling, number of concurrent messages to deliver, etc.
            MessageHandlerOptions messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                // Maximum number of concurrent calls to the callback ProcessMessagesAsync(), set to 1 for simplicity.
                // Set it according to how many messages the application wants to process in parallel.
                MaxConcurrentCalls = 1,

                // Indicates whether MessagePump should automatically complete the messages after returning from User Callback.
                // False below indicates the Complete will be handled by the User Callback as in `ProcessMessagesAsync` below.
                AutoComplete = false,
            };

            // Register the function that processes messages.
            _subClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }

        private async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            string msg = Encoding.UTF8.GetString(message.Body);

            await ProcessMessageEvent(msg);

            await _subClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private Task ProcessMessageEvent(string msg)
        {
            var subject = _configuration.GetSection("subject").Value;
            var publicKey = _configuration.GetSection("publicKey").Value;
            var privateKey = _configuration.GetSection("privateKey").Value;
            IRedisCache _redis = (IRedisCache)_serviceProvider.CreateScope().ServiceProvider.GetRequiredService(typeof(IRedisCache));
            var msgObj = JsonConvert.DeserializeObject<PushNotificationMessge>(msg);

            var client = _redis.GetCache($"{msgObj.Client}-sub").Result;
            if (client == null)
            {
                return Task.FromResult("");
            }

            var subscription = JsonConvert.DeserializeObject<PushSubscription>(client);
            var vapidDetails = new VapidDetails(subject, publicKey, privateKey);

            try
            {
                var webPushClient = new WebPushClient();
                webPushClient.SendNotification(subscription, msgObj.Message, vapidDetails);
            }
            catch (Exception exception)
            {
                _logger.LogError("PushNotification", exception);
                return Task.FromResult("Error");
            }

            return Task.FromResult("Ok");
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            _logger.LogError(exceptionReceivedEventArgs.Exception, "Message handler encountered an exception");
            ExceptionReceivedContext context = exceptionReceivedEventArgs.ExceptionReceivedContext;

            _logger.LogDebug($"- Endpoint: {context.Endpoint}");
            _logger.LogDebug($"- Entity Path: {context.EntityPath}");
            _logger.LogDebug($"- Executing Action: {context.Action}");

            return Task.CompletedTask;
        }

        public async Task CloseQueueAsync()
        {
            await _subClient.CloseAsync();
        }
    }
}