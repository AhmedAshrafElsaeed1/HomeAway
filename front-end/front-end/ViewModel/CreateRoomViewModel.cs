namespace front_end.ViewModel
{
    public class CreateRoomViewModel
    {
        public string Number { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; } = 1;
        public IFormFileCollection? Photos { get; set; }
    }
}

