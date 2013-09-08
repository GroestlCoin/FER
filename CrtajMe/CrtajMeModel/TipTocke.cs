using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CrtajMeModel
{
    public class TipTocke : Prikaz
    {
        private int cnt = 0; //brojac koji oznacava stanje u kojem se prikaz trenutno nalazi
        private double _oldCoordinate; //x koordinata tocke koja ceka dok na ulaz dodje i y koordinata

        public TipTocke(string name, double grade) : base(name, grade)
        {
        }


        /// <summary>
        /// Metoda sprema poruke koje salje tester, pipeline komunikacija
        /// </summary>
        /// <param name="num">Broj tipa double koji se salje</param>
        public override void SaveNumber(double num)
        {
            if (cnt == 0)
            {
                _oldCoordinate = num;
            }
            else
            {
                _pointList.Add(new Point(_oldCoordinate, num)); //ubacuje se nova tocka
            }

            cnt += 1;
            if (cnt == 2) cnt = 0;
        }
    }
}
