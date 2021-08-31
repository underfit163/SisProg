using ClassLibrary;
using ClassLibrary.Hibernate;
using SisProgF.MVP;
using SisProgF.MVPHibernate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SisProgF
{
    public partial class MainForm : Form, IView, IViewHibernate
    {
        private int _indexRecord;
        private List<Node> _records;
        private List<NodeHibernate> _recordsHibernate;
        public string[] CycleLines { get => TextBoxCycle.Lines; set => TextBoxCycle.Lines = value; }
        public string ResultCycleAnalyze { get => LabelResultCycle.Text; set => LabelResultCycle.Text = value.Equals("True") ? "1 раз выполнится" : value.Equals("False") ? "Выполнится 0 раз" : "Ошибка"; }
        public uint First { get => (uint)NUDFirst.Value; set => NUDFirst.Value = value; }
        public uint Second { get => (uint)NUDSecond.Value; set => NUDSecond.Value = value; }
        public uint Result { get => uint.Parse(LabelResultFunction.Text); set => LabelResultFunction.Text = value.ToString(); }

        public int IndexRecord => _indexRecord;

        public List<Node> Records
        {
            get => _records;
            set
            {
                DGVFile.Rows.Clear();
                _records = value;
                int i = 0;
                value.ForEach((el) => DGVFile.Rows.Add(i++, el.Adress, el.Port, el.Protocol));
            }
        }
        

        public string LogMessage
        {
            set
            {
                TextBoxLogMessage.Lines = TextBoxLogMessage.Lines.ToList().Append($"[{DateTime.Now.ToLongTimeString()}]: {value}").ToArray();
                TextBoxLogMessage.SelectionStart = TextBoxLogMessage.Text.Length;
                TextBoxLogMessage.ScrollToCaret();
            }
        }

        public long IndexRecordHibernate => _indexRecord;

        public List<NodeHibernate> RecordsHibernate
        {
            get => _recordsHibernate;
            set
            {
                DGVDataBase.Rows.Clear();
                _recordsHibernate = value;
                value.ForEach((el) => DGVDataBase.Rows.Add(el.Id, el.Adress, el.Port, el.Protocol));
            }
        }

        public MainForm()
        {
            InitializeComponent();
            LabelResultCycle.Text = LabelResultFunction.Text = String.Empty;
        }

        public event EventHandler GetAnalyzeEvent;
        public event EventHandler ResetAnalyzeEvent;
        
        public event EventHandler GetFunctionEvent;
        public event EventHandler ResetFunctionEvent;
        
        public event EventHandler AddElementInFileEvent;
        public event EventHandler EditElementInFileEvent;
        public event EventHandler DeleteElementInFileEvent;
        public event EventHandler CreateFileEvent;
        public event EventHandler OpenFileEvent;
        public event EventHandler SaveFileEvent;
        public event EventHandler SaveAsFileEvent;
        
        public event EventHandler GetAllDataBaseEvent;
        public event EventHandler AddElementInDataBaseEvent;
        public event EventHandler EditElementInDataBaseEvent;
        public event EventHandler DeleteElementInDataBaseEvent;

           internal void GetAll()
           {
               GetAllDataBaseEvent?.Invoke(null, null);
           }

        private void MenuStripCreateFile_Click(object sender, EventArgs e)
        {
            CreateFileEvent?.Invoke(null, null);
            tabControl1.SelectedIndex = 2;
        }

        private void MenuStripOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileEvent?.Invoke(null, null);
            tabControl1.SelectedIndex = 2;
        }

        private void MenuStripSaveFile_Click(object sender, EventArgs e)
        {
            SaveFileEvent?.Invoke(null, null);
        }

        private void MenuStripSaveAsFile_Click(object sender, EventArgs e)
        {
            SaveAsFileEvent?.Invoke(null, null);
        }

        private void MenuStripAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Тышкун Андрей. 6301. 12 Вариант");
        }

        private void MenuStripExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MenuStripDataBase_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3;
        }

        private void ButtonClearCycle_Click(object sender, EventArgs e)
        {
            ResetAnalyzeEvent?.Invoke(null, null);
        }


        private void ButtonAddFile_Click(object sender, EventArgs e)
        {
            AddElementInFileEvent?.Invoke(null, null);
        }

        private void ButtonResultCycle_Click(object sender, EventArgs e)
        {
            LabelResultCycle.Text=null;
            GetAnalyzeEvent?.Invoke(null, null);

        }


        private void DrawMenuDGV(MouseEventArgs e, EventHandler EditElement, EventHandler DeleteElement,
           DataGridView dataGridView)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(
                    new MenuItem(
                        "Изменить",
                        EditElement
                    )
                );
                m.MenuItems.Add(
                    new MenuItem(
                        "Удалить",
                        DeleteElement
                    )
                );
                try
                {
                    var _point = new Point(e.X, e.Y);

                    _indexRecord = int.Parse(dataGridView.Rows[dataGridView.HitTest(_point.X, _point.Y).RowIndex].Cells[0]
                        .Value.ToString());

                    m.Show(dataGridView, _point);
                }
                catch
                {

                }
            }
        }

        private void DGVFile_MouseClick(object sender, MouseEventArgs e)
        {
            DrawMenuDGV(e, EditElementInFileEvent, DeleteElementInFileEvent, DGVFile);
        }

        private void ButtonResultFunction_Click_1(object sender, EventArgs e)
        {
            GetFunctionEvent?.Invoke(null, null);
        }
        private void ButtonClearFunction_Click(object sender, EventArgs e)
        {
            ResetFunctionEvent?.Invoke(null, null);
        }
       
 /*
 * БД
 */
        private void DGVDataBase_MouseClick(object sender, MouseEventArgs e)
        {
            DrawMenuDGV(e, EditElementInDataBaseEvent, DeleteElementInDataBaseEvent, DGVDataBase);
        }

        private void ButtonAddBD_Click(object sender, EventArgs e)
        {
            AddElementInDataBaseEvent?.Invoke(null, null);
        }

   
    }
}
