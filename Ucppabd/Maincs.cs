using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Ucppabd
{
    // Form ini berfungsi sebagai menu utama atau dashboard aplikasi.
    // Dari sini, pengguna dapat membuka form-form lain untuk mengelola data.
    public partial class Maincs : Form
    {
        private Koneksi koneksi = new Koneksi();
        private string strKonek;

        public Maincs()
        {
            InitializeComponent();
            strKonek = koneksi.connectionString();

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

        private void btnTesKoneksi_Click(object sender, EventArgs e)
        {
            // Menggunakan connection string yang sudah ada
            using (var con = new SqlConnection(strKonek))
            {
                try
                {
                    // Coba buka koneksi
                    con.Open();
                    // Jika berhasil, tampilkan pesan sukses
                    MessageBox.Show("Koneksi ke database berhasil!", "Status: Tersambung", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // Jika gagal, tampilkan pesan error
                    MessageBox.Show("Koneksi ke database gagal.\nError: " + ex.Message, "Koneksi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                // Koneksi akan otomatis tertutup oleh blok 'using'
            }
        }
    }
}