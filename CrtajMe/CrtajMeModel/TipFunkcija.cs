using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrtajMeModel
{
    public class TipFunkcija : Prikaz
    {

        private int _mode = 0; //predstavlja oznaku moda (tj. to je trenutno stanje prikaza)
        private double _oldCoordinate; //spremamo zadnju koordinatu
        private Point _oldPoint; //za nastavak crtanja linije koristimo staru tocku

        public TipFunkcija(string name, double grade) : base(name, grade)
        {
        }

        /// <summary>
        /// Metoda sprema poruke koje salje tester, pipeline komunikacija, konstruira se linija
        /// </summary>
        /// <param name="num">Broj tipa double koji se salje</param>
        public override void SaveNumber(double num)
        {
            if (_mode == 0)
            {
                _oldCoordinate = num;
            }
            else if (_mode == 1)
            {
                _oldPoint = new Point(_oldCoordinate, num);
            }
            else if (_mode == 2)
            {
                _oldCoordinate = num;
            }
            else if (_mode == 3)
            { 
                //ubacuje se linija
                _lineList.Add(new Line(_oldPoint.getX, _oldPoint.getY, _oldCoordinate, num));
                _oldPoint = new Point(_oldCoordinate, num);
            }

            if (_mode == 3) 
            {
                _mode = 2;
            }
            else
            {
                _mode += 1;
            }
         }
    }
}
