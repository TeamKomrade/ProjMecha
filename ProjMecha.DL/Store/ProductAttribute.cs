using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjMecha.DL.Store
{
    public class ProductAttribute
    {
        [Key]
        public int ProductAttributeID { get; set; }
        [MinLength(2)]
        public string Name { get; set; }
        public ICollection<AttributeStoreItemPair> AttributeProductPairs { get; set; }
    }
}
