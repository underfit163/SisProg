using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.CycleAnalyz
{
    /// <summary>
    /// Класс анализа цикла
    /// </summary>
    public static class Analyzer
    {/// <summary>
     /// Формируем строку цикла
     /// </summary>
     /// <param name="cycles">Анализируемый цикл</param>
     /// <returns>возвращаем цикл ввиде строки</returns>
        private static string ListCode(string[] cycles)
        {
            StringBuilder str = new StringBuilder(string.Empty);
            foreach (var el in cycles) str.Append(el);

            string cycle = str.ToString();
            int leftScob = cycle.IndexOf('(');
            int rightScob = cycle.IndexOf(')');
            if (rightScob == -1 || leftScob == -1)
            {
                throw new Exception("Ошибка построения конструкции");
            }
            var s = string.Format("using System; namespace Test\n {{ class TestClass{{static void Main(string[] args){{ {0} }}}}}}",
                $"bool temp=true;{cycle.Substring(0, cycle.IndexOf("while"))} if {cycle.Substring(leftScob, rightScob + 1 - leftScob) } temp = true;else temp=false; Console.WriteLine(temp.ToString());");

            return s;

        }
        /// <summary>
        /// Анализируем строку с помошью программного компилятора
        /// </summary>
        /// <param name="cycle">Анализируемый цикл</param>
        /// <returns>возвращаем получаемое значение</returns>
        public static string Analiz(string[] cycle)
        {
            // Задаём язык
            CodeDomProvider codeProvider = CodeDomProvider.CreateProvider("CSharp");//доступ к компиляции
            // Имя файла
            string analiz = "analizer.exe";
            CompilerParameters parameters = new CompilerParameters//параметры компиляции
            {
                GenerateExecutable = true,//создавать ли исполняемый файл
                OutputAssembly = analiz//задаем имя сборки
            };


            // создаём exeFile
            CompilerResults results = codeProvider.CompileAssemblyFromSource(parameters, ListCode(cycle));
            if (results.Errors.Count > 0)
            {
                // Возвразаем ошибку
                foreach (CompilerError CompErr in results.Errors)
                    throw new Exception(CompErr.ErrorText);
                return results.Errors[0].ErrorText;
            }
            else
            {
                Process process = new Process();

                // Задаём стартовые параметры 
                process.StartInfo.FileName = analiz;
                process.StartInfo.RedirectStandardOutput = true;//записывает текстовые данные в стандартный поток
                process.StartInfo.CreateNoWindow = false;
                process.StartInfo.UseShellExecute = false;

                // В StringBuilder будем добавлять полученные данные
                var sb = new StringBuilder(string.Empty);
                // Запускаем процесс
                process.Start();
                while (!process.StandardOutput.EndOfStream)
                {
                    sb.Append(process.StandardOutput.ReadLine());
                }
                process.Close();

                return sb.ToString();
            }

        }
    }
}
