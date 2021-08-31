using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace ClassLibrary.Hibernate
{
    public class CrudRepository
    {
        public CrudRepository() { }
        /// <summary>
        /// Добавление элемента в бд
        /// </summary>
        /// <param name="hibernateNotes">Элемент</param>
        public void AddElement(NodeHibernate hibernateNotes)
        {
            using (ISession s = Session.OpenSession())
            using (ITransaction tr = s.BeginTransaction())
            {
                hibernateNotes.Id = (long)s.Save(hibernateNotes);
                tr.Commit();//здесь мы сохраняем все в базу
            }
        }
        /// <summary>
        /// Выводим записи
        /// </summary>
        /// <returns>список записей</returns>
        public List<NodeHibernate> GetAll()
        {
            List<NodeHibernate> notes;

            using (var session = Session.OpenSession())
            {
                notes = session.Query<NodeHibernate>().ToList<NodeHibernate>();
            }

            return notes;
        }
        /// <summary>
        /// Производим обновление элемента(действие над сущностью)
        /// </summary>
        /// <param name="hibernateNotes">Элемент бд</param>
        public void EditAtElement(NodeHibernate hibernateNotes)
        {
            using (ISession session = Session.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Update(hibernateNotes);
                transaction.Commit();//здесь мы сохраняем все в базу
            }
        }
        /// <summary>
        /// Удаление элемента из бд
        /// </summary>
        /// <param name="index">номер элемента</param>
        public void DeleteAtElement(long index)
        {
            using (ISession s = Session.OpenSession())
            using (ITransaction tr = s.BeginTransaction())
            {
                s.Delete(GetAtElement(index));
                tr.Commit();//здесь мы сохраняем все в базу
            }
        }
        /// <summary>
        /// Получение элемента по первичному ключу
        /// </summary>
        /// <param name="index">номер элемента</param>
        /// <returns></returns>
        public NodeHibernate GetAtElement(long index)
        {
            NodeHibernate notes;
            using (ISession s = Session.OpenSession())
            {
                notes = s.Get<NodeHibernate>(index);
            }

            return notes;
        }
    }
}
