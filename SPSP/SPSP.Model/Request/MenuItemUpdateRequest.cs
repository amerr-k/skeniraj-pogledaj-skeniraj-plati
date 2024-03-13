namespace SPSP.Services
{
    public class MenuItemUpdateRequest
    {
        public int MenuId { get; set; }
        public int? CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
    }
}