using ExpensesTracker.Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Tests.Mappings
{
    [TestFixture]
    public class AccountMapFixture : BaseInMemoryFixture
    {
        [Test]
        public void AccountProperties()
        {
            object entityId = null;

            using (var session = OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                entityId = session.Save(new Account { Name = "Account Name" });
                transaction.Commit();
            }

            using (var session = OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var entity = session.Get<Account>(entityId);

                Assert.NotNull(entity);
                Assert.That("Account Name", Is.EqualTo(entity.Name));
            }
        }
    }
}
