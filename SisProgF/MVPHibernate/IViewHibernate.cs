using ClassLibrary.Hibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisProgF.MVPHibernate
{
    public interface IViewHibernate
    {
        event EventHandler GetAllDataBaseEvent;
        event EventHandler AddElementInDataBaseEvent;
        event EventHandler EditElementInDataBaseEvent;
        event EventHandler DeleteElementInDataBaseEvent;

        long IndexRecordHibernate { get; }
        string LogMessage { set; }
        List<NodeHibernate> RecordsHibernate { get; set; }
    }
}
