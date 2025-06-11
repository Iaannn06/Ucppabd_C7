using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Ucppabd
{
    public partial class Hewan : Form
    {
        static string connectionString = "Data Source=DESKTOP-L9CBIM9\\SQLEXPRESS01;Initial Catalog=ProjecctPABD;Integrated Security=True";
        private DataTable _hewanCache = null;
        private DateTime _cacheTime;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(10);

        public Hewan()
        {
            InitializeComponent();
            LoadData();
            dataGridViewHewan.CellClick += dataGridViewHewan_CellContentClick;

            if (cmbSatuanUmur.Items.Count == 0)
            {
                cmbSatuanUmur.Items.AddRange(new string[] { "Hari", "Bulan", "Tahun" });
                cmbSatuanUmur.SelectedIndex = 2;
            }
        }

        private void LoadData()
        {
            if (_hewanCache != null && (DateTime.Now - _cacheTime) < _cacheDuration)
            {
                dataGridViewHewan.DataSource = _hewanCache;
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT ID_Hewan, ID_Pemilik, Nama, Jenis, Umur, SatuanUmur FROM Hewan";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    _hewanCache = dt;
                    _cacheTime = DateTime.Now;
                    dataGridViewHewan.DataSource = _hewanCache;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDHewan.Text) || string.IsNullOrWhiteSpace(txtIDPemilik.Text) || string.IsNullOrWhiteSpace(txtNama.Text))
            {
                MessageBox.Show("ID Hewan, ID Pemilik, dan Nama wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtUmur.Text, out int umurAngka))
            {
                MessageBox.Show("Format umur tidak valid. Harap masukkan angka.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string satuanUmur = cmbSatuanUmur.SelectedItem.ToString();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("AddHewan", con, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_Hewan", txtIDHewan.Text.Trim());
                        cmd.Parameters.AddWithValue("@ID_Pemilik", txtIDPemilik.Text.Trim());
                        cmd.Parameters.AddWithValue("@Nama", txtNama.Text.Trim());
                        cmd.Parameters.AddWithValue("@Jenis", txtJenis.Text.Trim());
                        cmd.Parameters.AddWithValue("@Umur", umurAngka);
                        cmd.Parameters.AddWithValue("@SatuanUmur", satuanUmur);

                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    _hewanCache = null;
                    MessageBox.Show("Data hewan berhasil ditambahkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (dataGridViewHewan.CurrentRow == null)
            {
                MessageBox.Show("Pilih data yang akan dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("Yakin ingin menghapus data hewan ini? Menghapus hewan juga akan menghapus janji temu terkait.", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                string id = dataGridViewHewan.CurrentRow.Cells["ID_Hewan"].Value.ToString();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction();
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("DeleteHewan", con, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ID_Hewan", id);
                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                        _hewanCache = null;
                        MessageBox.Show("Data berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewHewan.CurrentRow == null)
            {
                MessageBox.Show("Pilih data yang akan diupdate!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtUmur.Text, out int umurAngka))
            {
                MessageBox.Show("Format umur tidak valid. Harap masukkan angka.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string satuanUmur = cmbSatuanUmur.SelectedItem.ToString();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("UpdateHewan", con, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_Hewan", txtIDHewan.Text.Trim());
                        cmd.Parameters.AddWithValue("@ID_Pemilik", txtIDPemilik.Text.Trim());
                        cmd.Parameters.AddWithValue("@Nama", txtNama.Text.Trim());
                        cmd.Parameters.AddWithValue("@Jenis", txtJenis.Text.Trim());
                        cmd.Parameters.AddWithValue("@Umur", umurAngka);
                        cmd.Parameters.AddWithValue("@SatuanUmur", satuanUmur);
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    _hewanCache = null;
                    MessageBox.Show("Data berhasil diperbarui.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void dataGridViewHewan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewHewan.Rows[e.RowIndex];

                txtIDHewan.Text = row.Cells["ID_Hewan"].Value.ToString();
                txtIDPemilik.Text = row.Cells["ID_Pemilik"].Value.ToString();
                txtNama.Text = row.Cells["Nama"].Value.ToString();
                txtJenis.Text = row.Cells["Jenis"].Value.ToString();
                txtUmur.Text = row.Cells["Umur"].Value.ToString();

                if (row.Cells["SatuanUmur"].Value != DBNull.Value)
                {
                    cmbSatuanUmur.SelectedItem = row.Cells["SatuanUmur"].Value.ToString();
                }
                else
                {
                    cmbSatuanUmur.SelectedIndex = 2;
                }
            }
        }

        private void ClearForm()
        {
            txtIDHewan.Clear();
            txtIDPemilik.Clear();
            txtNama.Clear();
            txtJenis.Clear();
            txtUmur.Clear();
            cmbSatuanUmur.SelectedIndex = 2;
        }

        private void Hewan_Load(object sender, EventArgs e) { }
    }
}

