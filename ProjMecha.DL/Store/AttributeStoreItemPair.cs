using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjMecha.DL.Store
{
    public class AttributeStoreItemPair
    {
        public int AttributeStoreItemPairID { get; set; }
        public StoreItem StoreItem { get; set; }
        public int StoreItemID { get; set; }
        public ProductAttribute ProductAttribute { get; set; }

        public string Value { get; set; }
    }
}
