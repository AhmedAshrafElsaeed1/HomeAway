using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace front_end.DTOs
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string? UserId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int Status { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
