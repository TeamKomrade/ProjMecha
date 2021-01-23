using ProjMecha.DL.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjMecha.DAL
{
    public static class DatabaseCredentialsExtension
    {
        public static IQueryable<Credentials> GetCredentialsByFullNameExact(this IQueryable<Credentials> creds, string fullname)
            => creds.Where(c => c.FullName == fullname);
        public static IQueryable<Credentials> GetCredentialsByFullName(this IQueryable<Credentials> creds, string fullname)
            => creds.Where(c => c.FullName.Contains(fullname));

        public static IQueryable<Credentials> GetCredentialsByLoginPassword(this IQueryable<Credentials> creds, string login, string password)
            => creds.Where(c => c.Login == login && c.Password == password);

        public static IQueryable<Credentials> GetOnlyClients(this IQueryable<Credentials> creds)
            => creds.Where(c => c.AccountType == AccountType.Client);

        public static IQueryable<Credentials> GetOnlyEmployees(this IQueryable<Credentials> creds)
            => creds.Where(c => c.AccountType == AccountType.Employee);

        public static Credentials GetByID(this IQueryable<Credentials> creds, int id)
            => creds.Where(c => c.CredentialsID == id).FirstOrDefault();
    }
}
