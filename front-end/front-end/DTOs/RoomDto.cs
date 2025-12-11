using System;

namespace front_end.DTOs
{
    public class RoomDto
    {
        public int Id { get; set; }

        // عدد الغرف المتاحة
        public int Quantity { get; set; }

        // نوع الغرفة
        // 0 = Single
        // 1 = Double
        // 2 = Triple
        // 3 = Quadruple
        // 4 = Penthouse
        public int Type { get; set; }

        // هل الغرفة متاحة للحجز
        public bool IsAvailable { get; set; }

        // رقم الفندق المرتبطة به الغرفة
        public int? HotelId { get; set; }

        // رقم الغرفة
        public string? Number { get; set; }

        // سعر الغرفة
        public decimal Price { get; set; }
    }
}
