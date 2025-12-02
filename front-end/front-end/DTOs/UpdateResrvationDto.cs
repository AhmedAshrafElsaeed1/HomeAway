namespace front_end.DTOs
{
    public class UpdateResrvationDto
    {
        public int Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int Status { get; set; }
    }
}
