using ExpensesTracker.Persistence;
using NHibernate;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Tests
{
    public class BaseInMemoryFixture
    {
        protected ISessionHelper SessionHelper;

        [OneTimeSetUp]
        public void Setup()
        {
            SessionHelper = new InMemorySessionHelper();
        }

        protected ISession OpenSession()
        {
            return SessionHelper.OpenSession();
        }
    }
}
