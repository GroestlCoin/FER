using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrtajMeUtil;

namespace CrtajMeModel
{
    public class PrikazRepos : Subject
    {
        private List<Prikaz> _listaPrikaz = new List<Prikaz>();
        private List<string> _listaTipova = new List<string>();

        private static PrikazRepos _instance = null;

        /// <summary>
        /// Stavi prikaz u repozitorij
        /// </summary>
        /// <param name="newPrikaz">Objekt tipa Prikaz</param>
        public void AddPrikaz(Prikaz newPrikaz)
        {
            _listaPrikaz.Add(newPrikaz);
        }


        /// <summary>
        /// Dohvacanje Prikaza pomocu imena
        /// </summary>
        /// <param name="name">Ime prikaza</param>
        /// <returns>Objekt razreda prikaz</returns>
        public Prikaz GetPrikazByName(string name)
        {
            foreach (Prikaz p in _listaPrikaz)
            {
                if (p.Name == name) return p;
            }

            throw new CrtajMeException("Prikaz ne postoji!");
        }

        /// <summary>
        /// Dohvacanje svih prikaza u obliku liste imena
        /// </summary>
        /// <returns>Lista imena prikaza</returns>
        public List<string> ListPrikazByName()
        {
            List<string> ret = new List<string>();

            foreach (Prikaz p in _listaPrikaz)
            {
                ret.Add(p.Name);
            }
            return ret;
        }

        /// <summary>
        /// Izbaci prikaz iz repozitorija, izbacuje se i iz svih skupova
        /// </summary>
        /// <param name="name">Ime prikaza</param>
        public void RemovePrikaz(string name)
        {
            foreach (Prikaz p in _listaPrikaz)
            {
                if (p.Name == name)
                {
                    _listaPrikaz.Remove(p);
                    SkupRepos.Instance().RemovePrikazInSkup(p);
                    notifyObservers();
                    return;
                }
            }
            throw new CrtajMeException("Prikaz koji se brise ne postoji u sustavu!");
        }

        /// <summary>
        /// Skupljaju se tipovi Prikaza koji su na sustavu
        /// </summary>
        private PrikazRepos()
        {
            Type[] types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();
            foreach (Type type in types)
            {
                if (type.IsSubclassOf(typeof(Prikaz)))
                {
                    _listaTipova.Add(type.ToString().Split('.')[1]);
                }
            }
        }

        /// <summary>
        /// Dohvacanje svih tipova koji su na sustavu
        /// </summary>
        /// <returns>Lista stringova gdje su imena tipova prikaza</returns>
        public List<string> GetListaTipova()
        {
            return _listaTipova;
        }

        /// <summary>
        /// Instanca metoda za singleton razred
        /// </summary>
        /// <returns>Instanca razrada</returns>
        public static PrikazRepos Instance()
        {
            if (_instance == null)
                _instance = new PrikazRepos();
            return _instance;
        }

    }
}
