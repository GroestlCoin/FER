using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrtajMe.CrtajMeView;
using CrtajMeControllers;

namespace CrtajMe
{
    public partial class mainForm : Form, IMainForm
    {
        private MainWindowConroller _mainWindowConroller;
        bool _aplikacijaIsActive = false; //da li je aplikacija ucitana


        /// <summary>
        /// Generiranje pocetnog stanja
        /// </summary>
        private void GenerateOnView()
        {
            //dummy generiranje
            string applicationName = "ispis_aplikacija_test.exe";

            textBox1.Text = applicationName;
            _aplikacijaIsActive = true; //ovo tu ne smije biti, to je samo u svrhu prezentacije
            _mainWindowConroller.LoadUlazniObrazac(this, "Ulazni\\ulazni1.txt");
            _mainWindowConroller.LoadUlazniObrazac(this, "Ulazni\\ulazni2.txt");
            _mainWindowConroller.LoadUlazniObrazac(this, "Ulazni\\ulazni3.txt");
            _mainWindowConroller.LoadUlazniObrazac(this, "Ulazni\\ulazni4.txt");
            _mainWindowConroller.LoadUlazniObrazac(this, "Ulazni\\ulazni5.txt");


            _mainWindowConroller.LoadIzlazniObrazac(this, "Izlazni\\izlazni1.txt");
            _mainWindowConroller.LoadIzlazniObrazac(this, "Izlazni\\izlazni2.txt");
            _mainWindowConroller.LoadIzlazniObrazac(this, "Izlazni\\izlazni3.txt");
            _mainWindowConroller.LoadIzlazniObrazac(this, "Izlazni\\izlazni4.txt");
            _mainWindowConroller.LoadIzlazniObrazac(this, "Izlazni\\izlazni5.txt");
        }

        public mainForm()
        {
            InitializeComponent();
            _mainWindowConroller = new MainWindowConroller(this);
            GenerateOnView();
        }

        public void ShowErrorMessage(string msg)
        {
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void UpdateEx()
        {
            ListUlazniObrazac();
            ListIzlazniObrazac();
        }

        private void ListUlazniObrazac()
        {
            inputListBox.Items.Clear();
            List<string> obrazacList = _mainWindowConroller.UlazniObrazacList();
            foreach (string s in obrazacList)
            {
                inputListBox.Items.Add(s);
            }
        }

        private void ListIzlazniObrazac()
        {
            outputListBox.Items.Clear();
            List<string> obrazacList = _mainWindowConroller.IzlazniObrazacList();
            foreach (string s in obrazacList)
            {
                outputListBox.Items.Add(s);
            }
        }

        private void loadApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();
            textBox1.Text = openFileDialog1.FileName;
            if ( textBox1.Text != "") 
                    _aplikacijaIsActive = true;
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            aboutFrm newaboutFrm = new aboutFrm();
            newaboutFrm.ShowDialog();
        }

        private void rezultatiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

      
        private void mainForm_Load(object sender, EventArgs e)
        {
            LoadTypesNames();
        }


        private void LoadTypesNames()
        {
            comboBox1.Items.Clear();
            List<string> listType = _mainWindowConroller.GetPrikazTypes();
            foreach (string s in listType)
            {
                comboBox1.Items.Add(s);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (_aplikacijaIsActive == false)
            {
                ShowErrorMessage("Potrebno je ucitati aplikaciju!");
                return;
            }
            if (comboBox1.SelectedIndex == -1)
            {
                ShowErrorMessage("Potrebno je odabrati tip!");
                return;
            }

            if (inputListBox.SelectedIndex == -1 || outputListBox.SelectedIndex == -1)
            {
                ShowErrorMessage("Potrebno je odabrati ulazni i izlazni obrazac!");
                return;
            }

            if (textBox2.Text == "" || _mainWindowConroller.GetAllPrikaz().Contains(textBox2.Text))
            {
                ShowErrorMessage("Ime prikaza nije navedeno ili isto postoji u sustavu vec!");
                return;
            }

            string applicationName = textBox1.Text;
            string inputName = inputListBox.SelectedItem.ToString();
            string outputName = outputListBox.SelectedItem.ToString();
            string prikazType = comboBox1.SelectedItem.ToString();
            string prikazName = textBox2.Text;

            vizualniPrikazFrm newPrikaz = new vizualniPrikazFrm();

            _mainWindowConroller.RunTest(this, newPrikaz,applicationName, inputName, outputName, prikazType, prikazName);
            textBox2.Text = "";
       }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _mainWindowConroller.StopTest(this);   
        }

        private void skupoviToolStripMenuItem_Click(object sender, EventArgs e)
        {
            skupoviFrm newSkup = new skupoviFrm();
            _mainWindowConroller.ShowSkupoviView(newSkup);
        }

        private void prihvatljivostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rezFrm newRez = new rezFrm();
            _mainWindowConroller.ShowResultsView(newRez);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void prikaziToolStripMenuItem_Click(object sender, EventArgs e)
        {
            prikaziFrm newPrikaz = new prikaziFrm();
            _mainWindowConroller.ShowPrikazView(newPrikaz);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fileName;
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();
            fileName = openFileDialog1.FileName;
            _mainWindowConroller.LoadUlazniObrazac(this, fileName);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string fileName;
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();
            fileName = openFileDialog1.FileName;
            _mainWindowConroller.LoadIzlazniObrazac(this, fileName);
        }
        
       
    }
}
