using ExpensesTracker.Persistence.Listeners;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Event;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Persistence
{
    public class InMemorySessionHelper : ISessionHelper
    {
        private const string CONNECTION_STRING = "Data Source=:memory:";

        private Configuration _config;
        private ISessionFactory _sessionFactory;
        private SQLiteConnection _connection;

        public InMemorySessionHelper()
        {
            _config = new Configuration()
                .DataBaseIntegration(db =>
                {
                    db.Dialect<SQLiteDialect>();
                    db.Driver<SQLite20Driver>();
                    db.ConnectionString = CONNECTION_STRING;
                    db.LogFormattedSql = true;
                    db.LogSqlInConsole = true;
                })
                .SetNamingStrategy(ImprovedNamingStrategy.Instance)
                .AddAssembly("ExpensesTracker.Persistence");

            _config.SetListener(ListenerType.PreInsert, new AuditEventListener());
            _config.SetListener(ListenerType.PreUpdate, new AuditEventListener());

            _sessionFactory = _config.BuildSessionFactory();
        }

        public ISession OpenSession()
        {
            return _sessionFactory.OpenSession(GetConnection());
        }

        private SQLiteConnection GetConnection()
        {
            if (_connection == null)
            {
                _connection = new SQLiteConnection(CONNECTION_STRING);
                _connection.Open();

                SchemaExport se = new SchemaExport(_config);
                se.Execute(false, true, false, _connection, null);
            }

            return _connection;
        }
    }
}
