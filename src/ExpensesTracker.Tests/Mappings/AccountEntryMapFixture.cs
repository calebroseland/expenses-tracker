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
    public class AccountEntryMapFixture : BaseInMemoryFixture
    {
        [Test]
        public void AccountEntryProperties()
        {
            object entityId = null;
            object categoryId = null;
            object accountId = null;
            DateTime now = DateTime.Now;

            using (var session = OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                accountId = session.Save(new Account { Name = "Account Name" });
                categoryId = session.Save(new Category { Name = "Category Name" });

                entityId = session.Save(new AccountEntry
                {
                    Value = 123.45M,
                    Date = now,
                    Description = "Entry Description",
                    Account = session.Load<Account>(accountId),
                    Category = session.Load<Category>(categoryId)
                });
                transaction.Commit();
            }

            using (var session = OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var entity = session.Get<AccountEntry>(entityId);

                Assert.NotNull(entity);
                Assert.That(entity.Value, Is.EqualTo(123.45M));
                Assert.That(entity.Date, Is.EqualTo(now).Within(3).Hours);
                Assert.AreEqual("Entry Description", entity.Description);

                Assert.AreEqual(accountId, entity.Account.Id);
                Assert.AreEqual("Account Name", entity.Account.Name);

                Assert.AreEqual(categoryId, entity.Category.Id);
                Assert.AreEqual("Category Name", entity.Category.Name);
            }
        }
    }
}
