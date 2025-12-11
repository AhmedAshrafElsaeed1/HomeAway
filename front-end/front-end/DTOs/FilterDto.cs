namespace front_end.DTOs
{
    public class SearchFilterDto
    {
        public decimal? MaxPrice { get; set; }   // الحد الأقصى للسعر
        public int? StarRating { get; set; }     // عدد النجوم: 4 أو 5 فقط
    }
}
