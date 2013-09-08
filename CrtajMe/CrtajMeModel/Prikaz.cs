using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrtajMeModel
{
    public abstract class Prikaz
    {

        protected List<Point> _pointList = new List<Point>();
        protected List<Line> _lineList = new List<Line>();
        protected string _ime;
        private double _ocjena = 0;
        private bool _isOcjenaSet = false;


        public Prikaz(string name, double grade)
        {
            _ime = name;
            _ocjena = grade;
            _isOcjenaSet = true;
        }


        public Prikaz(string name)
        {
            _ime = name;
        }

        public string Type
        {
            get
            {
                return this.GetType().ToString().Split('.')[1];
            }
        }

        public bool isOcjenaSet
        {
            get
            {
                return _isOcjenaSet;
            }
        }

        public double Grade
        {
            get
            {
                return _ocjena;
            }
            set
            {
                _ocjena = value;
                _isOcjenaSet = true;
            }
        }

        public string Name
        {
            get
            {
                return _ime;
            }
        }
        

        /// <summary>
        /// Vracaju se sve linije koje trebaju biti iscrtane
        /// </summary>
        /// <returns>Lista linija</returns>
        public List<Line> getLines()
        {
            return new List<Line>(_lineList);
        }

        /// <summary>
        /// Vracaju se sve tocke koje trebaju biti iscrtane
        /// </summary>
        /// <returns>Lista tocaka</returns>
        public List<Point> getPoints()
        {
            return new List<Point>(_pointList);
        }

        public abstract void SaveNumber(double num);
    }
}
