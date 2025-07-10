using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Ucppabd
{
    public partial class Dokter : Form
    {
        private Koneksi koneksi = new Koneksi();
        private string strKonek;

        private DataTable _dokterCache = null;
        private DateTime _cacheTime;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(10);

        public Dokter()
        {
            InitializeComponent();
            strKonek = koneksi.connectionString(); // Asumsi Koneksi.cs sudah standar
            LoadData();
            dataGridViewDokter.CellClick += dataGridViewDokter_CellContentClick;
        }

        private void LoadData()
        {
            if (_dokterCache != null && (DateTime.Now - _cacheTime) < _cacheDuration)
            {
                dataGridViewDokter.DataSource = _dokterCache;
                return;
            }
            try
            {
                using (var con = new SqlConnection(strKonek))
                using (var cmd = new SqlCommand("GetAllDokter", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var da = new SqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    _dokterCache = dt;
                    _cacheTime = DateTime.Now;
                    dataGridViewDokter.DataSource = _dokterCache;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error memuat data: " + ex.Message);
            }
        }

        private bool ValidasiInput()
        {
            if (string.IsNullOrWhiteSpace(txtID.Text) || string.IsNullOrWhiteSpace(txtNama.Text) || string.IsNullOrWhiteSpace(txtSpesialisasi.Text) || string.IsNullOrWhiteSpace(txtTelepon.Text))
            {
                MessageBox.Show("Semua field wajib diisi.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!Regex.IsMatch(txtNama.Text, @"^[a-zA-Z\s.-]+$"))
            {
                MessageBox.Show("Nama hanya boleh mengandung huruf, spasi, titik, dan strip.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!Regex.IsMatch(txtSpesialisasi.Text, @"^[a-zA-Z\s]+$"))
            {
                MessageBox.Show("Spesialisasi hanya boleh mengandung huruf dan spasi.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!Regex.IsMatch(txtTelepon.Text, @"^\d{10,13}$"))
            {
                MessageBox.Show("Nomor telepon harus berupa angka dengan panjang 10-13 digit.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (!ValidasiInput()) return;

            using (var con = new SqlConnection(strKonek))
            {
                con.Open();

                // Validasi duplikat sebelum insert
                var cmdId = new SqlCommand("SELECT COUNT(*) FROM dbo.Dokter WHERE ID = @ID", con);
                cmdId.Parameters.AddWithValue("@ID", txtID.Text.Trim());
                if ((int)cmdId.ExecuteScalar() > 0)
                {
                    MessageBox.Show("ID Dokter sudah terdaftar.", "Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var cmdTelepon = new SqlCommand("SELECT COUNT(*) FROM dbo.Dokter WHERE Telepon = @Telepon", con);
                cmdTelepon.Parameters.AddWithValue("@Telepon", txtTelepon.Text.Trim());
                if ((int)cmdTelepon.ExecuteScalar() > 0)
                {
                    MessageBox.Show("Nomor telepon sudah terdaftar.", "Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var transaction = con.BeginTransaction();
                try
                {
                    using (var cmd = new SqlCommand("AddDokter", con, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", txtID.Text.Trim());
                        cmd.Parameters.AddWithValue("@Nama", txtNama.Text.Trim());
                        cmd.Parameters.AddWithValue("@Spesialisasi", txtSpesialisasi.Text.Trim());
                        cmd.Parameters.AddWithValue("@Telepon", txtTelepon.Text.Trim());
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    _dokterCache = null;
                    MessageBox.Show("Data dokter berhasil ditambahkan.");
                    LoadData();
                    ClearForm();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Gagal menambahkan data: " + ex.Message);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewDokter.CurrentRow == null)
            {
                MessageBox.Show("Pilih data yang akan diupdate.");
                return;
            }
            if (!ValidasiInput()) return;

            string currentId = txtID.Text.Trim();
            string newTelepon = txtTelepon.Text.Trim();
            string originalTelepon = dataGridViewDokter.CurrentRow.Cells["Telepon"].Value.ToString();

            using (var con = new SqlConnection(strKonek))
            {
                con.Open();

                if (newTelepon != originalTelepon)
                {
                    var cmdCekTelepon = new SqlCommand("SELECT COUNT(*) FROM dbo.Dokter WHERE Telepon = @Telepon", con);
                    cmdCekTelepon.Parameters.AddWithValue("@Telepon", newTelepon);
                    if ((int)cmdCekTelepon.ExecuteScalar() > 0)
                    {
                        MessageBox.Show("Nomor telepon sudah digunakan oleh dokter lain.", "Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                var transaction = con.BeginTransaction();
                try
                {
                    using (var cmd = new SqlCommand("UpdateDokter", con, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", currentId);
                        cmd.Parameters.AddWithValue("@Nama", txtNama.Text.Trim());
                        cmd.Parameters.AddWithValue("@Spesialisasi", txtSpesialisasi.Text.Trim());
                        cmd.Parameters.AddWithValue("@Telepon", newTelepon);
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    _dokterCache = null;
                    MessageBox.Show("Data berhasil diperbarui.");
                    LoadData();
                    ClearForm();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Gagal memperbarui data: " + ex.Message);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewDokter.CurrentRow == null)
            {
                MessageBox.Show("Pilih data yang akan dihapus.");
                return;
            }
            if (MessageBox.Show("Yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            string id = dataGridViewDokter.CurrentRow.Cells["ID"].Value.ToString();

            using (var con = new SqlConnection(strKonek))
            {
                con.Open();
                using (var tx = con.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = new SqlCommand("DeleteDokter", con, tx))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ID", id);
                            cmd.ExecuteNonQuery();
                        }
                        tx.Commit();
                        _dokterCache = null;
                        MessageBox.Show("Data berhasil dihapus.");
                        LoadData();
                        ClearForm();
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        MessageBox.Show("Gagal menghapus data: " + ex.Message);
                    }
                }
            }
        }

        private void dataGridViewDokter_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dataGridViewDokter.Rows[e.RowIndex];
            txtID.Text = row.Cells["ID"].Value.ToString();
            txtNama.Text = row.Cells["Nama"].Value.ToString();
            txtSpesialisasi.Text = row.Cells["Spesialisasi"].Value.ToString();
            txtTelepon.Text = row.Cells["Telepon"].Value.ToString();
            txtID.ReadOnly = true;
        }

        private void ClearForm()
        {
            txtID.Clear();
            txtNama.Clear();
            txtSpesialisasi.Clear();
            txtTelepon.Clear();
            txtID.ReadOnly = false;
        }

        private void Dokter_Load(object sender, EventArgs e) { }
    }
}