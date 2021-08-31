using ClassLibrary.Hibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisProgF.MVPHibernate
{
    class PresenterHibernate
    {
        private IViewHibernate _view;
        private ModelHibernate _model;

        public PresenterHibernate(IViewHibernate view)
        {
            _view = view;
            _model = new ModelHibernate();

            _view.AddElementInDataBaseEvent += new EventHandler(AddElementDB);
            _view.DeleteElementInDataBaseEvent += new EventHandler(DeleteElementDB);
            _view.EditElementInDataBaseEvent += new EventHandler(EditElementDB);
            _view.GetAllDataBaseEvent += new EventHandler(GetAllDB);
        }
      
        private void GetAllDB(object sender, EventArgs e)
        {
            try
            {
                _view.LogMessage = "Загрузка записей из БД";
                List<NodeHibernate> result = _model.GetAllDB();
                _view.RecordsHibernate = result;
                _view.LogMessage = "Записи из БД загружены успешны";
            }
            catch (Exception ex)
            {
                _view.LogMessage = ex.Message;
            }
        }

        private void AddElementDB(object sender, EventArgs e)
        {
            try
            {
                _view.LogMessage = "Добавления элемента в БД";
                List<NodeHibernate> result = _model.AddElementDB(_view.RecordsHibernate);
                _view.RecordsHibernate = result;
                _view.LogMessage = "Элемент в БД добавлен успешно";
            }
            catch (Exception ex)
            {
                _view.LogMessage = ex.Message;
            }
        }

        private void DeleteElementDB(object sender, EventArgs e)
        {
            try
            {
                _view.LogMessage = "Удаление элемента из БД";
                List<NodeHibernate> result = _model.DeleteElementDB(_view.RecordsHibernate, _view.IndexRecordHibernate);
                _view.RecordsHibernate = result;
                _view.LogMessage = "Удаление из БД прошло успешно";
            }
            catch (Exception ex)
            {
                _view.LogMessage = ex.Message;
            }
        }

        private void EditElementDB(object sender, EventArgs e)
        {
            try
            {
                _view.LogMessage = "Редактирование элемента в БД";
                List<NodeHibernate> result = _model.EditElementDB(_view.RecordsHibernate, _view.IndexRecordHibernate);
                _view.RecordsHibernate = result;
                _view.LogMessage = "Редактирование элемента в БД прошло успешно";
            }
            catch (Exception ex)
            {
                _view.LogMessage = ex.Message;
            }
        }
    }
}
