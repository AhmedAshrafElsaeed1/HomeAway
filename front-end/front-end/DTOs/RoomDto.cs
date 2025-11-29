using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAway.Application.DTOs
{
    public class RoomDto
    {
        public int Id { get; set; }
        //public string HotelName { get; set; }
        public int Quantity { get; set; }
        public int Type { get; set; }
        public bool IsAvailable { get; set; }
        public int? HotelId { get; set; }

        public string? Number { get; set; }
        public decimal Price { get; set; }
    }
}
