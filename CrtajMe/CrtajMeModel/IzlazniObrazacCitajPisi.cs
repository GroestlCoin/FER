using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrtajMeModel
{
    public class IzlazniObrazacCitajPisi : IzlazniObrazac
    {
        public IzlazniObrazacCitajPisi(string name) : base(name)
        { }

        /// <summary>
        /// Jednostavna metoda koja izlaz samo posalje kao broj double
        /// Ideja je da postoje i kompleksnije metode koje parsiraju izlaz prema potrebi i zatim vrate listu brojeva
        /// koji se koriste za prikaz
        /// </summary>
        /// <param name="output"></param>
        /// <returns>Lista brojeva</returns>
        public override List<double> sendOutput(string output)
        {
            List<double> ret = new List<double>();
            try
            {
                ret.Add(Convert.ToDouble(output));
            }
            catch
            {
                throw new CrtajMeException("Izlazni obrazac ne parsira dobro!");
            }
            
            return ret;
        }
    }
}
