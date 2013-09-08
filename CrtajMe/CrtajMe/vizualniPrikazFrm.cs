using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrtajMeControllers;


namespace CrtajMe
{
    public partial class vizualniPrikazFrm : Form, IVisualView
    {
        public vizualniPrikazFrm()
        {
            InitializeComponent();
        }

        List<CrtajMeModel.Point> _points = new List<CrtajMeModel.Point>();
        List<CrtajMeModel.Line> _lines = new List<CrtajMeModel.Line>();
        GraphController _graphController = new GraphController();

        private void vizualniPrikazFrm_Load(object sender, EventArgs e)
        {
            
        }

        public void ShowView(string prikazName)
        {
            label2.Text = prikazName;
            this.Show();
        }

        public void DrawLine(CrtajMeModel.Line newLine)
        {
            _lines.Add(newLine);
        }

        public void DrawPoint(CrtajMeModel.Point newPoint)
        {
            _points.Add(newPoint);
        }

        private double maxXPoint()
        {
            double retMaxX = _points[0].X;
            foreach (CrtajMeModel.Point p in _points)
            {
                if (p.X > retMaxX)
                    retMaxX = p.X;
            }
            return retMaxX;
        }

        private double minXPoint()
        {
            double retMinX = _points[0].X;
            foreach (CrtajMeModel.Point p in _points)
            {
                if (p.X < retMinX)
                    retMinX = p.X;
            }
            return retMinX;
        }

        private double maxYPoint()
        {
            double retMaxY = _points[0].Y;
            foreach (CrtajMeModel.Point p in _points)
            {
                if (p.Y> retMaxY)
                    retMaxY = p.Y;
            }
            return retMaxY;
        }
        
        private double minYPoint()
        {
            double retMinY = _points[0].Y;
            foreach (CrtajMeModel.Point p in _points)
            {
                if (p.Y < retMinY)
                    retMinY = p.Y;
            }
            return retMinY;
        }

        private double maxXLine()
        {
            double retMaxX = Math.Max(_lines[0].getX1, _lines[0].getX2);
            foreach (CrtajMeModel.Line l in _lines)
            {
                if (Math.Max(l.getX1, l.getX2) > retMaxX)
                    retMaxX = Math.Max(l.getX1, l.getX2);
            }
            return retMaxX;
        }

        private double minXLine()
        {
            double retMinX = Math.Min(_lines[0].getX1, _lines[0].getX2);
            foreach (CrtajMeModel.Line l in _lines)
            {
                if (Math.Min(l.getX1, l.getX2) < retMinX)
                    retMinX = Math.Min(l.getX1, l.getX2);
            }
            return retMinX;
        }

        private double maxYLine()
        {
            double retMaxY = Math.Max(_lines[0].getY1, _lines[0].getY2);
            foreach (CrtajMeModel.Line l in _lines)
            {
                if (Math.Max(l.getY1, l.getY2) > retMaxY)
                    retMaxY = Math.Max(l.getY1, l.getY2);
            }
            return retMaxY;
        }

        private double minYLine()
        {
            double retMinY = Math.Min(_lines[0].getY1, _lines[0].getY2);
            foreach (CrtajMeModel.Line l in _lines)
            {
                if (Math.Min(l.getY1, l.getY2) < retMinY)
                    retMinY = Math.Min(l.getY1, l.getY2);
            }
            return retMinY;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                Graphics g = Graphics.FromImage(b);
                g.FillRectangle(Brushes.White, new Rectangle(new Point(0, 0), pictureBox1.Size));
                DrawAll(g);
                b.Save(label2.Text+".jpg");
                ShowInfoMessage("Slika je spremljena pod nazivom: " + label2.Text + ".jpg");
            }
            catch
            {
                ShowInfoMessage("Slika nije spremljena!");
            }
        }

        private void DrawPoints(Graphics g)
        {
            if (_points.Count == 0)
                return;

            double height = pictureBox1.Height - 10;
            double width = pictureBox1.Width - 10;

            //skaliranje
            double _maxX = 1.1*maxXPoint();
            double _minX = minXPoint();
            double _maxY = 1.1*maxYPoint();
            double _minY = minYPoint();

            double koefX = width / Math.Abs(_maxX - _minX);
            double koefY = height / Math.Abs(_maxY - _minY);

            int realX, realY;

            foreach (CrtajMeModel.Point p in _points)
            {
                realX = 5+(int)((p.X-_minX) * koefX);
                realY = 5+(int)height - (int)((p.Y-_minY) * koefY);
                g.DrawEllipse(Pens.Black, realX, realY, 2, 2);
                Application.DoEvents();
            }

        }

        private void DrawLines(Graphics g)
        {
            if (_lines.Count == 0)
                return;

            double height = pictureBox1.Height - 10;
            double width = pictureBox1.Width - 10;

            //skaliranje
            double _maxX = 1.1 * maxXLine();
            double _minX = minXLine();
            double _maxY = 1.1 * maxYLine();
            double _minY = minYLine();

            double koefX = width / Math.Abs(_maxX - _minX);
            double koefY = height / Math.Abs(_maxY - _minY);

            int realX1, realX2, realY1, realY2;

            foreach (CrtajMeModel.Line l in _lines)
            {
                realX1 = 5+(int)((l.getX1) * koefX);
                realY1 = 5+(int)height - (int)((l.getY1) * koefY);

                realX2 = 5+(int)((l.getX2) * koefX);
                realY2 = 5+(int)height - (int)((l.getY2) * koefY);

                g.DrawLine(Pens.Black, new Point(realX1, realY1), new Point(realX2, realY2));
                Application.DoEvents();
            }
        }

        private void DrawAll(Graphics g)
        {
            DrawPoints(g);
            DrawLines(g);   
        }


        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            DrawAll(e.Graphics);
        }

        public void ShowErrorMessage(string msg)
        {
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void ShowInfoMessage(string msg)
        {
            MessageBox.Show(msg, "CrtajMe", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                ShowErrorMessage("Ocjena skupa nece biti spremljena!");
                return;
            }
            _graphController.SavePrikaz(this, label2.Text, Convert.ToDouble(textBox1.Text));
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            double br;
            bool isDouble = double.TryParse(textBox1.Text, out br);

            if (!isDouble)
                if (textBox1.Text != "")
                {
                    ShowErrorMessage("Samo brojcanje vrijednosti se prihvacaju!");
                    textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
                }
        }
    }
}
