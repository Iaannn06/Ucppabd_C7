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
    public partial class Pemilik : Form
    {
        static string connectionString = "Data Source=DESKTOP-L9CBIM9\\SQLEXPRESS01;Initial Catalog=UCP4;Integrated Security=True";
        public Pemilik()
        {
            InitializeComponent();
            LoadData();
            dataGridViewPemilik.CellClick += DataGridViewPemilik_CellClick; 
        }

        private void DataGridViewPemilik_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewPemilik.Rows[e.RowIndex];
                txtIDPemilik.Text = row.Cells["ID_Pemilik"].Value.ToString();
                txtNama.Text = row.Cells["Nama"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtTelepon.Text = row.Cells["Telepon"].Value.ToString();
            }
        }


        private void Dokter_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Pemilik (ID_Pemilik, Nama, Email, Telepon) VALUES (@ID_Pemilik, @Nama, @Email, @Telepon)";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@ID_Pemilik", txtIDPemilik.Text);
                cmd.Parameters.AddWithValue("@Nama", txtNama.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Telepon", txtTelepon.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data berhasil ditambahkan");
                ClearForm();
                LoadData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewPemilik.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridViewPemilik.CurrentRow.Cells["ID"].Value);

                var confirm = MessageBox.Show("Yakin ingin menghapus data Dokter ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string query = "DELETE FROM Dokter WHERE ID = @id";
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
            if (dataGridViewPemilik.CurrentRow != null)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Pemilik SET ID_Pemilik=@ID_Pemilik, Nama=@Nama, Email=@Email, Telepon=@Telepon WHERE ID_Pemilik=@ID_Pemilik";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ID_Pemilik", txtIDPemilik.Text);
                    cmd.Parameters.AddWithValue("@Nama", txtNama.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Telepon", txtTelepon.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Data berhasil diperbarui");
                    ClearForm();
                    LoadData();
                }
            }
        }


        private void dataGridViewDokter_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewPemilik.Rows[e.RowIndex];

                txtIDPemilik.Text = row.Cells["ID_Pemilik"].Value.ToString();
                txtNama.Text = row.Cells["Nama"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtTelepon.Text = row.Cells["Telepon"].Value.ToString();
            }
        }

        private void ClearForm()
        {
            txtIDPemilik.Clear();
            txtNama.Clear();
            txtEmail.Clear();
            txtTelepon.Clear();

        }
        private void LoadData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Pemilik";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridViewPemilik.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data: " + ex.Message);
            }
        }

        private void Pemilik_Load(object sender, EventArgs e)
        {

        }
    }
}
