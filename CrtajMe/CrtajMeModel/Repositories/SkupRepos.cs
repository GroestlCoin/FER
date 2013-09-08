using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrtajMeUtil;

namespace CrtajMeModel
{
    public class SkupRepos :  Subject
    {
        private List<Skup> _skupList = new List<Skup>();

        private static SkupRepos _instance = null;

        private SkupRepos()
        { 
        
        }

        /// <summary>
        /// Instanca metoda za singleton razred
        /// </summary>
        /// <returns>Instanca razrada</returns>
        public static SkupRepos Instance()
        {
            if (_instance == null)
                _instance = new SkupRepos();
            return _instance;
        }

        /// <summary>
        /// Dohvaca se broj skupova u repozitoriju
        /// </summary>
        /// <returns>Broj skupova</returns>
        public int Count()
        {
            return _skupList.Count();
        }

        /// <summary>
        /// Dohvaca se skup pomocu njegovog imena, ako ga nema baca se iznimka
        /// </summary>
        /// <param name="name">Ime skupa</param>
        /// <returns>Objekt razreda Skup</returns>
        public Skup GetSkupByName(string name)
        {
            foreach (Skup s in _skupList)
            {
                if (s.Name == name) return s;
            }

            throw new CrtajMeException("Skup ne postoji!");
        }


        /// <summary>
        /// U repozitorij se ubacuje skup, ako postoji neki takvog imena baca se iznimka
        /// </summary>
        /// <param name="noviSkup">Objekt razreda skup</param>
        public void AddSkup(Skup newSkup)
        {
            if (newSkup.Name == "")
                throw new CrtajMeException("Ime skupa je prazno!");

            foreach (Skup s in _skupList)
            {
                if (s.Name == newSkup.Name)
                    throw new CrtajMeException("Skup s tim imenom vec postoji!");
            }

            _skupList.Add(newSkup);
            notifyObservers();
        }

        /// <summary>
        /// Brise se prikaz iz svih skupova
        /// </summary>
        /// <param name="p">Prikaz koji se brise</param>
        public void RemovePrikazInSkup(Prikaz p)
        {
            foreach (Skup s in _skupList)
            {
                if ( s.GetListNameOfPrikaz().Contains(p.Name) )
                    s.DeletePrikaz(p);
            }
        }

        /// <summary>
        /// Brise se objekt razreda Skup iz repozitorija
        /// </summary>
        /// <param name="s">String ime skupa</param>
        public void RemoveSkup(string name)
        {
            foreach (Skup s in _skupList)
            {
                if (s.Name == name)
                {
                    _skupList.Remove(s);
                    notifyObservers();
                    return;
                }
            }
            
            throw new CrtajMeException("Skup ne postoji i ne moze se obrisati!");
        }

        /// <summary>
        /// Dohvacaju se svi skupovi u obliku liste imena
        /// </summary>
        /// <returns>Lista imena skupova</returns>
        public List<string> ListSkupoviByName()
        {
            List<string> ret = new List<string>();
            foreach (Skup s in _skupList)
            {
                ret.Add(s.Name);
            }
            return ret;
        }
    }
}
