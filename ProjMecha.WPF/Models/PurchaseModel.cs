using ProjMecha.DL.People;
using ProjMecha.DL.Store;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjMecha.WPF.Models
{
    public class PurchaseModel
    {
        public int PurchaseID { get; set; }
        [Range(0, double.MaxValue)]
        public double TotalPrice { get; set; }
        public StoreItem StoreItem { get; set; }
        public string StoreItemName { get; set; }
        [Range(0, int.MaxValue)]
        public string QuantityPreview { get; set; }
        public int Quantity { get; set; } = 0;
        public Credentials Credentials { get; set; }
        public string ClientName { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsValid()
        {
            int quantity = 0;
            var valid = true;
            valid &= StoreItem is not null;
            valid &= Credentials is not null;
            valid &= int.TryParse(QuantityPreview, out quantity);
            valid &= quantity > 0;
            if (valid)
            {
                Quantity = quantity;
                TotalPrice = Quantity * StoreItem.Price;
            } 

            return valid;
        }
    }
}
