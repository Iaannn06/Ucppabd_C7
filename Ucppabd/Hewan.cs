using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Ucppabd
{
    public partial class Hewan : Form
    {
        // 1. Membuat instance dari kelas Koneksi dan variabel string koneksi
        private Koneksi koneksi = new Koneksi();
        private string strKonek;

        private DataTable _hewanCache = null;
        private DateTime _cacheTime;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(10);

        public Hewan()
        {
            InitializeComponent();

            strKonek = koneksi.connectionString();

            LoadData();
            dataGridViewHewan.CellClick += dataGridViewHewan_CellClick;

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
                // 3. Menggunakan 'strKonek' dan Stored Procedure 'GetAllHewan'
                using (var con = new SqlConnection(strKonek))
                using (var cmd = new SqlCommand("GetAllHewan", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var da = new SqlDataAdapter(cmd);
                    var dt = new DataTable();
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

        // --- Sisa kode di bawah ini sudah benar, hanya perlu dipastikan ---
        // --- mereka menggunakan 'strKonek' untuk koneksi. ---

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDHewan.Text) || string.IsNullOrWhiteSpace(txtIDPemilik.Text) || string.IsNullOrWhiteSpace(txtNama.Text))
            {
                MessageBox.Show("ID Hewan, ID Pemilik, Nama, Jenis, Dan Umur wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(txtUmur.Text, out int umurAngka))
            {
                MessageBox.Show("Format umur tidak valid. Harap masukkan angka.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(txtNama.Text, "^[a-zA-Z\\s]+$"))
            {
                MessageBox.Show("Nama hewan hanya boleh mengandung huruf dan spasi.", "Validasi Nama", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string polaNama = "^[a-zA-Z\\s]+$"; // Pola hanya huruf dan spasi
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtNama.Text, polaNama))
            {
                MessageBox.Show("Nama hewan hanya boleh mengandung huruf dan spasi.", "Validasi Nama", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtJenis.Text, polaNama))
            {
                MessageBox.Show("Jenis hewan hanya boleh mengandung huruf dan spasi.", "Validasi Jenis", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string satuanUmur = cmbSatuanUmur.SelectedItem.ToString();

            using (var con = new SqlConnection(strKonek))
            {
                con.Open();
                var transaction = con.BeginTransaction();
                try
                {
                    using (var cmd = new SqlCommand("AddHewan", con, transaction))
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

            if (!System.Text.RegularExpressions.Regex.IsMatch(txtNama.Text, "^[a-zA-Z\\s]+$"))
            {
                MessageBox.Show("Nama hewan hanya boleh mengandung huruf dan spasi.", "Validasi Nama", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string polaNama = "^[a-zA-Z\\s]+$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtNama.Text, polaNama))
            {
                MessageBox.Show("Nama hewan hanya boleh mengandung huruf dan spasi.", "Validasi Nama", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtJenis.Text, polaNama))
            {
                MessageBox.Show("Jenis hewan hanya boleh mengandung huruf dan spasi.", "Validasi Jenis", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            string satuanUmur = cmbSatuanUmur.SelectedItem.ToString();

            using (var con = new SqlConnection(strKonek))
            {
                con.Open();
                var transaction = con.BeginTransaction();
                try
                {
                    using (var cmd = new SqlCommand("UpdateHewan", con, transaction))
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewHewan.CurrentRow == null)
            {
                MessageBox.Show("Pilih data yang akan dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Yakin ingin menghapus data hewan ini? Menghapus hewan juga akan menghapus janji temu dan rekam medis terkait.", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string id = dataGridViewHewan.CurrentRow.Cells["ID_Hewan"].Value.ToString();

                using (var con = new SqlConnection(strKonek))
                {
                    con.Open();
                    var transaction = con.BeginTransaction();
                    try
                    {
                        using (var cmd = new SqlCommand("DeleteHewan", con, transaction))
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

        // DIUBAH: Nama method dan ditambahkan try-catch serta konversi yang lebih aman
        private void dataGridViewHewan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Pengecekan agar tidak error saat header diklik
            if (e.RowIndex < 0) return;

            try
            {
                DataGridViewRow row = dataGridViewHewan.Rows[e.RowIndex];

                // Mengisi textbox lain yang kolomnya sudah pasti ada
                txtIDHewan.Text = Convert.ToString(row.Cells["ID_Hewan"].Value);
                txtIDPemilik.Text = Convert.ToString(row.Cells["ID_Pemilik"].Value);
                txtNama.Text = Convert.ToString(row.Cells["Nama"].Value);
                txtJenis.Text = Convert.ToString(row.Cells["Jenis"].Value);
                txtUmur.Text = Convert.ToString(row.Cells["Umur"].Value);

                // --- PERUBAHAN DI SINI ---
                // Cek dulu apakah kolom "SatuanUmur" ada di dalam DataGridView
                if (dataGridViewHewan.Columns.Contains("SatuanUmur"))
                {
                    // Jika kolomnya ada, baru jalankan kode untuk ComboBox
                    if (row.Cells["SatuanUmur"].Value != DBNull.Value)
                    {
                        cmbSatuanUmur.SelectedItem = Convert.ToString(row.Cells["SatuanUmur"].Value);
                    }
                    else
                    {
                        cmbSatuanUmur.SelectedIndex = 2; // Default "Tahun"
                    }
                }
                else
                {
                    // Jika kolom tidak ada sama sekali, cukup atur ComboBox ke nilai default
                    cmbSatuanUmur.SelectedIndex = 2; // Default "Tahun"
                }
            }
            catch (Exception ex)
            {
                // Jika terjadi error tak terduga, tampilkan pesan
                MessageBox.Show("Terjadi kesalahan saat memilih data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void Hewan_Load_1(object sender, EventArgs e) { }
    }
}