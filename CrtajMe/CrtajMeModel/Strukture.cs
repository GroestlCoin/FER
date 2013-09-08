using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrtajMeModel
{
    public class Point
    {
        public double X;
        public double Y;

        public double getX
        {
            get
            {
                return X;
            }
        }

        public double getY
        {
            get
            {
                return Y;
            }
        }

        /// <summary>
        /// Konstruktor napravi tocku s koordinatama X i Y
        /// </summary>
        /// <param name="_X">X koordinata</param>
        /// <param name="_Y">Y koordinata</param>
        public Point(double _X, double _Y)
        {
            X = _X;
            Y = _Y;
        }

        /// <summary>
        /// Konstruktor napravi tocku s koordinatama (0,0)
        /// </summary>
        public Point()
        {
            X = 0;
            Y = 0;
        }
    }

    public class Line
    {
        double X1, Y1;
        double X2, Y2;

        public double getX1
        { get { return X1; } }

        public double getX2
        { get { return X2; } }

        public double getY1
        { get { return Y1; } }

        public double getY2
        { get { return Y2; } }


        /// <summary>
        /// Konstruktor napravi liniju s tockama (X1, Y1) do (X2, Y2)
        /// </summary>
        /// <param name="_X1">X koordinata prve tocke</param>
        /// <param name="_Y1">Y koordinata druge tocke</param>
        /// <param name="_X2">X koordinata prve tocke</param>
        /// <param name="_Y2">Y koordinata druge tocke</param>
        public Line(double _X1, double _Y1, double _X2, double _Y2)
        {
            X1 = _X1;
            Y1 = _Y1;
            X2 = _X2;
            Y2 = _Y2;
        }
    }

}
