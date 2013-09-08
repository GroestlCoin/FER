using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrtajMeControllers
{
    public interface IVisualView
    {
        void DrawPoint(CrtajMeModel.Point P);
        void DrawLine(CrtajMeModel.Line L);
        void ShowView(string prikazName);
        void ShowErrorMessage(string msg);
    }
}
