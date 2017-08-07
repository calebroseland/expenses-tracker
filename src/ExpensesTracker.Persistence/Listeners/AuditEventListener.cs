using ExpensesTracker.Domain;
using NHibernate.Event;
using NHibernate.Persister.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Persistence.Listeners
{
    public class AuditEventListener : IPreUpdateEventListener, IPreInsertEventListener
    {
        public bool OnPreUpdate(PreUpdateEvent e)
        {
            var audit = e.Entity as IAuditInformation;
            if (audit != null)
            {
                var time = DateTime.Now;
                Set(e.Persister, e.State, "UpdatedOn", time);
                audit.UpdatedOn = time;
            }

            return false;
        }

        public bool OnPreInsert(PreInsertEvent e)
        {
            var audit = e.Entity as IAuditInformation;
            if (audit != null)
            {
                var time = DateTime.Now;

                Set(e.Persister, e.State, "CreatedOn", time);
                Set(e.Persister, e.State, "UpdatedOn", time);

                audit.CreatedOn = time;
                audit.UpdatedOn = time;
            }

            return false;
        }

        private void Set(IEntityPersister persister, object[] state, string propertyName, object value)
        {
            var index = Array.IndexOf(persister.PropertyNames, propertyName);
            if (index != -1)
            {
                state[index] = value;
            }
        }
    }
}
