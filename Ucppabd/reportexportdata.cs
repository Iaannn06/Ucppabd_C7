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

        private void reportexport_Load(object sender, EventArgs e)
        {
            // Setup ReportViewer data
            SetupReportViewer();
            // Refresh report to display data
            this.reportViewer1.RefreshReport();
            // ^^^ Perhatikan: Di sini pakai 'ReportViewer1' (R besar)
        }

        private void SetupReportViewer()
        {
            // connection string to your database
            string connectionString = "Data Source=DESKTOP-L9CBIM9\\SQLEXPRESS01;Initial Catalog=UCP4;Integrated Security=True;";
            // SQL query to retrieve the required data from the database
            string query = @"
                SELECT Pemilik.ID_Pemilik, Hewan.ID_Hewan, Pemilik.Nama AS NamaPemilik, Hewan.Nama AS NamaHewan, Hewan.Jenis, Pemilik.Telepon, Dokter.ID AS ID_Dokter, Dokter.Nama AS NamaDokter, Dokter.Spesialisasi, JanjiTemu.Tanggal
                FROM     Pemilik INNER JOIN
                  Hewan ON Pemilik.ID_Pemilik = Hewan.ID_Pemilik INNER JOIN
                  JanjiTemu ON Hewan.ID_Hewan = JanjiTemu.ID_Hewan INNER JOIN
                  Dokter ON JanjiTemu.ID_Dokter = Dokter.ID";

            // Create a DataTable to store the data
            DataTable dt = new DataTable();

            // Use SqlDataAdapter to fill the DataTable with data from the database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }

            // Create a ReportDataSource
            ReportDataSource rds = new ReportDataSource("DataSetReport", dt); // Make sure "DataSet1" matches your RDLC dataset name

            // Clear any existing data sources and add the new data source
            reportViewer1.LocalReport.DataSources.Clear(); // <-- Perhatikan: Di sini pakai 'reportViewer1' (r kecil)
            reportViewer1.LocalReport.DataSources.Add(rds);

            // Set the path to the report (.rdlc file)
            // Change this to the actual path of your RDLC file
            reportViewer1.LocalReport.ReportPath = @"D:\ADB\Ucppabd\Ucppabd\ReportExport.rdlc";
            // Refresh the ReportViewer to show the updated report
            reportViewer1.RefreshReport();
        }
    }
}