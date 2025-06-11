using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            // Setup ReportViewer data saat form dimuat
            SetupReportViewer();
            // Refresh report untuk menampilkan data
            this.reportViewer1.RefreshReport();
        }

        private void SetupReportViewer()
        {
            // Connection string ke database Anda
            // Perubahan disini: Initial Catalog diubah menjadi ProjecctPABD
            string connectionString = "Data Source=DESKTOP-L9CBIM9\\SQLEXPRESS01;Initial Catalog=ProjecctPABD;Integrated Security=True;";

            // Query SQL untuk mengambil data yang dibutuhkan dari database
            string query = @"
                SELECT Pemilik.ID_Pemilik, Hewan.ID_Hewan, Pemilik.Nama AS NamaPemilik, 
                       Hewan.Nama AS NamaHewan, Hewan.Jenis, Pemilik.Telepon, 
                       Dokter.ID AS ID_Dokter, Dokter.Nama AS NamaDokter, Dokter.Spesialisasi, 
                       JanjiTemu.Tanggal
                FROM   Pemilik 
                       INNER JOIN Hewan ON Pemilik.ID_Pemilik = Hewan.ID_Pemilik 
                       INNER JOIN JanjiTemu ON Hewan.ID_Hewan = JanjiTemu.ID_Hewan 
                       INNER JOIN Dokter ON JanjiTemu.ID_Dokter = Dokter.ID";

            // Buat DataTable untuk menampung data
            DataTable dt = new DataTable();

            // Gunakan SqlDataAdapter untuk mengisi DataTable dengan data dari database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }

            // Buat sebuah ReportDataSource. 
            // Pastikan "DataSetJanjiTemu" sama persis dengan nama DataSet di file RDLC Anda.
            ReportDataSource rds = new ReportDataSource("DataSetJanjiTemu", dt);

            // Hapus sumber data yang ada dan tambahkan yang baru
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            // Atur path ke file laporan (.rdlc) Anda
            // Ganti ini dengan path sebenarnya dari file RDLC Anda
            reportViewer1.LocalReport.ReportPath = @"D:\ADB\Ucppabd\Ucppabd\ReportExport.rdlc";

            // Refresh ReportViewer untuk menampilkan laporan yang sudah diperbarui
            reportViewer1.RefreshReport();
        }
    }
}