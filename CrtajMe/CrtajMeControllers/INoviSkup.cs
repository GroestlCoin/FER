using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrtajMeUtil;

namespace CrtajMeControllers
{
    public interface INoviSkup
    {
        bool outerShow();
        string GetSkupName();
        double GetSkupTreshold();
        void ShowErrorMessage(string msg);
        void LoadPrikazTypes(List<string> listType);
        string GetSkupType();
    }
}
