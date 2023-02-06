using System;
using System.Collections.Generic;
using System.Linq;

namespace CrediPayPublic.Models.Enum
{
    public enum PaymentChannel
    {
        nochannel = 0,
        transfer,
        card,
        qrcode,
        ussd,
        account
    }

    public class ConvertToStringChannel
    {
        private static string FastToString(PaymentChannel channel)
        {
            switch (channel)
            {
                case PaymentChannel.ussd:
                    return nameof(PaymentChannel.ussd);
                case PaymentChannel.account:
                    return nameof(PaymentChannel.account);
                case PaymentChannel.qrcode:
                    return nameof(PaymentChannel.qrcode);
                case PaymentChannel.card:
                    return nameof(PaymentChannel.card);
                case PaymentChannel.transfer:
                    return nameof(PaymentChannel.transfer);
                default:
                    throw new ArgumentOutOfRangeException(nameof(channel), channel, null);
            }
        }
    }

    public class PaymentChannelResponse
    {
        public int Id { get; set; }
        public string PaymentChannel { get; set; } = string.Empty;
    }

    public static class PaymentChannelList
    {
        public static List<PaymentChannelResponse> GetPaymentChannel()
        {
            var role = System.Enum.GetValues(typeof(PaymentChannel))
                       .Cast<PaymentChannel>()
                       .Select(o => new PaymentChannelResponse { Id = (int)o, PaymentChannel = o.ToString() })
                       .ToList();
            return role;
        }
    }
}
