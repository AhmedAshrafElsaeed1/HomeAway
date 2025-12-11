using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace front_end.ViewModel
{
    public class HotelRegisterViewModel
    {
        public string HotelName { get; set; }
        public string Description { get; set; }
        public string HotelAddress { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<IFormFile> Photos { get; set; }
        public int Rating { get; set; }
    }
}

