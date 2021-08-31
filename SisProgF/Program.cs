using SisProgF.MVP;
using SisProgF.MVPHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SisProgF
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm view = new MainForm();
            var pr = new Presenter(view);
            var pr1 = new PresenterHibernate(view);
            view.GetAll();
            Application.Run(view);
        }
    }
}
