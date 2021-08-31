using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace ClassLibrary.Hibernate
{
    public class Session
    {
        public static ISession OpenSession()
        {
            ISessionFactory sessionFactory = Fluently.Configure()
                //Настройки БД. Строка подключения к БД MS Sql Server 
                .Database(FluentNHibernate.Cfg.Db.MsSqlConfiguration.MsSql2012
                .ConnectionString(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\163ty\Documents\MyDataBase.mdf;Integrated Security=True;Connect Timeout=30").ShowSql())
                //Маппинг. Используя AddFromAssemblyOf NHibernate будет пытаться маппить КАЖДЫЙ класс в этой сборке (assembly). Можно выбрать любой класс.
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<NodeHibernate>())
            //SchemeUpdate позволяет создавать/обновлять в БД таблицы и поля (2 поля ==true)
            .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(true, true))
            .BuildSessionFactory();

            return sessionFactory.OpenSession();
        }
    }
}
