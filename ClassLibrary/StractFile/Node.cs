using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    [Serializable]

    /// <summary>
    ///Класс для записей  
    /// </summary>
    public class Node
    {
        private string _adress;
        private int _port;
        private string _protocol;

        public Node()
        {
        }
        /// <summary>
        /// конструктор запимей
        /// </summary>
        /// <param name="adress">адрес</param>
        /// <param name="port">порт</param>
        /// <param name="protocol">протокол UDP or TCP</param>
        public Node(string adress, int port, string protocol)
        {
            _adress = adress;
            _port = port;
            if (protocol.ToUpper() == "TCP" || protocol.ToUpper() == "UDP") _protocol = protocol;
            else throw new Exception("Ошибка записи, протокол должен быть TCP или UDP");
        }

        public string Adress { get => _adress; set => _adress = value; }
        public int Port { get => _port; set => _port = value; }
        public string Protocol { get => _protocol; set => _protocol = value; }
    }
}
