using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Ucppabd
{
    public partial class Dokter : Form
    {
        // 1. Membuat instance dari kelas Koneksi dan variabel string koneksi
        private Koneksi koneksi = new Koneksi();
        private string strKonek;

        private DataTable _dokterCache = null;
        private DateTime _cacheTime;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(10);

        public Dokter()
        {
            InitializeComponent();

            // 2. Mengambil connection string dari kelas Koneksi
            strKonek = koneksi.connectionString();

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

            // Validasi Nama: Hanya huruf, spasi, titik, dan strip
            if (!Regex.IsMatch(txtNama.Text, "^[a-zA-Z\\s.-]+$"))
            {
                MessageBox.Show("Nama hanya boleh mengandung huruf, spasi, titik, dan strip.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // --- VALIDASI BARU ---
            // Validasi Spesialisasi: Hanya huruf dan spasi
            if (!Regex.IsMatch(txtSpesialisasi.Text, "^[a-zA-Z\\s]+$"))
            {
                MessageBox.Show("Spesialisasi hanya boleh mengandung huruf dan spasi.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validasi Telepon: Hanya angka, 10-13 digit
            if (!Regex.IsMatch(txtTelepon.Text, "^\\d{10,13}$"))
            {
                MessageBox.Show("Nomor telepon harus berupa angka 10-13 digit.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        // --- Sisa kode (btnTambah, btnUpdate, dll.) tidak diubah ---
        // --- karena mereka sudah memanggil method ValidasiInput() ---

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (!ValidasiInput()) return;

            using (var con = new SqlConnection(strKonek))
            {
                con.Open();
                using (var tx = con.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = new SqlCommand("AddDokter", con, tx))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ID", txtID.Text.Trim());
                            cmd.Parameters.AddWithValue("@Nama", txtNama.Text.Trim());
                            cmd.Parameters.AddWithValue("@Spesialisasi", txtSpesialisasi.Text.Trim());
                            cmd.Parameters.AddWithValue("@Telepon", txtTelepon.Text.Trim());
                            cmd.ExecuteNonQuery();
                        }
                        tx.Commit();
                        _dokterCache = null;
                        MessageBox.Show("Data dokter berhasil ditambahkan.");
                        LoadData();
                        ClearForm();
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        MessageBox.Show("Gagal menambahkan data: " + ex.Message);
                    }
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

            using (var con = new SqlConnection(strKonek))
            {
                con.Open();
                using (var tx = con.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = new SqlCommand("UpdateDokter", con, tx))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ID", txtID.Text.Trim());
                            cmd.Parameters.AddWithValue("@Nama", txtNama.Text.Trim());
                            cmd.Parameters.AddWithValue("@Spesialisasi", txtSpesialisasi.Text.Trim());
                            cmd.Parameters.AddWithValue("@Telepon", txtTelepon.Text.Trim());
                            cmd.ExecuteNonQuery();
                        }
                        tx.Commit();
                        _dokterCache = null;
                        MessageBox.Show("Data berhasil diperbarui.");
                        LoadData();
                        ClearForm();
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        MessageBox.Show("Gagal memperbarui data: " + ex.Message);
                    }
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
            txtID.ReadOnly = true; // Kunci ID saat update
        }

        private void ClearForm()
        {
            txtID.Clear();
            txtNama.Clear();
            txtSpesialisasi.Clear();
            txtTelepon.Clear();
            txtID.ReadOnly = false; // Buka kunci ID untuk data baru
        }

        private void Dokter_Load(object sender, EventArgs e) { }
    }
}