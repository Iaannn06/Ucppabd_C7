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
    public partial class Dokter : Form
    {
        static string connectionString = "Data Source=DESKTOP-L9CBIM9\\SQLEXPRESS01;Initial Catalog=UCP4;Integrated Security=True";
        public Dokter()
        {
            InitializeComponent();
            LoadData();
            dataGridViewDokter.CellClick += dataGridViewDokter_CellContentClick;
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
                string query = "INSERT INTO Dokter (ID, Nama, Spesialisasi, Telepon) VALUES (@ID, @Nama, @Spesialisasi, @Telepon)";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@ID", txtID.Text);
                cmd.Parameters.AddWithValue("@Nama", txtNama.Text);
                cmd.Parameters.AddWithValue("@Spesialisasi", txtSpesialisasi.Text);
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
            if (dataGridViewDokter.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridViewDokter.CurrentRow.Cells["ID"].Value);

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
            if (dataGridViewDokter.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridViewDokter.CurrentRow.Cells["ID"].Value);

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Dokter SET ID=@ID, Nama=@Nama, Spesialisasi=@Spesialisasi, Telepon=@Telepon WHERE ID=@ID";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ID", txtID.Text);
                    cmd.Parameters.AddWithValue("@Nama", txtNama.Text);
                    cmd.Parameters.AddWithValue("@Spesialisasi", txtSpesialisasi.Text);
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
                DataGridViewRow row = dataGridViewDokter.Rows[e.RowIndex];

                txtID.Text = row.Cells["ID"].Value.ToString();
                txtNama.Text = row.Cells["Nama"].Value.ToString();
                txtSpesialisasi.Text = row.Cells["Spesialisasi"].Value.ToString();
                txtTelepon.Text = row.Cells["Telepon"].Value.ToString();
            }
        }

        private void ClearForm()
        {
            txtID.Clear();
            txtNama.Clear();
            txtSpesialisasi.Clear();
            txtTelepon.Clear();

        }
        private void LoadData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Dokter";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridViewDokter.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data: " + ex.Message);
            }
        }
    }
}
