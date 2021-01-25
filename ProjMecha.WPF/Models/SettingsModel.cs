using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjMecha.WPF.Models
{
    public class SettingsModel
    {
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool IsValid()
        {
            var valid = true;
            valid &= CompanyName.Length > 3;
            valid &= Phone.Length > 9;
            valid &= Address.Length > 3;
            return valid;
        }
    }
}
