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
    public class CategoryMapFixture : BaseInMemoryFixture
    {
        [Test]
        public void CategoryProperties()
        {
            object entityId = null;

            using (var session = OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                entityId = session.Save(new Category { Name = "Category Name" });
                transaction.Commit();
            }

            using (var session = OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var entity = session.Get<Category>(entityId);

                Assert.NotNull(entity);
                Assert.AreEqual("Category Name", entity.Name);
            }
        }
    }
}
