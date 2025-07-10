using Microsoft.Reporting.WinForms;
using System.Data; // Ensure this namespace is included for DataTable

namespace Ucppabd
{
    partial class DataSetReport
    {
        // Declare and initialize the DataTable 'dt' before using it
        private DataTable dt = new DataTable();

        // Rename the constructor to avoid conflict with the existing one
        public DataSetReport(string reportName)
        {
            // Example initialization of 'dt' (you can replace this with actual data)
            dt.Columns.Add("Column1");
            dt.Rows.Add("SampleData");

            // Nama "DataSetReport" ini cocok persis dengan nama kelas DataSet Anda.
            ReportDataSource rds = new ReportDataSource(reportName, dt);
        }
    }
}
