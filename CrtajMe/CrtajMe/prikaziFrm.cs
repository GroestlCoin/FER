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
    public partial class prikaziFrm : Form, IViewPrikaz
    {
        PrikazController _prikazController = new PrikazController();

        public prikaziFrm()
        {
            InitializeComponent();
        }

        private void prikaziFrm_Load(object sender, EventArgs e)
        {
            LoadPrikaz();
        }

        public void ShowErrorMessage(string msg)
        {
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }


        public void LoadPrikaz()
        {
            listBox1.Items.Clear();
            List<string> prikazList = _prikazController.GetAllPrikaz();
            foreach (string s in prikazList)
            {
                listBox1.Items.Add(s);
            }
        }

        public void UpdateEx()
        {
            LoadPrikaz();
        }

        public void ShowView()
        {
            this.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                _prikazController.RemovePrikaz(this, listBox1.SelectedItem.ToString());
            }
        }

        private void prikaziFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _prikazController.RemoveForm(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
           if (listBox1.SelectedItem == null)
           {
                ShowErrorMessage("Potrebno je odabrati prikaz!");
                return;
           }
           vizualniPrikazFrm frm = new vizualniPrikazFrm();
           _prikazController.ShowPrikaz(frm, listBox1.SelectedItem.ToString());
        }

    }
}
