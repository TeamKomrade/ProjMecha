using ProjMecha.DL.Store;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjMecha.WPF.Models
{
    public class StoreItemModel
    {
        public int StoreItemID { get; set; }
        [MinLength(3)]
        public string Title { get; set; }
        public string Description { get; set; }
        [Range(0, double.MaxValue)]
        public string PricePreview { get; set; }
        public double Price { get; set; } = -1;
        public ICollection<AttributeStoreItemPair> AttributeProductPairs { get; set; }
        public StoreItemType StoreItemType { get; set; }

        public bool IsValid()
        {
            double price = -1;
            var valid = true;
            valid &= !string.IsNullOrWhiteSpace(Title);
            valid &= !string.IsNullOrWhiteSpace(Description);
            valid &= double.TryParse(PricePreview, out price);
            if (valid) { valid &= price >= 0; Price = price; }
            return valid;
        }
    }
}
