using ClassLibrary;
using ClassLibrary.Hibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisProgF.Forms;
using System.Windows.Forms;

namespace SisProgF.MVPHibernate
{
    public class ModelHibernate
    {
        private CrudRepository _crudRepository;

        public ModelHibernate()
        {
            _crudRepository = new CrudRepository();
        }
        /// <summary>
        /// Изменить элемент
        /// </summary>
        /// <param name="recordsHibernate">Список элементов бд</param>
        /// <param name="indexRecord">номер изменяемого элемента</param>
        /// <returns>возвращаемый список</returns>
        internal List<NodeHibernate> EditElementDB(List<NodeHibernate> recordsHibernate, long indexRecord)
        {
            var element = GetRecordHibernate(recordsHibernate[GetElementList(recordsHibernate, indexRecord)]);

            if (element != null)
            {
                element.Id = indexRecord;
                _crudRepository.EditAtElement(element);
                recordsHibernate[GetElementList(recordsHibernate, indexRecord)] = element;
            }

            return recordsHibernate;
        }
        /// <summary>
        /// Удалить элемент из бд
        /// </summary>
        /// <param name="recordsHibernate">Список элементов бд</param>
        /// <param name="indexRecord">номер удаляемого элемента</param>
        /// <returns>возвращаемый список</returns>
        internal List<NodeHibernate> DeleteElementDB(List<NodeHibernate> recordsHibernate, long indexRecord)
        {
            _crudRepository.DeleteAtElement(indexRecord);
            recordsHibernate.RemoveAt(GetElementList(recordsHibernate, indexRecord));

            return recordsHibernate;
        }
        /// <summary>
        /// Добавить элемент в бд
        /// </summary>
        /// <param name="recordsHibernate">Список элементов бд</param>
        /// <returns>возвращаемый список</returns>
        internal List<NodeHibernate> AddElementDB(List<NodeHibernate> recordsHibernate)
        {
            var element = GetRecordHibernate();

            if (element != null)
            {
                _crudRepository.AddElement(element);
                recordsHibernate.Add(element);
            }

            return recordsHibernate;
        }
        /// <summary>
        /// Получить элемент списка
        /// </summary>
        /// <param name="elementsHibernate">Список элементов бд</param>
        /// <param name="indexElement">номер получаемого элемена из списка</param>
        /// <returns></returns>
        private static int GetElementList(List<NodeHibernate> elementsHibernate, long indexElement)
        {
            return elementsHibernate.IndexOf(elementsHibernate.Where(el => el.Id == indexElement).First());
        }
        /// <summary>
        /// Получить запись бд 
        /// </summary>
        /// <param name="record">Запись</param>
        /// <returns>Список</returns>
        private NodeHibernate GetRecordHibernate(NodeHibernate record = null)
        {
            var element = GetRecord(record == null ? null : new Node(record.Adress, record.Port, record.Protocol));


            var el = new NodeHibernate()
            {
                Adress = element.Adress,
                Port = element.Port,
                Protocol = element.Protocol
            };

            if (record != null)
            {
                el.Id = record.Id;
            }

            return el;
        }
        /// <summary>
        /// Получить запись из формы
        /// </summary>
        /// <param name="oldElement">элемент списка</param>
        /// <returns>возврашаемая запись из формы</returns>
        private Node GetRecord(Node oldElement = null)
        {
            RecordForm RecordForm = new RecordForm(oldElement);
            Node element = null;

            if (RecordForm.ShowDialog() == DialogResult.OK)
            {
                element = RecordForm.Record;
            }

            return element;
        }
        /// <summary>
        /// Получить все элементы из бд
        /// </summary>
        /// <returns>список бд</returns>
        internal List<NodeHibernate> GetAllDB()
        {
            return _crudRepository.GetAll();
        }
    }
}
