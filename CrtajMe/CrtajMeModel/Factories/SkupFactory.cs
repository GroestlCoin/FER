using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrtajMeModel.Factories
{
    public class SkupFactory
    {
        /// <summary>
        /// Factory za skup
        /// </summary>
        /// <param name="name">Ime skupa</param>
        /// <param name="typePrikaz">Tip prikaza</param>
        /// <param name="grade">Granica</param>
        /// <returns>Objekt razreda Skup</returns>
        public static Skup CreateNewSkup(string name, string typePrikaz, double trashold)
        {
            return new Skup(name, typePrikaz, trashold);
        }
    }
}
