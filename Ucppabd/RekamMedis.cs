using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Ucppabd
{
    public partial class RekamMedis : Form
    {
        // Perubahan disini: Initial Catalog diubah menjadi ProjecctPABD
        static string connectionString = "Data Source=DESKTOP-L9CBIM9\\SQLEXPRESS01;Initial Catalog=ProjecctPABD;Integrated Security=True";

        public RekamMedis()
        {
            InitializeComponent();
            LoadData();
            dataGridViewRekamMedis.CellClick += dataGridViewRekamMedis_CellContentClick;
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                // Anda perlu membuat Stored Procedure 'GetAllRekamMedis' di SQL
                using (SqlCommand cmd = new SqlCommand("GetAllRekamMedis", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridViewRekamMedis.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text) || string.IsNullOrWhiteSpace(txtIDHewan.Text) || string.IsNullOrWhiteSpace(txtIDVaksin.Text))
            {
                MessageBox.Show("ID, ID Hewan, dan ID Vaksin wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!DateTime.TryParse(txtTanggal.Text, out DateTime tanggal))
            {
                MessageBox.Show("Format tanggal tidak valid.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("TambahRekamMedis", con, transaction))
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewRekamMedis.CurrentRow == null)
            {
                MessageBox.Show("Pilih data yang akan dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string id = dataGridViewRekamMedis.CurrentRow.Cells["ID"].Value.ToString();
            if (MessageBox.Show("Yakin ingin menghapus?", "Konfirmasi",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("DeleteRekamMedis", con, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
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

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("UpdateRekamMedis", con, transaction))
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

        private void dataGridViewRekamMedis_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dataGridViewRekamMedis.Rows[e.RowIndex];
            txtID.Text = row.Cells["ID"].Value.ToString();
            txtIDHewan.Text = row.Cells["ID_Hewan"].Value.ToString();
            txtIDVaksin.Text = row.Cells["ID_Vaksin"].Value.ToString();
            txtKeterangan.Text = row.Cells["Keterangan"].Value.ToString();
            txtTanggal.Text = Convert.ToDateTime(row.Cells["Tanggal"].Value).ToString("yyyy-MM-dd");
        }

        private void ClearForm()
        {
            txtID.Clear();
            txtIDHewan.Clear();
            txtIDVaksin.Clear();
            txtKeterangan.Clear();
            txtTanggal.Clear();
        }
    }
}