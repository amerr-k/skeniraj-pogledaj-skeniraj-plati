using System.ComponentModel.DataAnnotations;

namespace SPSP.Services
{
    public class MenuItemCreateRequest
    {
        [Required]
        public int MenuId { get; set; }
        public int? CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        [MinLength(1)]
        [MaxLength(10)]
        public string Code { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
    }
}

