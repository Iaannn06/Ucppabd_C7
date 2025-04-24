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

namespace Ucppabd
{
    public partial class RekamMedis  : Form
    {
        static string connectionString = "Data Source=DESKTOP-L9CBIM9\\SQLEXPRESS01;Initial Catalog=UCP4;Integrated Security=True";
        public RekamMedis()
        {
            InitializeComponent();
            LoadData();
            dataGridViewRekamMedis.CellClick += dataGridViewRekamMedis_CellContentClick_1;
        }
        private void RekamMedis_Load(object sender, EventArgs e)
        {

        }


        private void btnTambah_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO RekamMedis (ID_Vaksin, NamaVaksin, TanggalKadaluarsa) VALUES (@ID_Vaksin, @NamaVaksin, @TanggalKadaluarsa)";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@ID", txtID.Text);
                cmd.Parameters.AddWithValue("@ID_Hewan", txtIDHewan.Text);
                cmd.Parameters.AddWithValue("@ID_Vaksin", txtIDVaksin.Text);
                cmd.Parameters.AddWithValue("@Keterangan", txtKeterangan.Text);
                DateTime Tanggal;
                if (DateTime.TryParse(txtTanggal.Text, out Tanggal))
                {
                    cmd.Parameters.AddWithValue("@Tanggal", Tanggal);
                }
                else
                {
                    MessageBox.Show("Format tanggal tidak valid. Harap masukkan tanggal dengan format yang benar (misalnya: 2025-04-23).");
                    return;
                }



                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data berhasil ditambahkan");
                ClearForm();
                LoadData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewRekamMedis.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridViewRekamMedis.CurrentRow.Cells["ID"].Value);

                var confirm = MessageBox.Show("Yakin ingin menghapus data Rekam Medis ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string query = "DELETE FROM RekamMedis WHERE ID = @id";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@id", id);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data berhasil dihapus", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearForm();
                        LoadData();
                    }
                }
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewRekamMedis.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridViewRekamMedis.CurrentRow.Cells["ID"].Value);

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "UPDATE RekamMedis SET ID=@ID, ID_Hewan=@ID_Hewan,ID_Vaksin=@ID_Vaksin,Keteranggan=@Keterangan Tanggal=@Tanggal WHERE ID=@ID";


                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ID", txtID.Text);
                    cmd.Parameters.AddWithValue("@ID_Hewan", txtIDHewan.Text);
                    cmd.Parameters.AddWithValue("@ID_Vaksin", txtIDVaksin.Text);
                    cmd.Parameters.AddWithValue("@Keterangan", txtKeterangan.Text);
                    DateTime Tanggal;
                    if (DateTime.TryParse(txtTanggal.Text, out Tanggal))
                    {
                        cmd.Parameters.AddWithValue("@Tangga", Tanggal);
                    }
                    else
                    {
                        MessageBox.Show("Format tanggal tidak valid. Harap masukkan tanggal dengan format yang benar (misalnya: 2025-04-23).");
                        return;
                    }




                    con.Open();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Data berhasil diperbarui");
                    ClearForm();
                    LoadData();
                }
            }
        }

        private void dataGridViewVaksin_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewRekamMedis.Rows[e.RowIndex];

                txtID.Text = row.Cells["ID"].Value.ToString();
                txtIDHewan.Text = row.Cells["ID_Hewan"].Value.ToString();
                txtIDVaksin.Text = row.Cells["TanggalKadaluarsa"].Value.ToString();
                txtKeterangan.Text = row.Cells["Keterangan"].Value.ToString();
                txtTanggal.Text = row.Cells["Tanggal"].Value.ToString();    

            }
        }

        private void ClearForm()
        {
            txtID.Clear();
            txtIDHewan.Clear();
            txtIDVaksin.Clear();
            txtKeterangan.Clear();
            txtTanggal.Clear();

        }
        private void LoadData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM RekamMedis";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridViewRekamMedis.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data: " + ex.Message);
            }
        }

        private void dataGridViewRekamMedis_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void RekamMedis_Load_1(object sender, EventArgs e)
        {

        }

        private void dataGridViewRekamMedis_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}