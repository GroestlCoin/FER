using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrtajMeModel
{
    public class CrtajMeException : Exception
    {
        private string _msg;
        public CrtajMeException(string msg)
        {
            _msg = msg;
        }
        public string getMsg()
        {
            return _msg;
        }
    }
}
