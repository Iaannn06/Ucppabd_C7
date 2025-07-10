using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace Ucppabd
{
    public partial class ReportViewer : Form
    {
        // 1. Membuat instance dari kelas Koneksi dan variabel string koneksi
        private Koneksi koneksi = new Koneksi();
        private string strKonek;

        public ReportViewer()
        {
            InitializeComponent();

            // 2. Mengambil connection string dari kelas Koneksi
            // Pastikan Anda sudah menyesuaikan file Koneksi.cs Anda
            strKonek = koneksi.connectionString();
        }

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            // Atur agar ReportViewer mengisi seluruh form
            this.reportViewer1.Dock = DockStyle.Fill;

            // Hentikan proses jika form dibuka dalam mode Desain
            if (this.DesignMode) return;

            try
            {
                // 3. Mengambil data dari database
                DataTable dt = new DataTable();
                using (var con = new SqlConnection(strKonek))
                using (var cmd = new SqlCommand("GetAllJanjiTemuDetail", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }

                // 4. Membuat dan menyambungkan sumber data ke laporan
                // PERHATIAN: Nama "DataSetReport" HARUS SAMA PERSIS dengan nama DataSet di file .rdlc
                ReportDataSource rds = new ReportDataSource("DataSetReport", dt);

                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.DataSources.Add(rds);

                // 5. Refresh laporan untuk menampilkan data
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat laporan: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}