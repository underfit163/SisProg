using ClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SisProgF.Forms
{/// <summary>
/// Форма для записей
/// </summary>
    public partial class RecordForm : Form
    {
        private Node _record;
        public Node Record { get => _record; }
        public RecordForm(Node element = null)
        {
            InitializeComponent();

            if (element != null)
            {
                FirstTextBox.Text = element.Adress;
                SecondTextBox.Text = element.Port.ToString();
                comboBox1.SelectedItem = element.Protocol;
                _record = element;
            }
            else
            {
                _record = new Node();
            }

        }
   
        private void ButtonOk_Click(object sender, EventArgs e)
        {
            int x = 0;
            _record.Adress = FirstTextBox.Text;
            if (int.TryParse(SecondTextBox.Text, out x))
            { _record.Port = x; }
            else throw new Exception("Ошибка записи, должен быть тип int");
            if (comboBox1.SelectedItem.ToString().ToUpper() == "TCP" || comboBox1.SelectedItem.ToString().ToUpper() == "UDP")
            { _record.Protocol = comboBox1.SelectedItem.ToString(); }
            else throw new Exception("Ошибка записи, протокол должен быть TCP или UDP");

            DialogResult = DialogResult.OK;
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
