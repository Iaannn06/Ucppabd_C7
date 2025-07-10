using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Ucppabd
{
    public partial class JanjiTemu : Form
    {
        private Koneksi koneksi = new Koneksi();
        private string strKonek;

        private DataTable _janjiTemuCache = null;
        private DateTime _cacheTime;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(10);
        private string _selectedJanjiTemuId = null;

        public JanjiTemu()
        {
            InitializeComponent();
            strKonek = koneksi.connectionString(); // Asumsi Koneksi.cs sudah standar
            LoadData();
            dataGridViewJanjiTemu.CellClick += dataGridViewJanjiTemu_CellClick;
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
                using (var con = new SqlConnection(strKonek))
                using (var cmd = new SqlCommand("GetAllJanjiTemuDetail", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var da = new SqlDataAdapter(cmd);
                    var dt = new DataTable();
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
            if (string.IsNullOrWhiteSpace(txtIDJanjiTemu.Text) || string.IsNullOrWhiteSpace(txtIDHewan.Text) || string.IsNullOrWhiteSpace(txtID.Text) || string.IsNullOrWhiteSpace(txtTanggal.Text))
            {
                MessageBox.Show("Semua ID dan Tanggal wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!DateTime.TryParse(txtTanggal.Text, out DateTime tanggal))
            {
                MessageBox.Show("Format tanggal tidak valid (contoh: 2025-06-10)", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (tanggal.Date < DateTime.Now.Date)
            {
                MessageBox.Show("Tanggal janji temu tidak boleh di masa lalu.", "Validasi Tanggal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var con = new SqlConnection(strKonek))
            {
                con.Open();

                // Validasi data duplikat dan keberadaan Foreign Key
                var cmdCekId = new SqlCommand("SELECT COUNT(*) FROM dbo.JanjiTemu WHERE ID_JanjiTemu = @ID", con);
                cmdCekId.Parameters.AddWithValue("@ID", txtIDJanjiTemu.Text.Trim());
                if ((int)cmdCekId.ExecuteScalar() > 0)
                {
                    MessageBox.Show("ID Janji Temu sudah ada.", "Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var cmdCekHewan = new SqlCommand("SELECT COUNT(*) FROM dbo.Hewan WHERE ID_Hewan = @ID_Hewan", con);
                cmdCekHewan.Parameters.AddWithValue("@ID_Hewan", txtIDHewan.Text.Trim());
                if ((int)cmdCekHewan.ExecuteScalar() == 0)
                {
                    MessageBox.Show("ID Hewan tidak ditemukan.", "Data Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var cmdCekDokter = new SqlCommand("SELECT COUNT(*) FROM dbo.Dokter WHERE ID = @ID_Dokter", con);
                cmdCekDokter.Parameters.AddWithValue("@ID_Dokter", txtID.Text.Trim());
                if ((int)cmdCekDokter.ExecuteScalar() == 0)
                {
                    MessageBox.Show("ID Dokter tidak ditemukan.", "Data Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var transaction = con.BeginTransaction();
                try
                {
                    using (var cmd = new SqlCommand("AddJanjiTemu", con, transaction))
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
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedJanjiTemuId))
            {
                MessageBox.Show("Pilih data janji temu yang akan diupdate dari tabel!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!DateTime.TryParse(txtTanggal.Text, out DateTime tanggal))
            {
                MessageBox.Show("Format tanggal tidak valid (contoh: 2025-07-07).", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (tanggal.Date < DateTime.Now.Date)
            {
                MessageBox.Show("Tanggal janji temu tidak boleh di masa lalu.", "Validasi Tanggal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var con = new SqlConnection(strKonek))
            {
                con.Open();

                // Validasi keberadaan Foreign Key sebelum update
                var cmdCekHewan = new SqlCommand("SELECT COUNT(*) FROM dbo.Hewan WHERE ID_Hewan = @ID_Hewan", con);
                cmdCekHewan.Parameters.AddWithValue("@ID_Hewan", txtIDHewan.Text.Trim());
                if ((int)cmdCekHewan.ExecuteScalar() == 0)
                {
                    MessageBox.Show("ID Hewan tidak ditemukan.", "Data Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var cmdCekDokter = new SqlCommand("SELECT COUNT(*) FROM dbo.Dokter WHERE ID = @ID_Dokter", con);
                cmdCekDokter.Parameters.AddWithValue("@ID_Dokter", txtID.Text.Trim());
                if ((int)cmdCekDokter.ExecuteScalar() == 0)
                {
                    MessageBox.Show("ID Dokter tidak ditemukan.", "Data Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var transaction = con.BeginTransaction();
                try
                {
                    using (var cmd = new SqlCommand("UpdateJanjiTemuFull", con, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Old_ID_JanjiTemu", _selectedJanjiTemuId);
                        cmd.Parameters.AddWithValue("@New_ID_JanjiTemu", txtIDJanjiTemu.Text.Trim());
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
                    MessageBox.Show("Error saat memperbarui data: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (MessageBox.Show("Yakin ingin menghapus janji temu ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            string id = dataGridViewJanjiTemu.CurrentRow.Cells["ID_JanjiTemu"].Value.ToString();

            using (var con = new SqlConnection(strKonek))
            {
                con.Open();
                var transaction = con.BeginTransaction();
                try
                {
                    using (var cmd = new SqlCommand("DeleteJanjiTemu", con, transaction))
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

        private void dataGridViewJanjiTemu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            try
            {
                DataGridViewRow row = dataGridViewJanjiTemu.Rows[e.RowIndex];
                _selectedJanjiTemuId = Convert.ToString(row.Cells["ID_JanjiTemu"].Value);
                txtIDJanjiTemu.Text = Convert.ToString(row.Cells["ID_JanjiTemu"].Value);
                txtIDHewan.Text = Convert.ToString(row.Cells["ID_Hewan"].Value);
                txtID.Text = Convert.ToString(row.Cells["ID_Dokter"].Value);
                txtTanggal.Text = Convert.ToDateTime(row.Cells["Tanggal"].Value).ToString("yyyy-MM-dd");

                // Kunci ID saat update
                txtIDJanjiTemu.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memilih data: " + ex.Message);
            }
        }

        private void ClearForm()
        {
            txtIDJanjiTemu.Clear();
            txtIDHewan.Clear();
            txtID.Clear();
            txtTanggal.Clear();
            _selectedJanjiTemuId = null;

            // Buka kunci ID untuk data baru
            txtIDJanjiTemu.ReadOnly = false;
        }

        private void JanjiTemu_Load(object sender, EventArgs e) { }
    }
}