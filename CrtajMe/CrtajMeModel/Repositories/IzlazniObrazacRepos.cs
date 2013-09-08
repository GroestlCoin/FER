using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrtajMeUtil;


namespace CrtajMeModel
{
    public class IzlazniObrazacRepos : Subject
    {
        private List<IzlazniObrazac> _listObrazac = new List<IzlazniObrazac>();

        private static IzlazniObrazacRepos _instance = null;

        /// <summary>
        /// Vraca imena obrazaca u obliku string liste
        /// </summary>
        /// <returns>Lista imena</returns>
        public List<string> List()
        {
            List<string> stringList = new List<string>();
            foreach (IzlazniObrazac u in _listObrazac)
            {
                stringList.Add(u.Name);
            }
            return stringList;
        }

        /// <summary>
        /// Instanca metoda za singleton razred
        /// </summary>
        /// <returns>Instanca razrada</returns>
        public static IzlazniObrazacRepos Instance()
        {
            if (_instance == null)
                _instance = new IzlazniObrazacRepos();
            return _instance;
        }

        /// <summary>
        /// Ubacivanje u repozitorij
        /// </summary>
        /// <param name="obrazac">Ulaniz obrazac</param>
        public void Add(IzlazniObrazac obrazac)
        {
            foreach (IzlazniObrazac o in _listObrazac)
            {
                if (o.Name == obrazac.Name)
                    throw new CrtajMeException("Obrazac je vec ucitan! (mozda se imena datoteka poklapaju)");
            }

            _listObrazac.Add(obrazac);
            notifyObservers();
        }

        /// <summary>
        /// Dohvacanje obrasca
        /// </summary>
        /// <param name="name">Ime obrasca</param>
        /// <returns>Objekt izlaznog obrasca</returns>
        public IzlazniObrazac GetObrazac(string name)
        {
            foreach (IzlazniObrazac o in _listObrazac)
            {
                if (o.Name == name)
                    return o;
            }
            throw new CrtajMeException("Obrazac ne postoji!");
        }

    }
}
