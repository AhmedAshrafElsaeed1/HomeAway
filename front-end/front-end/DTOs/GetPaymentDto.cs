using System;

namespace front_end.DTOs
{
    public class GetPaymentDto
    {
        public string CardHolderName { get; set; } = string.Empty;
        public string CardNumber { get; set; } = string.Empty;
        public string Expiry { get; set; } = string.Empty;
        public int CVV { get; set; }
    }
}
