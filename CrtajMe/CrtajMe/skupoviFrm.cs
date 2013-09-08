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
    public partial class skupoviFrm : Form, IViewSkup
    {
        SkupoviController _skupoviController = new SkupoviController();

        public skupoviFrm()
        {
            InitializeComponent();
        }

        private void skupoviFrm_Load(object sender, EventArgs e)
        {
            ShowSkupInList();
        }

        /// <summary>
        /// Ispis errora u obliku messagebox-a
        /// </summary>
        /// <param name="msg">Opis errora</param>
        public void ShowErrorMessage(string msg)
        {
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Prikaz prozora
        /// </summary>
        public void ShowView()
        {
            this.Show();
        }


        /// <summary>
        /// Sucelje od observera za update
        /// </summary>
        public void UpdateEx()
        {
            ShowSkupInList();
        }

        /// <summary>
        /// Prikazuju se skupovi u list box-u
        /// </summary>
        private void ShowSkupInList()
        {
            listBox1.Items.Clear();
            List<string> skupoviName = _skupoviController.GetAllSkupovi();
            foreach (string name in skupoviName)
            {
                listBox1.Items.Add(name);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            noviSkupFrm newFrm = new noviSkupFrm();
            _skupoviController.AddNewSkup(newFrm);
        }

        private void skupoviFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _skupoviController.RemoveForm(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox2.Text = textBox3.Text = "";
            if (listBox1.SelectedItem != null)
            {
                _skupoviController.RemoveSkup(this, listBox1.SelectedItem.ToString());
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = listBox1.SelectedItem.ToString();
            textBox2.Text = _skupoviController.GetTresholdOfSkup(this, listBox1.SelectedItem.ToString());
            textBox3.Text = _skupoviController.GetTypeOfSkup(this, listBox1.SelectedItem.ToString());
        }
    }
}
