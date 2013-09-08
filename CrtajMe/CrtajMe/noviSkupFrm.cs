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
    public partial class noviSkupFrm : Form, INoviSkup
    {
        public noviSkupFrm()
        {
            InitializeComponent();
        }

        public double GetSkupTreshold()
        {
            if (textBox2.Text == "")
                textBox2.Text = "0";
            return Convert.ToDouble(textBox2.Text);
        }

        public string GetSkupName()
        {
            return textBox1.Text;
        }

        public string GetSkupType()
        {
            if (comboBox1.SelectedItem == null) return "";
            return comboBox1.SelectedItem.ToString();
        }

        public bool outerShow()
        {
            DialogResult rez = this.ShowDialog();
            if (rez == System.Windows.Forms.DialogResult.OK)
                return true;
            else
                return false;
        }

        private void noviSkupFrm_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Ucitaju se svi tipovi prikaza koji su u sustavu
        /// </summary>
        /// <param name="listType"></param>
        public void LoadPrikazTypes(List<string> listType)
        {
            comboBox1.Items.Clear();
            foreach (string s in listType)
            {
                comboBox1.Items.Add(s);
            }
        }

        /// <summary>
        /// Ispis errora u obliku messagebox-a
        /// </summary>
        /// <param name="msg">Opis errora</param>
        public void ShowErrorMessage(string msg)
        {
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            double br;
            bool isDouble = double.TryParse(textBox2.Text,  out br);

            if (!isDouble)
                if (textBox2.Text != "")
                {
                    ShowErrorMessage("Samo brojcane vrijednosti se prihvacaju!");
                    textBox2.Text = textBox2.Text.Substring(0, textBox2.Text.Length - 1);
                }
      }

      

    }
}
