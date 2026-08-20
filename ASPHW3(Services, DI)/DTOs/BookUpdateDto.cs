namespace ASPHW3_Services__DI_.DTOs
{
    public class BookUpdateDto
    {
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int PageCount { get; set; }
        public bool IsAvailable { get; set; }
    }
}
