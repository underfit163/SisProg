using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisProgF.Forms;
using System.Windows.Forms;

namespace SisProgF.MVP
{
    
    class Presenter
    {
        private IView _view;
        private Model _model;

        public Presenter(IView view)
        {
            _view = view;
            _model = new Model();

            _view.GetAnalyzeEvent += new EventHandler(GetAnalyzerResult);
            _view.ResetAnalyzeEvent += new EventHandler(ResetAnalyzer);

            _view.GetFunctionEvent += new EventHandler(GetAsmResult);
            _view.ResetFunctionEvent += new EventHandler(ResetAsm);

            _view.AddElementInFileEvent += new EventHandler(AddElementFile);
            _view.DeleteElementInFileEvent += new EventHandler(DeleteElementFile);
            _view.EditElementInFileEvent += new EventHandler(EditElementFile);

            _view.CreateFileEvent += new EventHandler(CreateFile);
            _view.OpenFileEvent += new EventHandler(OpenFile);
            _view.SaveFileEvent += new EventHandler(SaveFile);
            _view.SaveAsFileEvent += new EventHandler(SaveAsFile);
        }

        private void GetAnalyzerResult(object sender, EventArgs e)
        {
            try
            {
                _view.LogMessage = "Начало анализа цикла";
                string result = _model.GetResulAnalyze(_view.CycleLines);
                _view.LogMessage = $"Анализ цикла прошёл успешно";
                _view.ResultCycleAnalyze = result;
            }
            catch (Exception ex)
            {
                _view.LogMessage = ex.Message;
                _view.ResultCycleAnalyze = "Ошибка";
            }
        }

        private void ResetAnalyzer(object sender, EventArgs e)
        {
            try
            {
                _view.LogMessage = "Начало очистки полей анализа.";
                var cycle = _view.CycleLines;
                var result = _view.ResultCycleAnalyze;
                _model.ClearFieldAnalyze(ref cycle, ref result);
                _view.CycleLines = cycle;
                _view.ResultCycleAnalyze = result;
                _view.LogMessage = "Поля цикла очищены успешно";
            }
            catch (Exception ex)
            {
                _view.LogMessage = ex.Message;
            }
        }

        private void GetAsmResult(object sender, EventArgs e)
        {
            try
            {
                _view.LogMessage = "Начало низкоуровневой функции";
                uint result = _model.GetResult(_view.First, _view.Second);
                _view.Result = result;
                _view.LogMessage = "Низкоуровневая функция прошла успешно";
            }
            catch (Exception ex)
            {
                _view.LogMessage = ex.Message;
            }
        }

        private void ResetAsm(object sender, EventArgs e)
        {
            try
            {
                _view.LogMessage = "Очистка полей функции";
                var first = _view.First;
                var second = _view.Second;
                var result = _view.Result;
                _model.ResetFunction(ref first, ref second, ref result);
                _view.First = first;
                _view.Second = second;
                _view.Result = result;
                _view.LogMessage = "Очистка полей функции прошла успешно";
            }
            catch (Exception ex)
            {
                _view.LogMessage = ex.Message;
            }
        }
        private void AddElementFile(object sender, EventArgs e)
        {
            try
            {
                _view.LogMessage = "Добавления элемента в файл";
                var record = GetRecord();
                List<Node> result = _model.AddElementFile(record);
                _view.Records = result;
                _view.LogMessage = "Элемент в файл добавлен успешно";
            }
            catch (Exception ex)
            {
                _view.LogMessage = ex.Message;
            }
        }
        private void DeleteElementFile(object sender, EventArgs e)
        {
            try
            {
                _view.LogMessage = "Удаление элемента из файла";
                List<Node> result = _model.DeleteElementFile(_view.IndexRecord);
                _view.Records = result;
                _view.LogMessage = "Удаление из файла прошло успешно";
            }
            catch (Exception ex)
            {
                _view.LogMessage = ex.Message;
            }
        }
        private void EditElementFile(object sender, EventArgs e)
        {
            try
            {
                _view.LogMessage = "Редактирование элемента в файла";
                var record = GetRecord(_view.Records[_view.IndexRecord]);
                List<Node> result = _model.EditElementFile(record, _view.IndexRecord);
                _view.Records = result;
                _view.LogMessage = "Редактирование элемента в файле прошло успешно";
            }
            catch (Exception ex)
            {
                _view.LogMessage = ex.Message;
            }
        }
        private void CreateFile(object sender, EventArgs e)
        {
            try
            {
                _view.LogMessage = "Начало создание файла";
                List<Node> result = _model.CreateFile();
                _view.Records = result;
                _view.LogMessage = "Файл создан успешно";
            }
            catch (Exception ex)
            {
                _view.LogMessage = ex.Message;
            }
        }

        private void OpenFile(object sender, EventArgs e)
        {
            try
            {
                _view.LogMessage = "Начало открытия файла";
                List<Node> result = _model.OpenFile();
                _view.Records = result;
                _view.LogMessage = "Файл открыт успешно";
            }
            catch (Exception ex)
            {
                _view.LogMessage = ex.Message;
            }
        }
        private void SaveFile(object sender, EventArgs e)
        {
            try
            {
                _view.LogMessage = "Начало сохранения файла";
                _model.SaveFile();
                _view.LogMessage = "Сохранение файла прошло успешно";
            }
            catch (Exception ex)
            {
                _view.LogMessage = ex.Message;
            }
        }

        private void SaveAsFile(object sender, EventArgs e)
        {
            try
            {
                _view.LogMessage = "Начало сохранения файла";
                _model.SaveAsFile();
                _view.LogMessage = "Сохранение файла прошло успешно";
            }
            catch (Exception ex)
            {
                _view.LogMessage = ex.Message;
            }
        }

        /// <summary>
        /// Вспомогательный метод, для добавления и изменения записи
        /// </summary>
        /// <param name="oldElement">Номер элемента</param>
        /// <returns>запись</returns>
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
    }
}
