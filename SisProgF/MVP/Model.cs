using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SisProgF.Forms;
using System.Windows.Forms;
using ClassLibrary.CycleAnalyz;
using System.IO;

namespace SisProgF.MVP
{
    public class Model
    {
        private object _instance;
        private MethodInfo _method;
        private FileServic _fileServic;

        public Model()
        {
            _fileServic = new FileServic();
            string functionName = "Asm";
            //указываем сборку
            Assembly asm = Assembly.Load(File.ReadAllBytes($"{functionName}.dll"));
            //получаем класс сборки
            Type t = asm.GetType($"{functionName}DLL");
            //получаем метод
            _method = t.GetMethod(functionName, BindingFlags.Instance | BindingFlags.Public);
            //создаем объект класса 
            _instance = Activator.CreateInstance(t);
        }


        /// <summary>
        /// Сохранение файла как...
        /// </summary>
        internal void SaveAsFile()
        {
            _fileServic.SaveFileAs();
        }
        /// <summary>
        /// Результат анализа цикла
        /// </summary>
        /// <param name="cycleLines">анализируеммый цикл</param>
        /// <returns></returns>
        internal string GetResulAnalyze(string[] cycleLines)
        {
            return Analyzer.Analiz(cycleLines);
        }
        /// <summary>
        /// сохранение файла
        /// </summary>
        internal void SaveFile()
        {
            _fileServic.SaveFile();
        }
        /// <summary>
        /// открытие файла
        /// </summary>
        /// <returns>список</returns>
        internal List<Node> OpenFile()
        {
            return _fileServic.OpenFile();
        }
        /// <summary>
        /// создание файла
        /// </summary>
        /// <returns>список</returns>
        internal List<Node> CreateFile()
        {
            return _fileServic.CreateFile();
        }
        /// <summary>
        /// Удаляет элемент с определённым номером из структуры данных
        /// </summary>
        /// <param name="indexRecord">Позиция удаления</param>
        /// <returns>Список</returns>
        internal List<Node> DeleteElementFile(int indexRecord)
        {
            return _fileServic.DeleteAtElement(indexRecord);
        }
        /// <summary>
        /// Очищет поля анализа цикла и задает новый массив строк для цикла 
        /// </summary>
        /// <param name="cycle">цикл в массив строк</param>
        /// <param name="result">очищаемый результат</param>
        internal void ClearFieldAnalyze(ref string[] cycle, ref string result)
        {
            cycle = new string[cycle.Length];
            result = String.Empty;
        }
        /// <summary>
        /// Изменяет значения элемента с определённым индексом
        /// </summary>
        /// <param name="record">список изменения</param>
        /// <param name="indexRecord">позиция элемента изменения</param>
        /// <returns>измененный список</returns>
        internal List<Node> EditElementFile(Node record, int indexRecord)
        {

            return _fileServic.EditAtElement(record, indexRecord);
        }
        /// <summary>
        /// Метод добавляет новый элемент в структуру данных
        /// </summary>
        /// <param name="record">Список</param>
        /// <returns>Возвращает список</returns>
        internal List<Node> AddElementFile(Node record)
        {

            return _fileServic.AddElement(record);
        }
        /// <summary>
        /// обнуление значений ассемблерной функции 
        /// </summary>
        /// <param name="first">первое значение</param>
        /// <param name="second">второе значение</param>
        /// <param name="result">результат функции</param>
        internal void ResetFunction(ref uint first, ref uint second, ref uint result)
        {
            first = second = result = 0;
        }
        /// <summary>
        /// Вызывает метод вычисления исключаещего или
        /// </summary>
        /// <param name="first">первое знчение</param>
        /// <param name="second">второе значение</param>
        /// <returns>результат</returns>
        internal uint GetResult(uint first, uint second)
        {
            return (uint)_method.Invoke(_instance, new object[] { first, second });
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
