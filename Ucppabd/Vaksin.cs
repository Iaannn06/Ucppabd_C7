using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Ucppabd
{
    public partial class Vaksin : Form
    {
        static string connectionString = "Data Source=DESKTOP-L9CBIM9\\SQLEXPRESS01;Initial Catalog=UCP4;Integrated Security=True";
        public Vaksin()
        {
            InitializeComponent();
            LoadData();
            dataGridViewVaksin.CellClick += dataGridViewVaksin_CellContentClick;
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT ID_Vaksin, NamaVaksin, TanggalKadaluarsa FROM Vaksin";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridViewVaksin.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDVaksin.Text) || string.IsNullOrWhiteSpace(txtNamaVaksin.Text))
            {
                MessageBox.Show("ID Vaksin dan Nama Vaksin wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!DateTime.TryParse(txtTanggalKadaluarsa.Text, out DateTime tanggalKadaluarsa))
            {
                MessageBox.Show("Format tanggal tidak valid. Harap masukkan format yang benar (contoh: 2026-05-10).", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("AddVaksin", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_Vaksin", txtIDVaksin.Text.Trim());
                        cmd.Parameters.AddWithValue("@NamaVaksin", txtNamaVaksin.Text.Trim());
                        cmd.Parameters.AddWithValue("@TanggalKadaluarsa", tanggalKadaluarsa);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data vaksin berhasil ditambahkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        ClearForm();
                    }
                }
                catch (Exception ex)
                {
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

            var confirm = MessageBox.Show("Yakin ingin menghapus data Vaksin ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                string id = dataGridViewVaksin.CurrentRow.Cells["ID_Vaksin"].Value.ToString();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("DeleteVaksin", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ID_Vaksin", id);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Data berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                            ClearForm();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
                MessageBox.Show("Format tanggal tidak valid. Harap masukkan format yang benar (contoh: 2026-05-10).", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("UpdateVaksin", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_Vaksin", txtIDVaksin.Text.Trim());
                        cmd.Parameters.AddWithValue("@NamaVaksin", txtNamaVaksin.Text.Trim());
                        cmd.Parameters.AddWithValue("@TanggalKadaluarsa", tanggalKadaluarsa);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data berhasil diperbarui.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        ClearForm();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridViewVaksin_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewVaksin.Rows[e.RowIndex];
                txtIDVaksin.Text = row.Cells["ID_Vaksin"].Value.ToString();
                txtNamaVaksin.Text = row.Cells["NamaVaksin"].Value.ToString();
                txtTanggalKadaluarsa.Text = Convert.ToDateTime(row.Cells["TanggalKadaluarsa"].Value).ToString("yyyy-MM-dd");
            }
        }

        private void ClearForm()
        {
            txtIDVaksin.Clear();
            txtNamaVaksin.Clear();
            txtTanggalKadaluarsa.Clear();
        }

        private void Vaksin_Load(object sender, EventArgs e) { }
    }
}
