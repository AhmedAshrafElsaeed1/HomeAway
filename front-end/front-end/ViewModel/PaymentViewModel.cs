namespace front_end.ViewModels
{
    public class PaymentViewModel
{
    public string UserId { get; set; } = string.Empty;
    public string CardHolderName { get; set; } = string.Empty;
    public string CardNumber { get; set; } = string.Empty;
    public string Expiry { get; set; } = string.Empty;
    public int CVV { get; set; }

    // بيانات الحجز
    public int RoomId { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public decimal TotalPrice { get; set; }
        
    }
}

