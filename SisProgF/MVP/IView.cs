using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisProgF.MVP
{
    public interface IView
    {
        event EventHandler GetAnalyzeEvent;
        event EventHandler ResetAnalyzeEvent;

        event EventHandler GetFunctionEvent;
        event EventHandler ResetFunctionEvent;

        event EventHandler AddElementInFileEvent;
        event EventHandler EditElementInFileEvent;
        event EventHandler DeleteElementInFileEvent;

        event EventHandler CreateFileEvent;
        event EventHandler OpenFileEvent;
        event EventHandler SaveFileEvent;
        event EventHandler SaveAsFileEvent;



        string[] CycleLines { get; set; }
        string ResultCycleAnalyze { get; set; }
        uint First { get; set; }
        uint Second { get; set; }
        uint Result { get; set; }
        int IndexRecord { get; }
        List<Node> Records { get; set; }
        string LogMessage { set; }
    }
}
