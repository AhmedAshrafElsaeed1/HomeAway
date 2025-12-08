using front_end.DTOs;

namespace front_end.ViewModel
{
    public class ProviderDashboardViewModel
    {
        public HotelDto HotelInfo { get; set; }
        public List<RoomDto> Rooms { get; set; } = new List<RoomDto>();
        public List<ReservationDto> Reservations { get; set; } = new List<ReservationDto>();
        public decimal TotalRevenue { get; set; }
    }
}