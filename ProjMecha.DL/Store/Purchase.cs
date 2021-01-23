using ProjMecha.DL.People;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjMecha.DL.Store
{
    public class Purchase
    {   
        [Key]
        public int PurchaseID { get; set; }
        [Range(0, double.MaxValue)]
        public double TotalPrice { get; set; }
        public StoreItem StoreItem { get; set; }
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; } = 1;
        public Credentials Credentials { get; set; }
        public DateTime Timestamp { get; set; }


    }
}
