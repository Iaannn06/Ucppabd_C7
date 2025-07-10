using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Ucppabd
{
    public partial class Pemilik : Form
    {
        private Koneksi koneksi = new Koneksi();
        private string strKonek;

        private DataTable _pemilikCache = null;
        private DateTime _cacheTime;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(10);

        public Pemilik()
        {
            InitializeComponent();
            strKonek = koneksi.connectionString(); 
            LoadData();
            dataGridViewPemilik.CellClick += DataGridViewPemilik_CellClick;
        }

        private void LoadData()
        {
            if (_pemilikCache != null && (DateTime.Now - _cacheTime) < _cacheDuration)
            {
                dataGridViewPemilik.DataSource = _pemilikCache;
                return;
            }
            try
            {
                using (var con = new SqlConnection(strKonek))
                using (var cmd = new SqlCommand("GetAllPemilik", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var da = new SqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    _pemilikCache = dt;
                    _cacheTime = DateTime.Now;
                    dataGridViewPemilik.DataSource = _pemilikCache;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- METHOD VALIDASI BARU ---
        private bool ValidasiInput()
        {
            // 1. Cek semua field wajib diisi
            if (string.IsNullOrWhiteSpace(txtIDPemilik.Text) ||
                string.IsNullOrWhiteSpace(txtNama.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtTelepon.Text))
            {
                MessageBox.Show("Semua field wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // 2. Validasi format Nama (hanya huruf, spasi, titik, strip)
            if (!Regex.IsMatch(txtNama.Text, @"^[a-zA-Z\s.-]+$"))
            {
                MessageBox.Show("Nama hanya boleh mengandung huruf, spasi, titik, dan strip.", "Validasi Nama", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // 3. Validasi format Email (wajib ada @ dan .)
            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                MessageBox.Show("Format email tidak valid. Pastikan mengandung '@' dan '.'.", "Validasi Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // 4. Validasi format Telepon (hanya angka, 10-13 digit)
            if (!Regex.IsMatch(txtTelepon.Text, @"^\d{10,13}$"))
            {
                MessageBox.Show("Nomor telepon harus berupa angka dengan panjang 10-13 digit.", "Validasi Telepon", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true; // Semua validasi lolos
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            // Panggil method validasi utama
            if (!ValidasiInput()) return;

            using (var con = new SqlConnection(strKonek))
            {
                con.Open();

                // Validasi duplikat data sebelum insert
                var cmdId = new SqlCommand("SELECT COUNT(*) FROM dbo.Pemilik WHERE ID_Pemilik = @ID_Pemilik", con);
                cmdId.Parameters.AddWithValue("@ID_Pemilik", txtIDPemilik.Text.Trim());
                if ((int)cmdId.ExecuteScalar() > 0)
                {
                    MessageBox.Show("ID Pemilik sudah terdaftar.", "Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var cmdEmail = new SqlCommand("SELECT COUNT(*) FROM dbo.Pemilik WHERE Email = @Email", con);
                cmdEmail.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                if ((int)cmdEmail.ExecuteScalar() > 0)
                {
                    MessageBox.Show("Email sudah terdaftar.", "Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var cmdTelepon = new SqlCommand("SELECT COUNT(*) FROM dbo.Pemilik WHERE Telepon = @Telepon", con);
                cmdTelepon.Parameters.AddWithValue("@Telepon", txtTelepon.Text.Trim());
                if ((int)cmdTelepon.ExecuteScalar() > 0)
                {
                    MessageBox.Show("Nomor telepon sudah terdaftar.", "Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

            

                var transaction = con.BeginTransaction();
                try
                {
                    using (var cmd = new SqlCommand("AddPemilik", con, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_Pemilik", txtIDPemilik.Text.Trim());
                        cmd.Parameters.AddWithValue("@Nama", txtNama.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@Telepon", txtTelepon.Text.Trim());
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    _pemilikCache = null;
                    MessageBox.Show("Data pemilik berhasil ditambahkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewPemilik.CurrentRow == null)
            {
                MessageBox.Show("Pilih data yang akan diupdate!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Panggil method validasi utama
            if (!ValidasiInput()) return;

            string currentId = txtIDPemilik.Text.Trim();
            string newEmail = txtEmail.Text.Trim();
            string newTelepon = txtTelepon.Text.Trim();

            string originalEmail = dataGridViewPemilik.CurrentRow.Cells["Email"].Value.ToString();
            string originalTelepon = dataGridViewPemilik.CurrentRow.Cells["Telepon"].Value.ToString();

            using (var con = new SqlConnection(strKonek))
            {
                con.Open();

                // Validasi duplikat cerdas: hanya cek jika data diubah
                if (newEmail != originalEmail)
                {
                    var cmdEmail = new SqlCommand("SELECT COUNT(*) FROM dbo.Pemilik WHERE Email = @Email", con);
                    cmdEmail.Parameters.AddWithValue("@Email", newEmail);
                    if ((int)cmdEmail.ExecuteScalar() > 0)
                    {
                        MessageBox.Show("Email sudah terdaftar untuk pemilik lain.", "Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                if (newTelepon != originalTelepon)
                {
                    var cmdTelepon = new SqlCommand("SELECT COUNT(*) FROM dbo.Pemilik WHERE Telepon = @Telepon", con);
                    cmdTelepon.Parameters.AddWithValue("@Telepon", newTelepon);
                    if ((int)cmdTelepon.ExecuteScalar() > 0)
                    {
                        MessageBox.Show("Nomor telepon sudah terdaftar untuk pemilik lain.", "Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                var transaction = con.BeginTransaction();
                try
                {
                    using (var cmd = new SqlCommand("UpdatePemilik", con, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_Pemilik", currentId);
                        cmd.Parameters.AddWithValue("@Nama", txtNama.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", newEmail);
                        cmd.Parameters.AddWithValue("@Telepon", newTelepon);
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    _pemilikCache = null;
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewPemilik.CurrentRow == null)
            {
                MessageBox.Show("Pilih data yang akan dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Yakin ingin menghapus data pemilik ini? Ini akan gagal jika pemilik masih memiliki hewan.", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            string id = dataGridViewPemilik.CurrentRow.Cells["ID_Pemilik"].Value.ToString();

            using (var con = new SqlConnection(strKonek))
            {
                con.Open();
                var transaction = con.BeginTransaction();
                try
                {
                    using (var cmd = new SqlCommand("DeletePemilik", con, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_Pemilik", id);
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    _pemilikCache = null;
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

        private void DataGridViewPemilik_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridViewPemilik.Rows[e.RowIndex];
            txtIDPemilik.Text = Convert.ToString(row.Cells["ID_Pemilik"].Value);
            txtNama.Text = Convert.ToString(row.Cells["Nama"].Value);
            txtEmail.Text = Convert.ToString(row.Cells["Email"].Value);
            txtTelepon.Text = Convert.ToString(row.Cells["Telepon"].Value);

            txtIDPemilik.ReadOnly = true;
        }

        private void ClearForm()
        {
            txtIDPemilik.Clear();
            txtNama.Clear();
            txtEmail.Clear();
            txtTelepon.Clear();

            txtIDPemilik.ReadOnly = false;
        }

        private void Pemilik_Load(object sender, EventArgs e) { }
    }
}