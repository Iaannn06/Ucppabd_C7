using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace Praktikum7
{
    public partial class reportexportdata : Form
    {
        public reportexportdata()
        {
            InitializeComponent();
        }

        private void reportexportdata_Load(object sender, EventArgs e)
        {
            // Siapkan data dan refresh tampilan laporan
            SetupReportViewer();
            this.reportViewer1.RefreshReport();
        }

        private void SetupReportViewer()
        {
            // Connection string ke database ProjecctPABD
            string connectionString = "Data Source=DESKTOP-L9CBIM9\\SQLEXPRESS01;Initial Catalog=ProjecctPABD;Integrated Security=True;";

            // Query join untuk laporan janji temu lengkap
            string query = @"
                SELECT Pemilik.ID_Pemilik, Hewan.ID_Hewan, Pemilik.Nama AS NamaPemilik, 
                       Hewan.Nama AS NamaHewan, Hewan.Jenis, Pemilik.Telepon, 
                       Dokter.ID AS ID_Dokter, Dokter.Nama AS NamaDokter, Dokter.Spesialisasi, 
                       JanjiTemu.Tanggal
                FROM   Pemilik 
                       INNER JOIN Hewan ON Pemilik.ID_Pemilik = Hewan.ID_Pemilik 
                       INNER JOIN JanjiTemu ON Hewan.ID_Hewan = JanjiTemu.ID_Hewan 
                       INNER JOIN Dokter ON JanjiTemu.ID_Dokter = Dokter.ID";

            // Siapkan tempat penyimpanan data
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mengambil data untuk laporan: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // ReportDataSource mengarah ke dataset dalam file RDLC
            ReportDataSource rds = new ReportDataSource("DataSetJanjiTemu", dt);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            // Gunakan path relatif ke file RDLC
            string reportPath = Path.Combine(Application.StartupPath, "ReportExport.rdlc");

            if (!File.Exists(reportPath))
            {
                MessageBox.Show("File RDLC tidak ditemukan di: " + reportPath, "File Tidak Ditemukan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            reportViewer1.LocalReport.ReportPath = reportPath;

            // Refresh final
            reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}

