using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrtajMeControllers;

namespace CrtajMe.CrtajMeView
{
    public partial class rezFrm : Form, IViewResults
    {
        private ResultsController _resultsController = new ResultsController();
        
        public rezFrm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Ispis errora u obliku messagebox-a
        /// </summary>
        /// <param name="msg">Opis errora</param>
        public void ShowErrorMessage(string msg)
        {
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        private void rezFrm_Load(object sender, EventArgs e)
        {
            ShowSkupInList();
            ShowPrikazInList();
            ShowPrikaziInSkup();
        }

        /// <summary>
        /// Update od observera
        /// </summary>
        public void UpdateEx()
        {
            ShowSkupInList();
            ShowPrikazInList();
            ShowPrikaziInSkup();
            ClearDetails();
        }

        
        /// <summary>
        /// Prikazuju se skupovi u listi
        /// </summary>
        private void ShowPrikazInList()
        {
            listBox1.Items.Clear();
            List<string> prikaziName = _resultsController.GetAllPrikazi();
            foreach(string name in prikaziName)
            {
                listBox1.Items.Add(name);
            }
        }

        /// <summary>
        /// Prikazuju se skupovi u list box-u
        /// </summary>
        private void ShowSkupInList()
        {
            listBox2.Items.Clear();
            List<string> skupoviName = _resultsController.GetAllSkupovi();
            foreach (string name in skupoviName)
            {
                listBox2.Items.Add(name);
            }
        }


        public void ShowView()
        {
            this.Show();
        }

        private void rezFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _resultsController.RemoveForm(this);
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowDetails();
         }

        public void ShowDetails()
        {
            string name = listBox2.SelectedItem.ToString();
            textBox1.Text = name;
            textBox2.Text = _resultsController.GetAverageGrade(this, name);
            textBox3.Text = _resultsController.GetAccept(this, name);
            textBox4.Text = _resultsController.GetTresholdOfSkup(this, name);
            textBox5.Text = _resultsController.GetTypeOfSkup(this, name);
            ShowPrikaziInSkup();
        }

        public void ClearDetails()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            listBox3.Items.Clear();
        }

        public void ShowPrikaziInSkup()
        {
            if (listBox2.SelectedItem == null)
                return;

            List<string> listPrikaza = _resultsController.GetPrikaziInSkup(this, listBox2.SelectedItem.ToString());
            listBox3.Items.Clear();
            
            foreach (string name in listPrikaza)
            {
                listBox3.Items.Add(name);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null || listBox2.SelectedItem == null)
            {
                ShowErrorMessage("Odaberite barem jedan prikaz i barem jedan skup!");
                return;
            }
            
            string prikazName = listBox1.SelectedItem.ToString();
            string skupName = listBox2.SelectedItem.ToString();

            _resultsController.InsertPrikazInSKup(this, prikazName, skupName);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = listBox1.SelectedItem.ToString();
            textBox6.Text = _resultsController.GetTypeOfPrikaz(this, name);
        }

        private void button2_Click(object sender, EventArgs e)
        {
                
            if (listBox3.SelectedItem != null && listBox2.SelectedItem != null)
            {
                string prikazName = listBox3.SelectedItem.ToString();
                string skupName = listBox2.SelectedItem.ToString();
                _resultsController.RemovePrikazFromSkup(this, prikazName, skupName);
            }
        }
    }
}
