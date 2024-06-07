using System.ComponentModel.DataAnnotations;

namespace SPSP.Models.Request.MenuItem
{
    public class MenuCreateRequest
    {

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        public bool? IsActive { get; set; }

    }
}

