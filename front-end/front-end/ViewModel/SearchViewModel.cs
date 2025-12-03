using front_end.DTOs;
using System.Collections.Generic;

namespace front_end.ViewModel
{
    public class SearchViewModel
    {
        public List<HotelDto>? Results { get; set; }
        public string? Destination { get; set; }
        public int Guests { get; set; }
        public string? CheckIn { get; set; }
        public string? CheckOut { get; set; }
    }
}
