using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjMecha.DL.Store
{
    public class StoreItem
    {
        public int StoreItemID { get; set; }
        [MinLength(3)]
        public string Title { get; set; }
        public string Description { get; set; }
        [Range(0, double.MaxValue)]
        public double Price { get; set; }
        public ICollection<AttributeStoreItemPair> AttributeProductPairs { get; set; }
        public StoreItemType StoreItemType { get; set; }
    }

    

    public enum StoreItemType
    {
        Product,
        Service
    }
}
