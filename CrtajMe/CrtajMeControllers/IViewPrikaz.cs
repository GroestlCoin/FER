﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrtajMeUtil;

namespace CrtajMeControllers
{
    public interface IViewPrikaz : IObserver
    {
        void ShowView();
        void ShowErrorMessage(string msg);
    }
}
