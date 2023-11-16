using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pizzacska
{
    public partial class Form1 : Form
    {
        public string kiválasztottPizza { get; private set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Adatbetoltes();
        }

        private void Adatbetoltes()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Vesszővel tagolt CSV |*.csv|Szöveges fájl (*.txt)|*.txt|Összes fájl (*.*)|*.*";
            ofd.FilterIndex = 0;
            ofd.Title = "Adatfálj neve";
            ofd.InitialDirectory = Environment.CurrentDirectory;
            ofd.FileName = "Pizza.csv";

            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                textBox_faljbetoltes.Text = Path.GetFileName(ofd.FileName);
                using (StreamReader sr = new StreamReader(ofd.FileName))
                {
                    sr.ReadLine();
                    while (!sr.EndOfStream)
                    {
                        listBox_pizza_menu.Items.Add(new Pizza(sr.ReadLine()));
                    }
                }
            }
        }

        public void Atlagszamitas()
        {
            double atlag = 0;
            foreach (Pizza item in listBox_pizza_menu.Items)
            {
                atlag += item.Par;
            }
            atlag = atlag / listBox_pizza_menu.Items.Count;

            MessageBox.Show($"A pizzák átlagára: {atlag.ToString("#,##0.0 Ft")}", "A pizzák átlaga", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button_atalagar(object sender, EventArgs e)
        {
            Atlagszamitas();
        }

        private void button_fizetesmegerosites_Click(object sender, EventArgs e)
        {
            if (radioButton_bankkartya.Checked || radioButton_kezpenz.Checked)
            {
                if (checkBox_szamla.Checked)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.InitialDirectory = Environment.CurrentDirectory;
                    saveFileDialog.FileName = "rendeles.txt";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string szamla = saveFileDialog.FileName;
                        listBox_kosar.Text = Path.GetFileName(szamla);
                        try
                        {
                            using (StreamWriter sw = new StreamWriter(szamla))
                            {
                                if (radioButton_bankkartya.Checked)
                                {
                                    sw.WriteLine("Bankkártya opció lett kiválasztva.");
                                    sw.WriteLine(listBox_kosar);
                                }
                                else if (radioButton_kezpenz.Checked)
                                {
                                    sw.WriteLine("Készpénz opció lett kiválasztva.");
                                    sw.WriteLine(listBox_kosar);
                                }
                            }
                        }
                        catch (IOException ex)
                        {
                            MessageBox.Show("Valami nem ok!" + Environment.NewLine + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Nincs kiválasztva fizető eszköz!");
            }
        }



        private void radioButton_bankkartya_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton_kezpenz_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void button_betoltes_Click(object sender, EventArgs e)
        {

        }


        private void listBox_pizza_menu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_pizza_menu.SelectedIndex != 0)
            {
                kiválasztottPizza = listBox_pizza_menu.SelectedItem.ToString();
            }
        }

        private void button_hozzaadas_Click(object sender, EventArgs e)
        {

            if (int.TryParse(textBox_db.Text, out int darabszam))
            {
  
                if (!string.IsNullOrEmpty(kiválasztottPizza))
                {
                    string kosarElem = $"{kiválasztottPizza} - {darabszam} db";
                    listBox_kosar.Items.Add(kosarElem);

                }
                else
                {
                    MessageBox.Show("Válassz ki egy pizzát a hozzáadáshoz!");
                }
            }
            else
            {
                MessageBox.Show("Kérlek, adj meg egy érvényes számot a darabszámnak!");
            }
            
        }
        private void checkBox_szamla_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox_db_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox_kosar_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }

}


