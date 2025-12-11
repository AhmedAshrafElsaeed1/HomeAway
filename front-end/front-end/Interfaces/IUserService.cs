using front_end.DTOs;
using System.Threading.Tasks;

namespace front_end.Interfaces
{
    public interface IUserService
    {
        // Get payment info by user ID
        Task<GetPaymentDto> GetPaymentAsync(string userId);

        // Set payment info
        Task<bool> SetPaymentAsync(PaymentDto payment);
    }
}

