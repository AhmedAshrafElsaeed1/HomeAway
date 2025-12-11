using front_end.DTOs;

namespace front_end.ViewModel
{
    public class ProfileViewModel
    {
        public UserDto CurrentUser { get; set; } = new UserDto();
        public List<ReservationDto>? Bookings { get; set; } = new List<ReservationDto>();

        // خصائص للإحصائيات
        public int TotalBookings { get; set; }
        public int Completed { get; set; }
        public int Upcoming { get; set; }
    }
}
