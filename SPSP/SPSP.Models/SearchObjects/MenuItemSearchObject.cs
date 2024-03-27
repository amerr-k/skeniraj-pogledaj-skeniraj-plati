using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPSP.Models.SearchObjects
{
    public class MenuItemSearchObject : BaseSearchObject
    {
        public string? Name { get; set; }
        public string? FTS { get; set; }
    }
}
