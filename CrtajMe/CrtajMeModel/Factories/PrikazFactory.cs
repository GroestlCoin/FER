using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrtajMeModel.Factories
{
    public class PrikazFactory
    {
        /// <summary>
        /// Factory za Prikaz
        /// </summary>
        /// <param name="name">Ime prikaza</param>
        /// <param name="grade">Ocjena prikaza</param>
        /// <param name="typePrikaz">Tip prikaza</param>
        /// <returns></returns>
        public static Prikaz CreateNewPrikaz(string name, double grade, string typePrikaz)
        {
            object[] arguments = new object[2];
            arguments[0] = name;
            arguments[1] = grade;
            try
            {
                return (Prikaz)Activator.CreateInstance(Type.GetType("CrtajMeModel." + typePrikaz), arguments);
            }
            catch
            {
                throw new CrtajMeException("Prikaz toga tipa ne postiji u sustavu!");
            }
        }
    }
}
