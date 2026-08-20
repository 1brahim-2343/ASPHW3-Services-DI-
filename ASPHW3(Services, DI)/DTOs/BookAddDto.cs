namespace ASPHW3_Services__DI_.DTOs
{
    public class BookAddDto
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Category { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int PageCount { get; set; }
        public int PublishedYear { get; set; }
        public bool IsAvailable { get; set; }
    }
}
