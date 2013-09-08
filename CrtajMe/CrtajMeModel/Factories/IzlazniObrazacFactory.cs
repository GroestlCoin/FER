using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrtajMeModel.Factories
{
    public class IzlazniObrazacFactory
    {
        /// <summary>
        /// Factory za izlazne obrasce
        /// </summary>
        /// <param name="fileName">Datoteka u kojoj je zapisan izlazni obrazac</param>
        /// <returns></returns>
        public static IzlazniObrazac CreateNewIzlazniObrazac(string fileName)
        {
            try
            {
                List<string> textFile = System.IO.File.ReadAllLines(fileName).ToList<string>();
                if (textFile[0] == "OutputPattern-Simple")
                    return new IzlazniObrazacCitajPisi(fileName.Split('\\').Last<string>());
            }
            catch
            {
                throw new CrtajMeException("Problem s ucitavanjem izlaznog obrasca!");
            }
            throw new CrtajMeException("Problem s ucitavanjem izlaznog obrasca!");
        }
    }
}