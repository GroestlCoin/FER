using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrtajMeModel
{
    public abstract class IzlazniObrazac
    {
        protected string _imeObrasca;

        public string Name
        {
            get
            {
                return _imeObrasca;
            }
        }

        public IzlazniObrazac(string name)
        {
            _imeObrasca = name;
        }

        public abstract List<double> sendOutput(string output);

    }
}
