using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrtajMeModel
{
    public class Skup
    {
        private List<Prikaz> _listaPrikaza = new List<Prikaz>();

        private string _imeSkupa;
        private double _ocjenaSkupa;
        private string _tipSkupa;
        private double _granica;
        private bool _prihvatljivost = false;

        public double AverageGrade
        {
            get
            {
                SetAverageGrade();
                return _ocjenaSkupa;
            }
        }

        public string Name
        {
            get 
            {
                return _imeSkupa;
            }
        }

        public string Type
        {
            get
            {
                return _tipSkupa;
            }
        }

        public double Treshold
        {
            get
            {
                return _granica;
            }
        }

        public bool Accept
        {
            get 
            {
                RefreshAccept();
                return _prihvatljivost;
            }
        }

        public Skup(string name, string typePrikaz, double treshold)
        {
            _imeSkupa = name;
            _tipSkupa = typePrikaz;
            _granica = treshold;
        }

        /// <summary>
        /// Metoda koja racuna prosjek ocjena u skupu
        /// </summary>
        private void SetAverageGrade()
        {
            double sum = 0;
            foreach (Prikaz p in _listaPrikaza)
            {
                if (!p.isOcjenaSet)
                    throw new CrtajMeException("Ocjena za jedan od prikaza nije postavljena!");
                sum += p.Grade;
            }
            
            if (_listaPrikaza.Count == 0) 
                _ocjenaSkupa = 0;
            else
                _ocjenaSkupa = sum / _listaPrikaza.Count;
        }

        /// <summary>
        /// Metoda koja ubacuje prikaz u skup
        /// </summary>
        public void AddPrikaz(Prikaz newPrikaz)
        {
            if (newPrikaz.Type != _tipSkupa)
                throw new CrtajMeException("Tip prikaza nije dobar, potreban je: "+_tipSkupa);
            if (_listaPrikaza.Contains(newPrikaz))
                throw new CrtajMeException("Prikaz je vec u tome skupu!");
            
            _listaPrikaza.Add(newPrikaz);
        }


        /// <summary>
        /// Dohvacaju se imena prikaza koji su u skupu
        /// </summary>
        /// <returns>Lista prikaza</returns>
        public List<string> GetListNameOfPrikaz()
        {
            List<string> ret = new List<string>();
            foreach (Prikaz p in _listaPrikaza)
            {
                ret.Add(p.Name);
            }
            return ret;
        }

        private void RefreshAccept()
        {
            if (_ocjenaSkupa >= _granica)
                _prihvatljivost = true;
            else
                _prihvatljivost = false;
        }

        /// <summary>
        /// Metoda koja brise prikaz iz skupa
        /// </summary>
        /// <param name="oldPrikaz">Prikaz koji se zeli obrisati</param>
        public void DeletePrikaz(Prikaz oldPrikaz)
        {
            if (!_listaPrikaza.Contains(oldPrikaz))
                throw new CrtajMeException("Skup ne sadrzi prikaz koji se brise!");
            _listaPrikaza.Remove(oldPrikaz);
            SetAverageGrade();
            RefreshAccept();
        }

    }
}
