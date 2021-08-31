using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ClassLibrary
{
   
    /// <summary>
    /// Класс, который предназначен для выполнения основных операций с файлами.
    /// </summary>
    public class FileServic
    {
        private static string _typeFile = "txt";
        private string _type = $"{_typeFile} files (*.{_typeFile})|*.{_typeFile}";
        private string _way = String.Empty;
        private List<Node> dataList;
        public List<Node> List { get => dataList; set => dataList = value; }
        
        public FileServic()
        {
        }
        /// <summary>
        /// Метод для открытия файла
        /// </summary>
        /// <returns>запись</returns>
        public List<Node> OpenFile()
        {
            dataList = new List<Node>();

            var fileDialog = new OpenFileDialog
            {
                Filter = _type
            };

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                _way = fileDialog.FileName;
                var fileStream = fileDialog.OpenFile();
                if (fileStream.Length != 0)
                {
                    using (var stremReader = new StreamReader(fileStream))//освобождение неуправляемых ресурсов
                    {
                        var count = int.Parse(stremReader.ReadLine());
                        for (int i = 0; i < count; i++)
                        {
                            dataList.Add(new Node(stremReader.ReadLine(), int.Parse(stremReader.ReadLine()), stremReader.ReadLine()));
                        }
                    }
                }
                fileStream.Close();
            }

            return dataList;
        }
        /// <summary>
        /// Создание файла
        /// </summary>
        /// <returns>запись или ничего</returns>
        public List<Node> CreateFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = _type
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var fileStream = File.Create(saveFileDialog.FileName);
                _way = saveFileDialog.FileName;
                fileStream.Close();
                return new List<Node>();
            }
            return null;
        }
        /// <summary>
        /// Сохранение файла
        /// </summary>
        /// <param name="way">путь файла</param>
        public void SaveFile(string way = null)
        {
            var saveFileDialog = new SaveFileDialog
            {
                FileName = way == null ? _way : way
            };
            var stream = saveFileDialog.OpenFile();

            using (var streamWriter = new StreamWriter(stream))
            {
                streamWriter.WriteLine(dataList.Count);
                dataList.ForEach((el) =>
                {
                    if (el.Protocol.ToUpper() == "TCP" || el.Protocol.ToUpper() == "UDP")
                    {
                        streamWriter.WriteLine(el.Adress);
                        streamWriter.WriteLine(el.Port);
                        streamWriter.WriteLine(el.Protocol);
                    }
                    else throw new Exception("Ошибка записи, протокол должен быть TCP или UDP");
                });
            }
            stream.Close();
        }
        /// <summary>
        /// Сохранить файл как...
        /// </summary>
        public void SaveFileAs()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = _type
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveFile(saveFileDialog.FileName);
            }
        }
        /// <summary>
        /// Метод добавляет элемент
        /// </summary>
        /// <param name="element">элемент</param>
        /// <returns>список</returns>
        public List<Node> AddElement(Node element)
        {
            if (element != null)
            {
                dataList.Add(element);
            }
            return dataList;
        }
        /// <summary>
        /// Удаляет элемент
        /// </summary>
        /// <param name="index">номер элемента</param>
        /// <returns>список</returns>
        public List<Node> DeleteAtElement(int index)
        {
            dataList.RemoveAt(index);

            return dataList;
        }
        /// <summary>
        /// Изменяет элемент
        /// </summary>
        /// <param name="element">изменяемый элемент</param>
        /// <param name="index">номер элемента</param>
        /// <returns>список</returns>
        public List<Node> EditAtElement(Node element, int index)
        {
            if (element != null)
            {
                dataList[index] = element;
            }
            return dataList;
        }

    }
}
