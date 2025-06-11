using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Ucppabd
{
    public partial class Dokter : Form
    {
        // Perubahan disini: Initial Catalog diubah menjadi ProjecctPABD
        static string connectionString = "Data Source=DESKTOP-L9CBIM9\\SQLEXPRESS01;Initial Catalog=ProjecctPABD;Integrated Security=True";

        public Dokter()
        {
            InitializeComponent();
            LoadData();
            dataGridViewDokter.CellClick += dataGridViewDokter_CellContentClick;
        }

        private void LoadData()
        {
            try
            {
                using (var con = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand("GetAllDokter", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var da = new SqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    dataGridViewDokter.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error memuat data: " + ex.Message);
            }
        }

        private bool ValidasiInput()
        {
            if (string.IsNullOrWhiteSpace(txtID.Text) || string.IsNullOrWhiteSpace(txtNama.Text))
            {
                MessageBox.Show("ID dan Nama wajib diisi.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!Regex.IsMatch(txtNama.Text, "^[a-zA-Z\\s.-]+$")) // Ditambah . dan - untuk nama seperti Dr. atau nama dengan strip
            {
                MessageBox.Show("Nama hanya boleh mengandung huruf, spasi, titik, dan strip.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!Regex.IsMatch(txtTelepon.Text, "^\\d{10,13}$"))
            {
                MessageBox.Show("Nomor telepon harus berupa angka 10-13 digit.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (!ValidasiInput()) return;

            using (var con = new SqlConnection(connectionString))
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
            if (!ValidasiInput()) return;

            using (var con = new SqlConnection(connectionString))
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

            var result = MessageBox.Show("Yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            string id = dataGridViewDokter.CurrentRow.Cells["ID"].Value.ToString();

            using (var con = new SqlConnection(connectionString))
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
        }

        private void ClearForm()
        {
            txtID.Clear();
            txtNama.Clear();
            txtSpesialisasi.Clear();
            txtTelepon.Clear();
        }

        private void Dokter_Load(object sender, EventArgs e) { }
    }
}