using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Ucppabd
{
    public partial class Vaksin : Form
    {
        // 1. Membuat instance dari kelas Koneksi dan variabel string koneksi
        private Koneksi koneksi = new Koneksi();
        private string strKonek;

        private DataTable _vaksinCache = null;
        private DateTime _cacheTime;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(10);

        public Vaksin()
        {
            InitializeComponent();

            // 2. Mengambil connection string dari kelas Koneksi
            strKonek = koneksi.connectionString();

            LoadData();
            dataGridViewVaksin.CellClick += dataGridViewVaksin_CellContentClick;
        }

        private void LoadData()
        {
            if (_vaksinCache != null && (DateTime.Now - _cacheTime) < _cacheDuration)
            {
                dataGridViewVaksin.DataSource = _vaksinCache;
                return;
            }
            try
            {
                using (var con = new SqlConnection(strKonek))
                using (var cmd = new SqlCommand("GetAllVaksin", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var da = new SqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    _vaksinCache = dt;
                    _cacheTime = DateTime.Now;
                    dataGridViewVaksin.DataSource = _vaksinCache;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDVaksin.Text) || string.IsNullOrWhiteSpace(txtNamaVaksin.Text) || string.IsNullOrWhiteSpace(txtTanggalKadaluarsa.Text))
            {
                MessageBox.Show("Semua field wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!DateTime.TryParse(txtTanggalKadaluarsa.Text, out DateTime tanggalKadaluarsa))
            {
                MessageBox.Show("Format tanggal tidak valid.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (tanggalKadaluarsa.Date <= DateTime.Now.Date)
            {
                MessageBox.Show("Tanggal kadaluarsa harus di masa depan.", "Validasi Tanggal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var con = new SqlConnection(strKonek))
            {
                con.Open();

                // Validasi duplikat ID dan Nama Vaksin
                var cmdCekId = new SqlCommand("SELECT COUNT(*) FROM dbo.Vaksin WHERE ID_Vaksin = @ID_Vaksin", con);
                cmdCekId.Parameters.AddWithValue("@ID_Vaksin", txtIDVaksin.Text.Trim());
                if ((int)cmdCekId.ExecuteScalar() > 0)
                {
                    MessageBox.Show("ID Vaksin sudah terdaftar.", "Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var cmdCekNama = new SqlCommand("SELECT COUNT(*) FROM dbo.Vaksin WHERE NamaVaksin = @NamaVaksin", con);
                cmdCekNama.Parameters.AddWithValue("@NamaVaksin", txtNamaVaksin.Text.Trim());
                if ((int)cmdCekNama.ExecuteScalar() > 0)
                {
                    MessageBox.Show("Nama vaksin sudah terdaftar.", "Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!System.Text.RegularExpressions.Regex.IsMatch(txtNamaVaksin.Text, "^[a-zA-Z\\s]+$"))
                {
                    MessageBox.Show("Nama vaksin hanya boleh mengandung huruf dan spasi.", "Validasi Nama", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                var transaction = con.BeginTransaction();
                try
                {
                    using (var cmd = new SqlCommand("AddVaksin", con, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_Vaksin", txtIDVaksin.Text.Trim());
                        cmd.Parameters.AddWithValue("@NamaVaksin", txtNamaVaksin.Text.Trim());
                        cmd.Parameters.AddWithValue("@TanggalKadaluarsa", tanggalKadaluarsa);
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    _vaksinCache = null;
                    MessageBox.Show("Data vaksin berhasil ditambahkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (dataGridViewVaksin.CurrentRow == null)
            {
                MessageBox.Show("Pilih data yang akan diupdate!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!DateTime.TryParse(txtTanggalKadaluarsa.Text, out DateTime tanggalKadaluarsa))
            {
                MessageBox.Show("Format tanggal tidak valid.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (tanggalKadaluarsa.Date <= DateTime.Now.Date)
            {
                MessageBox.Show("Tanggal kadaluarsa harus di masa depan.", "Validasi Tanggal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtNamaVaksin.Text, "^[a-zA-Z\\s]+$"))
            {
                MessageBox.Show("Nama vaksin hanya boleh mengandung huruf dan spasi.", "Validasi Nama", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            string currentId = txtIDVaksin.Text.Trim();
            string newName = txtNamaVaksin.Text.Trim();

            using (var con = new SqlConnection(strKonek))
            {
                con.Open();

                // Validasi nama vaksin unik untuk ID lain
                var cmdCek = new SqlCommand("SELECT COUNT(*) FROM dbo.Vaksin WHERE NamaVaksin = @NamaVaksin AND ID_Vaksin != @ID_Vaksin", con);
                cmdCek.Parameters.AddWithValue("@NamaVaksin", newName);
                cmdCek.Parameters.AddWithValue("@ID_Vaksin", currentId);
                if ((int)cmdCek.ExecuteScalar() > 0)
                {
                    MessageBox.Show("Nama vaksin tersebut sudah digunakan oleh ID lain.", "Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var transaction = con.BeginTransaction();
                try
                {
                    using (var cmd = new SqlCommand("UpdateVaksin", con, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_Vaksin", currentId);
                        cmd.Parameters.AddWithValue("@NamaVaksin", newName);
                        cmd.Parameters.AddWithValue("@TanggalKadaluarsa", tanggalKadaluarsa);
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    _vaksinCache = null;
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
            if (dataGridViewVaksin.CurrentRow == null)
            {
                MessageBox.Show("Pilih data yang akan dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Yakin ingin menghapus data Vaksin ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            string id = dataGridViewVaksin.CurrentRow.Cells["ID_Vaksin"].Value.ToString();

            using (var con = new SqlConnection(strKonek))
            {
                con.Open();
                var transaction = con.BeginTransaction();
                try
                {
                    using (var cmd = new SqlCommand("DeleteVaksin", con, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_Vaksin", id);
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    _vaksinCache = null;
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

        private void dataGridViewVaksin_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            try
            {
                DataGridViewRow row = dataGridViewVaksin.Rows[e.RowIndex];
                txtIDVaksin.Text = Convert.ToString(row.Cells["ID_Vaksin"].Value);
                txtNamaVaksin.Text = Convert.ToString(row.Cells["NamaVaksin"].Value);
                txtTanggalKadaluarsa.Text = Convert.ToDateTime(row.Cells["TanggalKadaluarsa"].Value).ToString("yyyy-MM-dd");
                txtIDVaksin.ReadOnly = true; // Kunci ID saat update
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memilih data: " + ex.Message);
            }
        }

        private void ClearForm()
        {
            txtIDVaksin.Clear();
            txtNamaVaksin.Clear();
            txtTanggalKadaluarsa.Clear();
            txtIDVaksin.ReadOnly = false; // Buka kunci ID untuk data baru
        }

        private void Vaksin_Load(object sender, EventArgs e) { }
    }
}