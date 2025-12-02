namespace front_end.DTOs
{
    public class UpdateRoomDto
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        public int Type { get; set; }
        // 0 = single
        // 1 = double
        // 2 = triple
        // 3 = quadruple
        // 4 = penthouse

        public int Quantity { get; set; }
        public Decimal Price { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}
