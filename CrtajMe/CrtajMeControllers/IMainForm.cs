using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrtajMeUtil;

namespace CrtajMeControllers
{
    public interface IMainForm : IObserver
    {
        void ShowErrorMessage(string msg);
    }
}
