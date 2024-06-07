using System.ComponentModel.DataAnnotations;

namespace SPSP.Models.Request.MenuItem
{
    public class MenuUpdateRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        public bool? IsActive { get; set; }
    }
}