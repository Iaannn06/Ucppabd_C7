using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Ucppabd
{
    public partial class RekamMedis : Form
    {
        static string connectionString = "Data Source=DESKTOP-L9CBIM9\\SQLEXPRESS01;Initial Catalog=UCP4;Integrated Security=True";

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
                {
                    // Untuk menampilkan data, kita bisa menggunakan query langsung atau View
                    string query = "SELECT ID, ID_Hewan, ID_Vaksin, Keterangan, Tanggal FROM RekamMedis";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
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
            if (string.IsNullOrWhiteSpace(txtID.Text) || string.IsNullOrWhiteSpace(txtIDHewan.Text))
            {
                MessageBox.Show("ID Rekam Medis dan ID Hewan wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!DateTime.TryParse(txtTanggal.Text, out DateTime tanggal))
            {
                MessageBox.Show("Format tanggal tidak valid. Harap masukkan format yang benar (contoh: 2025-06-10).", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("AddRekamMedis", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID", txtID.Text.Trim());
                        cmd.Parameters.AddWithValue("@ID_Hewan", txtIDHewan.Text.Trim());
                        cmd.Parameters.AddWithValue("@ID_Vaksin", string.IsNullOrWhiteSpace(txtIDVaksin.Text) ? (object)DBNull.Value : txtIDVaksin.Text.Trim());
                        cmd.Parameters.AddWithValue("@Keterangan", txtKeterangan.Text.Trim());
                        cmd.Parameters.AddWithValue("@Tanggal", tanggal);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data rekam medis berhasil ditambahkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (dataGridViewRekamMedis.CurrentRow == null)
            {
                MessageBox.Show("Pilih data yang akan dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("Yakin ingin menghapus data rekam medis ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                string id = dataGridViewRekamMedis.CurrentRow.Cells["ID"].Value.ToString();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("DeleteRekamMedis", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ID", id);
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
            if (dataGridViewRekamMedis.CurrentRow == null)
            {
                MessageBox.Show("Pilih data yang akan diupdate!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!DateTime.TryParse(txtTanggal.Text, out DateTime tanggal))
            {
                MessageBox.Show("Format tanggal tidak valid. Harap masukkan format yang benar (contoh: 2025-06-10).", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("UpdateRekamMedis", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID", txtID.Text.Trim());
                        cmd.Parameters.AddWithValue("@ID_Hewan", txtIDHewan.Text.Trim());
                        cmd.Parameters.AddWithValue("@ID_Vaksin", string.IsNullOrWhiteSpace(txtIDVaksin.Text) ? (object)DBNull.Value : txtIDVaksin.Text.Trim());
                        cmd.Parameters.AddWithValue("@Keterangan", txtKeterangan.Text.Trim());
                        cmd.Parameters.AddWithValue("@Tanggal", tanggal);

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



        private void dataGridViewRekamMedis_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewRekamMedis.Rows[e.RowIndex];
                txtID.Text = row.Cells["ID"].Value.ToString();
                txtIDHewan.Text = row.Cells["ID_Hewan"].Value.ToString();
                txtIDVaksin.Text = row.Cells["ID_Vaksin"].Value.ToString();
                txtKeterangan.Text = row.Cells["Keterangan"].Value.ToString();
                txtTanggal.Text = Convert.ToDateTime(row.Cells["Tanggal"].Value).ToString("yyyy-MM-dd");
            }
        }

        private void ClearForm()
        {
            txtID.Clear();
            txtIDHewan.Clear();
            txtIDVaksin.Clear();
            txtKeterangan.Clear();
            txtTanggal.Clear();
        }

        private void RekamMedis_Load(object sender, EventArgs e) { }
    }
}
