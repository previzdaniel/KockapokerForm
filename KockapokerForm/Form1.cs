using KockapokerForm.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KockapokerForm
{
    public partial class frmFo : Form
    {
        private Dobas gep;
        private Dobas ember;
        private PictureBox[] gepKep;
        private PictureBox[] emberKep;
        public int dontetlen = 0;
        public frmFo()
        {
            InitializeComponent();
            gepKep = new PictureBox[5] { pbGep1, pbGep2, pbGep3, pbGep4, pbGep5 };

            emberKep = new PictureBox[5] { pbEmber1, pbEmber2, pbEmber3, pbEmber4, pbEmber5 };
            gep = new Dobas();
            ember = new Dobas();
            lblGepReszeredmeny.Text = "";
            lblEmberReszeredmeny.Text = "";
            lblEmberEredmeny.Text = "0";
            lblGepEredmeny.Text = "0";
            lbEredmeny.Text = "";
            lblDontetlen.Text = "0";
            lblMenetszam.Text = "0";
        }

        private void KepElhelyez(PictureBox pb, int szam)
        {
            switch (szam)
            {
                case 1: pb.Image = Resources.egy;
                    break;
                case 2: pb.Image = Resources.ketto;
                    break;
                case 3: pb.Image = Resources.harom;
                    break;
                case 4: pb.Image = Resources.negy;
                    break;
                case 5: pb.Image = Resources.ot;
                    break;
                case 6: pb.Image = Resources.hat;
                    break;

                default:
                    break;
            }
        }

        private void DobasMegjelenit(Dobas d, PictureBox[] kepek)
        {
            for (int i = 0; i < 5; i++)
            {
                KepElhelyez(kepek[i], d.Kockak[i]);
            }
        }

        private void btnDobas_Click(object sender, EventArgs e)
        {
            gep.EgyDobas();
            ember.EgyDobas();
            

            DobasMegjelenit(gep, gepKep);
            DobasMegjelenit(ember, emberKep);

            lblEmberReszeredmeny.Text = ember.Eredmeny;
            lblGepReszeredmeny.Text = gep.Eredmeny;

            if (gep.Pont > ember.Pont)
            {
                lbEredmeny.Text = "Gép nyert";
                gep.nyert++;
                lblGepEredmeny.Text = gep.nyert.ToString();
            }
            else if (gep.Pont < ember.Pont)
            {
                lbEredmeny.Text = "Játékos nyert";
                ember.nyert++;
                lblEmberEredmeny.Text = ember.nyert.ToString();
            }
            else
            {
                dontetlen++;
                lbEredmeny.Text = "Döntetlen";
                lblDontetlen.Text = $"{dontetlen}";
            }
            lblMenetszam.Text = $"{dontetlen + ember.nyert + gep.nyert}";
            if (lblGepEredmeny.Text == "999" || lblEmberEredmeny.Text == "999")
            {
                MessageBox.Show("A szerencsejáték függőséget okoz", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnKilepes_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
