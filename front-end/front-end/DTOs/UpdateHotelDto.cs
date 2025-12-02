namespace front_end.DTOs
{
    public class UpdateHotelDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string[]? images { get; set; } = Array.Empty<string>();
        public int Rating { get; set; }
    }
}
