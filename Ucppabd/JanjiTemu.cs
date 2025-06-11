using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Ucppabd
{
    public partial class JanjiTemu : Form
    {
        private string connectionString = "Data Source=DESKTOP-L9CBIM9\\SQLEXPRESS01;Initial Catalog=ProjecctPABD;Integrated Security=True";
        private DataTable _janjiTemuCache = null;
        private DateTime _cacheTime;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(10);

        public JanjiTemu()
        {
            InitializeComponent();
            LoadData();
            dataGridViewJanjiTemu.CellClick += dataGridViewJanjiTemu_CellContentClick;
        }

        private void LoadData()
        {
            if (_janjiTemuCache != null && (DateTime.Now - _cacheTime) < _cacheDuration)
            {
                dataGridViewJanjiTemu.DataSource = _janjiTemuCache;
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT ID_JanjiTemu, NamaHewan, NamaPemilik, NamaDokter, Tanggal FROM View_JanjiTemuDetail";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    _janjiTemuCache = dt;
                    _cacheTime = DateTime.Now;
                    dataGridViewJanjiTemu.DataSource = _janjiTemuCache;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDJanjiTemu.Text) || string.IsNullOrWhiteSpace(txtIDHewan.Text) || string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Semua ID (Janji Temu, Hewan, Dokter) wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!DateTime.TryParse(txtTanggal.Text, out DateTime tanggal))
            {
                MessageBox.Show("Format tanggal tidak valid (misal: 2025-06-10)", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("AddJanjiTemu", con, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_JanjiTemu", txtIDJanjiTemu.Text.Trim());
                        cmd.Parameters.AddWithValue("@ID_Hewan", txtIDHewan.Text.Trim());
                        cmd.Parameters.AddWithValue("@ID_Dokter", txtID.Text.Trim());
                        cmd.Parameters.AddWithValue("@Tanggal", tanggal);

                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    _janjiTemuCache = null;
                    MessageBox.Show("Janji Temu berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearForm();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    MessageBox.Show(ex.Message, "Kesalahan Validasi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewJanjiTemu.CurrentRow == null)
            {
                MessageBox.Show("Pilih data janji temu yang akan diupdate!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!DateTime.TryParse(txtTanggal.Text, out DateTime tanggal))
            {
                MessageBox.Show("Format tanggal tidak valid.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("UpdateJanjiTemu", con, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_JanjiTemu", txtIDJanjiTemu.Text.Trim());
                        cmd.Parameters.AddWithValue("@ID_Hewan", txtIDHewan.Text.Trim());
                        cmd.Parameters.AddWithValue("@ID_Dokter", txtID.Text.Trim());
                        cmd.Parameters.AddWithValue("@Tanggal", tanggal);

                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    _janjiTemuCache = null;
                    MessageBox.Show("Data berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearForm();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewJanjiTemu.CurrentRow == null)
            {
                MessageBox.Show("Pilih data janji temu yang akan dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("Yakin ingin menghapus janji temu ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                string id = dataGridViewJanjiTemu.CurrentRow.Cells["ID_JanjiTemu"].Value.ToString();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction();
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("DeleteJanjiTemu", con, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ID_JanjiTemu", id);
                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                        _janjiTemuCache = null;
                        MessageBox.Show("Data berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        ClearForm();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void dataGridViewJanjiTemu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewJanjiTemu.Rows[e.RowIndex];
                txtIDJanjiTemu.Text = row.Cells["ID_JanjiTemu"].Value.ToString();
                txtTanggal.Text = Convert.ToDateTime(row.Cells["Tanggal"].Value).ToString("yyyy-MM-dd");

                txtIDHewan.Clear();
                txtID.Clear();
            }
        }

        private void ClearForm()
        {
            txtIDJanjiTemu.Clear();
            txtIDHewan.Clear();
            txtID.Clear();
            txtTanggal.Clear();
        }

        private void JanjiTemu_Load(object sender, EventArgs e)
        {

        }
    }
}
