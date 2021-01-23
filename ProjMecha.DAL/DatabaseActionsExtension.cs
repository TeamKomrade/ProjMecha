using ProjMecha.DL.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjMecha.DAL
{
    public static class DatabaseActionsExtension
    {
        public static bool TryAction(this MechanicDatabase db, DatabaseActions action, object o)
        {
            string message = "успешно";
            bool result = true;
            try
            {
                switch (action)
                {
                    case DatabaseActions.ADD: { db.Add(o); break; }
                    case DatabaseActions.DELETE: { db.Remove(o); break; }
                    case DatabaseActions.UPDATE: { db.Update(o); break; }
                }
                
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                message = $"с ошибкой: \n {ex.Message}";
                result = false;
            }
            db.OnNotifyAboutAction($"Действие [{action}] произведено {message}");
            return result;
        }
    }

    public enum DatabaseActions
    {
        ADD,
        DELETE,
        UPDATE
    }
}
