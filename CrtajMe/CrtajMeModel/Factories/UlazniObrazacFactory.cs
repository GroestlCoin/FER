using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CrtajMeModel.Factories
{
    public class UlazniObrazacFactory
    {
        /// <summary>
        /// Factory za ulazne obrasce
        /// </summary>
        /// <param name="fileName">Datoteka u kojoj je zapisan ulazni obrazac</param>
        /// <returns></returns>
        public static UlazniObrazac CreateNewUlazniObrazac(string fileName)
        { 
            try
            {
                List<string> textFile = System.IO.File.ReadAllLines(fileName).ToList<string>();
                
                if (textFile[0] == "InputPattern-Simple")
                    return CreateNewUlazniObrazacIzDatoteke(fileName, textFile);
            }
            catch
            {
                throw new CrtajMeException("Problem s ucitavanjem ulaznog obrasca!");
            }
            throw new CrtajMeException("Problem s ucitavanjem ulaznog obrasca!");
        }

        private static UlazniObrazacIzDatoteke CreateNewUlazniObrazacIzDatoteke(string name, List<string> text)
        {
            string allText = "";
            text.RemoveAt(0);
            foreach(string s in text)
            {
                allText += s + "\n";
            }

            name = name.Split('\\').Last<string>();
            UlazniObrazacIzDatoteke ret = new UlazniObrazacIzDatoteke(name);
            ret.loadText(allText);
            return ret;
        }

    }
}
