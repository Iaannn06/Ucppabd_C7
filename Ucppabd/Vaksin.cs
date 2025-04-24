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
    public partial class Vaksin : Form
    {
        static string connectionString = "Data Source=DESKTOP-L9CBIM9\\SQLEXPRESS01;Initial Catalog=UCP4;Integrated Security=True";
        public Vaksin()
        {
            InitializeComponent();
            LoadData();
            dataGridViewVaksin.CellClick += dataGridViewVaksin_CellContentClick;
        }
        private void Vaksin_Load(object sender, EventArgs e)
        {

        }

     
        private void btnTambah_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Vaksin (ID_Vaksin, NamaVaksin, TanggalKadaluarsa) VALUES (@ID_Vaksin, @NamaVaksin, @TanggalKadaluarsa)";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@ID_Vaksin", txtIDVaksin.Text);
                cmd.Parameters.AddWithValue("@NamaVaksin", txtNamaVaksin.Text);
                DateTime tanggalKadaluarsa;
                if (DateTime.TryParse(txtTanggalKadaluarsa.Text, out tanggalKadaluarsa))
                {
                    cmd.Parameters.AddWithValue("@TanggalKadaluarsa", tanggalKadaluarsa);
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
            if (dataGridViewVaksin.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridViewVaksin.CurrentRow.Cells["ID_Vaksin"].Value);

                var confirm = MessageBox.Show("Yakin ingin menghapus data Vaksin ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string query = "DELETE FROM Vaksin WHERE ID_Vaksin = @id";
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
            if (dataGridViewVaksin.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridViewVaksin.CurrentRow.Cells["ID_Vaksin"].Value);

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Vaksin SET ID_Vaksin=@ID_Vaksin, NamaVaksin=@NamaVaksin, TanggalKadaluarsa=@TanggalKadaluarsa WHERE ID_Vaksin=@ID_Vaksin";


                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ID_Vaksin", txtIDVaksin.Text);
                    cmd.Parameters.AddWithValue("@NamaVaksin", txtNamaVaksin.Text);
                    DateTime tanggalKadaluarsa;
                    if (DateTime.TryParse(txtTanggalKadaluarsa.Text, out tanggalKadaluarsa))
                    {
                        cmd.Parameters.AddWithValue("@TanggalKadaluarsa", tanggalKadaluarsa);
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
                DataGridViewRow row = dataGridViewVaksin.Rows[e.RowIndex];

                txtIDVaksin.Text = row.Cells["ID_Vaksin"].Value.ToString();
                txtNamaVaksin.Text = row.Cells["NamaVaksin"].Value.ToString();
                txtTanggalKadaluarsa.Text = row.Cells["TanggalKadaluarsa"].Value.ToString();
                
            }
        }

        private void ClearForm()
        {
            txtIDVaksin.Clear();
            txtNamaVaksin.Clear();
            txtTanggalKadaluarsa.Clear();

        }
        private void LoadData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Vaksin";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridViewVaksin.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data: " + ex.Message);
            }
        }

        private void dataGridViewVaksin_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Vaksin_Load_1(object sender, EventArgs e)
        {

        }

        private void dataGridViewVaksin_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}