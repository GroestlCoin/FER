using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrtajMeModel
{
    public abstract class UlazniObrazac
    {
        protected string _sadrzajObrasca;
        protected string _imeObrasca;

        public string Name
        {
            get
            {
                return _imeObrasca;
            }
        }

        public UlazniObrazac(string name)
        {
            _imeObrasca = name;
        }

        /// <summary>
        /// Ucitava se tekst iz datoteke koji sluzi kao opis
        /// </summary>
        /// <param name="inputText"></param>
        public void loadText(string inputText)
        {
            _sadrzajObrasca = inputText;
        }

        public abstract string generateInput();
    }
}
