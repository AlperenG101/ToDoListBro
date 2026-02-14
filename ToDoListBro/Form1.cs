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

namespace ToDoListBro
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int arttirilanKonum = 10;
        List<string> liste = new List<string>();
        private void ekle_button_Click(object sender, EventArgs e)
        {
            CheckBox yeniNot = new CheckBox();
            Button sil_button = new Button();
            yeniNot.ForeColor = Color.White;
            yeniNot.Text = gorev_txt.Text;
            yeniNot.CheckedChanged += (s, ev) =>
            {
                if (yeniNot.Checked)
                {
                    yeniNot.Font = new Font(yeniNot.Font, FontStyle.Italic | FontStyle.Strikeout);
                    yeniNot.ForeColor = Color.Gray;
                }
                else
                {
                    yeniNot.Font = new Font(yeniNot.Font, FontStyle.Regular);
                    yeniNot.ForeColor = Color.White;
                }
            };

            yeniNot.Location = new Point(10, arttirilanKonum);
            yeniNot.AutoSize = true;
            sil_button.Location = new Point(panel1.Right - 45, arttirilanKonum);
            sil_button.Text = "Sil";
            sil_button.BackColor = Color.White;
            sil_button.ForeColor = Color.Black;
            sil_button.Width = 40;
            sil_button.Click += (s, ev) =>
            {
                liste.Remove(yeniNot.Text);
                panel1.Controls.Remove(yeniNot);
                panel1.Controls.Remove(sil_button);
                int silinenYukseklik = 25; 
                foreach (Control ctrl in panel1.Controls)
                {
                    if (ctrl.Top > yeniNot.Top)
                    {
                        ctrl.Top -= silinenYukseklik;
                    }
                }
                arttirilanKonum -= silinenYukseklik;
                panel1.Controls.Remove(yeniNot);
                panel1.Controls.Remove(sil_button);
                yeniNot.Dispose();
                sil_button.Dispose();
            };
            panel1.Controls.Add(yeniNot);
            panel1.Controls.Add(sil_button);
            yeniNot.BringToFront();
            liste.Add(yeniNot.Text);
            gorev_txt.Text = "";
            arttirilanKonum += 25;
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.BringToFront();
            if (File.Exists(Path.Combine(Application.StartupPath, "index.txt")))
            {
                string[] okunanlar = File.ReadAllLines(Path.Combine(Application.StartupPath, "index.txt"));
                foreach (string gorev in okunanlar)
                {
                    if (string.IsNullOrWhiteSpace(gorev)) continue;
                    CheckBox yeniNot = new CheckBox();
                    Button sil_button = new Button();
                    sil_button.Location = new Point(panel1.Right -45, arttirilanKonum);
                    sil_button.Text = "Sil";
                    sil_button.BackColor = Color.White;
                    sil_button.ForeColor = Color.Black;
                    sil_button.Width = 40;
                    yeniNot.Text = gorev;
                    if (yeniNot.Checked == true)
                    {
                        yeniNot.Font = new Font(yeniNot.Font, FontStyle.Italic | FontStyle.Strikeout);
                    }
                    yeniNot.ForeColor = Color.White;
                    yeniNot.AutoSize = true;
                    yeniNot.Location = new Point(10, arttirilanKonum);
                    liste.Add (gorev);
                    sil_button.Click += (s, ev) =>
                    {
                        liste.Remove(yeniNot.Text);
                        panel1.Controls.Remove(yeniNot);
                        panel1.Controls.Add(sil_button);
                        panel1.Controls.Remove(sil_button);
                        int silinenYukseklik = 25; 
                        foreach (Control ctrl in panel1.Controls)
                        {
                            if (ctrl.Top > yeniNot.Top)
                            {
                                ctrl.Top -= 25; 
                            }
                        }
                        arttirilanKonum -= silinenYukseklik;
                        yeniNot.Dispose();
                        sil_button.Dispose();
                    };

                    panel1.Controls.Add (yeniNot);
                    panel1.Controls.Add (sil_button);

                    arttirilanKonum += 25;

                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            File.WriteAllLines(Path.Combine(Application.StartupPath, "index.txt"), liste);
        }
    }
}
