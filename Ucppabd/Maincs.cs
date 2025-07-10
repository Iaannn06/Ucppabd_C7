using System;
using System.Windows.Forms;

namespace Ucppabd
{
    // Form ini berfungsi sebagai menu utama atau dashboard aplikasi.
    // Dari sini, pengguna dapat membuka form-form lain untuk mengelola data.
    public partial class Maincs : Form
    {
        public Maincs()
        {
            InitializeComponent();
        }

        // Event handler saat tombol Dokter diklik
        private void btnDokter_Click(object sender, EventArgs e)
        {
            // Membuat instance baru dari form Dokter dan menampilkannya
            Dokter d = new Dokter();
            d.Show();
        }

        // Event handler saat tombol Vaksin diklik
        private void btnVaksin_Click(object sender, EventArgs e)
        {
            // Membuat instance baru dari form Vaksin dan menampilkannya
            Vaksin v = new Vaksin();
            v.Show();
        }

        // Event handler saat tombol Pemilik diklik
        private void btnPemilik_Click(object sender, EventArgs e)
        {
            // Membuat instance baru dari form Pemilik dan menampilkannya
            Pemilik P = new Pemilik();
            P.Show();
        }

        // Event handler saat tombol Rekam Medis diklik
        private void btnRekamMedis_Click(object sender, EventArgs e)
        {
            // Membuat instance baru dari form RekamMedis dan menampilkannya
            RekamMedis r = new RekamMedis();
            r.Show();
        }

        // Event handler saat tombol Hewan diklik
        private void btnHewan_Click(object sender, EventArgs e)
        {
            // Membuat instance baru dari form Hewan dan menampilkannya
            Hewan h = new Hewan();
            h.Show();
        }

        // Event handler saat tombol Janji Temu diklik
        private void btnJanjiTemu_Click(object sender, EventArgs e)
        {
            // Membuat instance baru dari form JanjiTemu dan menampilkannya
            JanjiTemu j = new JanjiTemu();
            j.Show();
        }

        // Event handler saat tombol Laporan diklik
        private void btnLaporan_Click(object sender, EventArgs e)
        {
            // Membuat instance dari form ReportViewer Anda
            ReportViewer formViewer = new ReportViewer();

            // Menampilkan form tersebut
            formViewer.Show();
        }
    }
}