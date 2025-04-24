using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ucppabd
{
    public partial class Maincs : Form
    {
        public Maincs()
        {
            InitializeComponent();
        }

        private void btnDokter_Click(object sender, EventArgs e)
        {
            Dokter d = new Dokter();
            d.Show();
        }

        private void btnVaksin_Click(object sender, EventArgs e)
        {
            Vaksin v = new Vaksin();
            v.Show();
        }

        private void btnPemilik_Click(object sender, EventArgs e)
        {
            Pemilik P = new Pemilik();
            P.Show();
        }

        private void btnRekamMedis_Click(object sender, EventArgs e)
        {
            RekamMedis r = new RekamMedis();
            r.Show();
        }

        private void btnHewan_Click(object sender, EventArgs e)
        {
            Hewan h = new Hewan();
            h.Show();
        }
        private void btnJanjiTemu_Click(object sender, EventArgs e)
        {
            JanjiTemu j = new JanjiTemu();
            j.Show();
        }
    }
}




