using front_end.DTOs;

namespace front_end.ViewModel
{
    public class ProfileViewModel
    {
        public UserDto? User { get; set; }
        public UserDto? CurrentUser { get; set; }
        public bool IsSignedIn { get; set; }

        public List<ReservationDto>? Bookings { get; set; }

        public int TotalBookings => Bookings?.Count ?? 0;
        public int Completed =>
            Bookings?.Count(b => b.Status == 1) ?? 0;
        public int Upcoming =>
            Bookings?.Count(b => b.Status == 0) ?? 0;
    }
}
