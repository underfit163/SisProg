using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace ClassLibrary.Hibernate
{
    public class NodeHibernate
    {
        public virtual long Id { get; set; }
        public virtual string Adress { get; set; }
        public virtual int Port { get; set; }
        public virtual string Protocol { get; set; }
    }
    //Маппинг класса
    public class RecorHibernateMap : ClassMap<NodeHibernate>
    {
        public RecorHibernateMap()
        {
            Id(obj => obj.Id);
            Map(obj => obj.Adress);
            Map(obj => obj.Port);
            Map(obj => obj.Protocol);
        }
    }
}
