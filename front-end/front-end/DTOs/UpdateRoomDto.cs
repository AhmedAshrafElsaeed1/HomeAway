namespace front_end.DTOs
{
    public class UpdateRoomDto
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        public int Type { get; set; }
        public int Quantity { get; set; }
        public Decimal Price { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}
