using ProjMecha.DL.People;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjMecha.WPF.Models
{
    public class CredentialsModel
    {
        public int CredentialsID { get; set; }
        [MinLength(3)]
        public string FullName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public AccountType AccountType { get; set; }
        public ICollection<PersonalDataPair> PersonalData { get; set; }
        public bool IsValid()
        {
            bool valid = true;
            valid &= !string.IsNullOrWhiteSpace(FullName);  
            valid &= !string.IsNullOrWhiteSpace(Login);  
            valid &= !string.IsNullOrWhiteSpace(Password);
            return valid;
        }
    }
}
