namespace front_end.DTOs
{
    public class UpdateResrvationDto
    {
        public int Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int Status { get; set; }
        // 0 = pending
        // 1 = confirmed
        // 2 = canceled
        // 3 = Cpmleted
    }
}
