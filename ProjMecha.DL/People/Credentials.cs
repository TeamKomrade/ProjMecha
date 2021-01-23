using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjMecha.DL.People
{
    public class Credentials
    {
        [Key]
        public int CredentialsID { get; set; }
        [MinLength(3)]
        public string FullName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public AccountType AccountType { get; set; }
        public ICollection<PersonalDataPair> PersonalData {get;set;}

    }
}
