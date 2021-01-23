using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjMecha.WPF.Models
{
    public class LoginModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsValid() => !(string.IsNullOrEmpty(Login) && string.IsNullOrEmpty(Password));
    }
}
