using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrtajMeUtil;

namespace CrtajMeModel
{
    public class UlazniObrazacRepos : Subject
    {
        private List<UlazniObrazac> _listObrazac = new List<UlazniObrazac>();
        private static UlazniObrazacRepos _instance = null;

        /// <summary>
        /// Vraca imena obrazaca u obliku string liste
        /// </summary>
        /// <returns>Lista imena</returns>
        public List<string> List()
        {
            List<string> stringList = new List<string>();
            foreach (UlazniObrazac u in _listObrazac)
            {
                stringList.Add(u.Name);
            }
            return stringList;
        }

        /// <summary>
        /// Instanca metoda za singleton razred
        /// </summary>
        /// <returns>Instanca razrada</returns>
        public static UlazniObrazacRepos Instance()
        {
            if (_instance == null)
                _instance = new UlazniObrazacRepos();
            return _instance;
        }

        /// <summary>
        /// Ubacivanje u repozitorij
        /// </summary>
        /// <param name="obrazac">Ulaniz obrazac</param>
        public void Add(UlazniObrazac obrazac)
        {
            foreach (UlazniObrazac o in _listObrazac)
            { 
                if (o.Name == obrazac.Name)
                    throw new CrtajMeException("Obrazac je vec ucitan! (mozda se imena datoteka poklapaju)");
            }
            
            _listObrazac.Add(obrazac);
            notifyObservers();
        }

        /// <summary>
        /// Dohvacanje obrasca po imenu
        /// </summary>
        /// <param name="name">Ime obrasca</param>
        /// <returns>Objekt ulaznog obrasca</returns>
        public UlazniObrazac GetObrazac(string name)
        {
            foreach (UlazniObrazac o in _listObrazac)
            {
                if (o.Name == name)
                    return o;
            }
            throw new CrtajMeException("Obrazac ne postoji!");
        }

    }
}
