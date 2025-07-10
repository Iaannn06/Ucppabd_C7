using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Ucppabd
{
    public partial class RekamMedis : Form
    {
        private Koneksi koneksi = new Koneksi();
        private string strKonek;

        private DataTable _rekamCache = null;
        private DateTime _cacheTime;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(10);

        public RekamMedis()
        {
            InitializeComponent();
            strKonek = koneksi.connectionString();
        }

        private void RekamMedis_Load(object sender, EventArgs e)
        {
            LoadData();
            dataGridViewRekamMedis.CellClick += dataGridViewRekamMedis_CellContentClick;
        }

        private void LoadData()
        {
            if (_rekamCache != null && (DateTime.Now - _cacheTime) < _cacheDuration)
            {
                dataGridViewRekamMedis.DataSource = _rekamCache;
                return;
            }
            try
            {
                using (var con = new SqlConnection(strKonek))
                using (var cmd = new SqlCommand("GetAllRekamMedis", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var da = new SqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    _rekamCache = dt;
                    _cacheTime = DateTime.Now;
                    dataGridViewRekamMedis.DataSource = _rekamCache;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data rekam medis: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtID.Text) ||
                string.IsNullOrWhiteSpace(txtIDHewan.Text) ||
                string.IsNullOrWhiteSpace(txtIDVaksin.Text) ||
                string.IsNullOrWhiteSpace(txtKeterangan.Text) || // Ditambahkan pengecekan Keterangan
                string.IsNullOrWhiteSpace(txtTanggal.Text))   // Ditambahkan pengecekan Tanggal
            {
                MessageBox.Show("Semua field wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!DateTime.TryParse(txtTanggal.Text, out DateTime tanggal))
            {
                MessageBox.Show("Format tanggal tidak valid.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (tanggal.Date > DateTime.Now.Date)
            {
                MessageBox.Show("Tanggal rekam medis tidak boleh di masa depan.", "Validasi Tanggal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var con = new SqlConnection(strKonek))
            {
                con.Open();

                // --- VALIDASI DATABASE SEBELUM INSERT ---
                // 1. Cek ID Rekam Medis duplikat
                var cmdCekId = new SqlCommand("SELECT COUNT(*) FROM dbo.RekamMedis WHERE ID = @ID", con);
                cmdCekId.Parameters.AddWithValue("@ID", txtID.Text.Trim());
                if ((int)cmdCekId.ExecuteScalar() > 0)
                {
                    MessageBox.Show("ID Rekam Medis sudah ada.", "Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Cek keberadaan ID Hewan
                var cmdCekHewan = new SqlCommand("SELECT COUNT(*) FROM dbo.Hewan WHERE ID_Hewan = @ID_Hewan", con);
                cmdCekHewan.Parameters.AddWithValue("@ID_Hewan", txtIDHewan.Text.Trim());
                if ((int)cmdCekHewan.ExecuteScalar() == 0)
                {
                    MessageBox.Show("ID Hewan tidak ditemukan.", "Data Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3. Cek keberadaan ID Vaksin
                var cmdCekVaksin = new SqlCommand("SELECT COUNT(*) FROM dbo.Vaksin WHERE ID_Vaksin = @ID_Vaksin", con);
                cmdCekVaksin.Parameters.AddWithValue("@ID_Vaksin", txtIDVaksin.Text.Trim());
                if ((int)cmdCekVaksin.ExecuteScalar() == 0)
                {
                    MessageBox.Show("ID Vaksin tidak ditemukan.", "Data Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // --- AKHIR VALIDASI ---

                var transaction = con.BeginTransaction();
                try
                {
                    using (var cmd = new SqlCommand("TambahRekamMedis", con, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", txtID.Text.Trim());
                        cmd.Parameters.AddWithValue("@ID_Hewan", txtIDHewan.Text.Trim());
                        cmd.Parameters.AddWithValue("@ID_Vaksin", txtIDVaksin.Text.Trim());
                        cmd.Parameters.AddWithValue("@Keterangan", txtKeterangan.Text.Trim());
                        cmd.Parameters.AddWithValue("@Tanggal", tanggal);
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    _rekamCache = null;
                    MessageBox.Show("Data berhasil ditambahkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearForm();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Gagal tambah: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewRekamMedis.CurrentRow == null)
            {
                MessageBox.Show("Pilih data yang akan diupdate!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!DateTime.TryParse(txtTanggal.Text, out DateTime tanggal))
            {
                MessageBox.Show("Format tanggal tidak valid.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (tanggal.Date > DateTime.Now.Date)
            {
                // Pesan ini yang seharusnya muncul jika validasi ini gagal
                MessageBox.Show("Tanggal rekam medis tidak boleh di masa depan.", "Validasi Tanggal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (var con = new SqlConnection(strKonek))
            {
                con.Open();
                // --- VALIDASI DATABASE SEBELUM UPDATE ---
                // Cek keberadaan ID Hewan
                var cmdCekHewan = new SqlCommand("SELECT COUNT(*) FROM dbo.Hewan WHERE ID_Hewan = @ID_Hewan", con);
                cmdCekHewan.Parameters.AddWithValue("@ID_Hewan", txtIDHewan.Text.Trim());
                if ((int)cmdCekHewan.ExecuteScalar() == 0)
                {
                    MessageBox.Show("ID Hewan tidak ditemukan.", "Data Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Cek keberadaan ID Vaksin
                var cmdCekVaksin = new SqlCommand("SELECT COUNT(*) FROM dbo.Vaksin WHERE ID_Vaksin = @ID_Vaksin", con);
                cmdCekVaksin.Parameters.AddWithValue("@ID_Vaksin", txtIDVaksin.Text.Trim());
                if ((int)cmdCekVaksin.ExecuteScalar() == 0)
                {
                    MessageBox.Show("ID Vaksin tidak ditemukan.", "Data Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // --- AKHIR VALIDASI ---

                var transaction = con.BeginTransaction();
                try
                {
                    using (var cmd = new SqlCommand("UpdateRekamMedis", con, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", txtID.Text.Trim());
                        cmd.Parameters.AddWithValue("@ID_Hewan", txtIDHewan.Text.Trim());
                        cmd.Parameters.AddWithValue("@ID_Vaksin", txtIDVaksin.Text.Trim());
                        cmd.Parameters.AddWithValue("@Keterangan", txtKeterangan.Text.Trim());
                        cmd.Parameters.AddWithValue("@Tanggal", tanggal);
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    _rekamCache = null;
                    MessageBox.Show("Data berhasil diperbarui.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearForm();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Gagal update: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewRekamMedis.CurrentRow == null)
            {
                MessageBox.Show("Pilih data yang akan dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Yakin ingin menghapus?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            string id = dataGridViewRekamMedis.CurrentRow.Cells["ID"].Value.ToString();

            using (var con = new SqlConnection(strKonek))
            {
                con.Open();
                var transaction = con.BeginTransaction();
                try
                {
                    using (var cmd = new SqlCommand("DeleteRekamMedis", con, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    _rekamCache = null;
                    MessageBox.Show("Data berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearForm();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Gagal hapus: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridViewRekamMedis_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            try
            {
                DataGridViewRow row = dataGridViewRekamMedis.Rows[e.RowIndex];
                txtID.Text = Convert.ToString(row.Cells["ID"].Value);
                txtIDHewan.Text = Convert.ToString(row.Cells["ID_Hewan"].Value);
                txtIDVaksin.Text = Convert.ToString(row.Cells["ID_Vaksin"].Value);
                txtKeterangan.Text = Convert.ToString(row.Cells["Keterangan"].Value);
                txtTanggal.Text = Convert.ToDateTime(row.Cells["Tanggal"].Value).ToString("yyyy-MM-dd");
                
                // Kunci kolom ID saat mode update
                txtID.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memilih data: " + ex.Message);
            }
        }

        private void ClearForm()
        {
            txtID.Clear();
            txtIDHewan.Clear();
            txtIDVaksin.Clear();
            txtKeterangan.Clear();
            txtTanggal.Clear();
            
            // Buka kembali kunci ID untuk input data baru
            txtID.ReadOnly = false;
        }
    }
}