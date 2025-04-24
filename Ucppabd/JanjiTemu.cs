using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Ucppabd
{
    public partial class JanjiTemu : Form
    {
        private string connectionString = "Data Source=DESKTOP-L9CBIM9\\SQLEXPRESS01;Initial Catalog=UCP4;Integrated Security=True";

        public JanjiTemu()
        {
            InitializeComponent();
            LoadData();
            dataGridViewJanjiTemu.CellClick += dataGridViewJanjiTemu_CellContentClick;
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Mengecek apakah ID Dokter valid
                string checkDokterQuery = "SELECT COUNT(1) FROM Dokter WHERE ID = @ID_Dokter";
                SqlCommand checkCmd = new SqlCommand(checkDokterQuery, con);
                checkCmd.Parameters.AddWithValue("@ID_Dokter", txtID.Text);

                con.Open();
                int dokterCount = (int)checkCmd.ExecuteScalar();

                if (dokterCount == 0)
                {
                    MessageBox.Show("ID Dokter tidak ditemukan. Pastikan ID Dokter yang dimasukkan valid.");
                    return;
                }

                // Menambahkan janji temu
                string query = "INSERT INTO JanjiTemu (ID_JanjiTemu, ID_Hewan, ID_Dokter, Tanggal) " +
                               "VALUES (@ID_JanjiTemu, @ID_Hewan, @ID_Dokter, @Tanggal)";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@ID_JanjiTemu", txtIDJanjiTemu.Text);
                cmd.Parameters.AddWithValue("@ID_Hewan", txtIDHewan.Text);
                cmd.Parameters.AddWithValue("@ID_Dokter", txtID.Text);

                DateTime tanggal;
                if (DateTime.TryParse(txtTanggal.Text, out tanggal))
                {
                    cmd.Parameters.AddWithValue("@Tanggal", tanggal);
                }
                else
                {
                    MessageBox.Show("Format tanggal tidak valid (misal: 2025-04-23)");
                    return;
                }

                cmd.ExecuteNonQuery();
                MessageBox.Show("Janji Temu berhasil ditambahkan!");
                ClearForm();
                LoadData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewJanjiTemu.CurrentRow != null)
            {
                string id = dataGridViewJanjiTemu.CurrentRow.Cells["ID_JanjiTemu"].Value.ToString();

                var confirm = MessageBox.Show("Yakin ingin menghapus janji temu ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string query = "DELETE FROM JanjiTemu WHERE ID_JanjiTemu = @ID";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@ID", id);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data berhasil dihapus!");
                        ClearForm();
                        LoadData();
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewJanjiTemu.CurrentRow != null)
            {
                string id = txtIDJanjiTemu.Text;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "UPDATE JanjiTemu SET ID_Hewan=@ID_Hewan, ID_Dokter=@ID_Dokter, Tanggal=@Tanggal WHERE ID_JanjiTemu=@ID_JanjiTemu";
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@ID_JanjiTemu", id);
                    cmd.Parameters.AddWithValue("@ID_Hewan", txtIDHewan.Text);
                    cmd.Parameters.AddWithValue("@ID_Dokter", txtID.Text);

                    DateTime tanggal;
                    if (DateTime.TryParse(txtTanggal.Text, out tanggal))
                    {
                        cmd.Parameters.AddWithValue("@Tanggal", tanggal);
                    }
                    else
                    {
                        MessageBox.Show("Format tanggal tidak valid.");
                        return;
                    }

                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data berhasil diperbarui!");
                    ClearForm();
                    LoadData();
                }
            }
        }

        private void dataGridViewJanjiTemu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewJanjiTemu.Rows[e.RowIndex];
                txtIDJanjiTemu.Text = row.Cells["ID_JanjiTemu"].Value.ToString();
                txtIDHewan.Text = row.Cells["ID_Hewan"].Value.ToString();
                txtID.Text = row.Cells["ID_Dokter"].Value.ToString();
                txtTanggal.Text = row.Cells["Tanggal"].Value.ToString();
            }
        }

        private void ClearForm()
        {
            txtIDJanjiTemu.Clear();
            txtIDHewan.Clear();
            txtID.Clear();
            txtTanggal.Clear();
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM JanjiTemu";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridViewJanjiTemu.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data: " + ex.Message);
            }
        }
    }
}
