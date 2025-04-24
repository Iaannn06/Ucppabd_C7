using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Ucppabd
{
    public partial class Hewan : Form
    {
        static string connectionString = "Data Source=DESKTOP-L9CBIM9\\SQLEXPRESS01;Initial Catalog=UCP4;Integrated Security=True";

        public Hewan()
        {
            InitializeComponent();
            LoadData();
            dataGridViewHewan.CellClick += dataGridViewHewan_CellContentClick;

            // Tambahkan isi ComboBox satuan umur
            cmbSatuanUmur.Items.AddRange(new string[] { "Hari", "Bulan", "Tahun" });
            cmbSatuanUmur.SelectedIndex = 0;
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            // Memastikan umur adalah angka valid
            int umurAngka;
            bool isValidUmur = int.TryParse(txtUmur.Text, out umurAngka);

            if (isValidUmur)
            {
                string satuanUmur = cmbSatuanUmur.SelectedItem.ToString();  // Ambil satuan umur

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Hewan (ID_Hewan, ID_Pemilik, Nama, Jenis, UmurAngka, SatuanUmur) VALUES (@ID_Hewan, @ID_Pemilik, @Nama, @Jenis, @UmurAngka, @SatuanUmur)";
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@ID_Hewan", txtIDHewan.Text);
                    cmd.Parameters.AddWithValue("@ID_Pemilik", txtIDPemilik.Text);
                    cmd.Parameters.AddWithValue("@Nama", txtNama.Text);
                    cmd.Parameters.AddWithValue("@Jenis", txtJenis.Text);
                    cmd.Parameters.AddWithValue("@UmurAngka", umurAngka);
                    cmd.Parameters.AddWithValue("@SatuanUmur", satuanUmur);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data berhasil ditambahkan");
                    ClearForm();
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Format umur tidak valid. Harap masukkan angka.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewHewan.CurrentRow != null)
            {
                string id = dataGridViewHewan.CurrentRow.Cells["ID_Hewan"].Value.ToString();

                var confirm = MessageBox.Show("Yakin ingin menghapus data Hewan ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string query = "DELETE FROM Hewan WHERE ID_Hewan = @id";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@id", id);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data berhasil dihapus", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearForm();
                        LoadData();
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewHewan.CurrentRow != null)
            {
                string id = dataGridViewHewan.CurrentRow.Cells["ID_Hewan"].Value.ToString();
                int umurAngka;
                bool isValidUmur = int.TryParse(txtUmur.Text, out umurAngka);
                string satuanUmur = cmbSatuanUmur.SelectedItem.ToString();

                if (isValidUmur)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string query = "UPDATE Hewan SET ID_Hewan=@ID_Hewan, ID_Pemilik=@ID_Pemilik, Nama=@Nama, Jenis=@Jenis, UmurAngka=@UmurAngka, SatuanUmur=@SatuanUmur WHERE ID_Hewan=@ID";

                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@ID_Hewan", txtIDHewan.Text);
                        cmd.Parameters.AddWithValue("@ID_Pemilik", txtIDPemilik.Text);
                        cmd.Parameters.AddWithValue("@Nama", txtNama.Text);
                        cmd.Parameters.AddWithValue("@Jenis", txtJenis.Text);
                        cmd.Parameters.AddWithValue("@UmurAngka", umurAngka);
                        cmd.Parameters.AddWithValue("@SatuanUmur", satuanUmur);
                        cmd.Parameters.AddWithValue("@ID", id);

                        con.Open();
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Data berhasil diperbarui");
                        ClearForm();
                        LoadData();
                    }
                }
                else
                {
                    MessageBox.Show("Format umur tidak valid. Harap masukkan angka.");
                }
            }
        }

        private void dataGridViewHewan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewHewan.Rows[e.RowIndex];

                txtIDHewan.Text = row.Cells["ID_Hewan"].Value.ToString();
                txtIDPemilik.Text = row.Cells["ID_Pemilik"].Value.ToString();
                txtNama.Text = row.Cells["Nama"].Value.ToString();
                txtJenis.Text = row.Cells["Jenis"].Value.ToString();

                // Cek jika kolom UmurAngka dan SatuanUmur tidak null
                if (row.Cells["UmurAngka"].Value != DBNull.Value)
                {
                    int umurAngka = Convert.ToInt32(row.Cells["UmurAngka"].Value);
                    txtUmur.Text = umurAngka.ToString();
                }
                else
                {
                    txtUmur.Text = string.Empty;
                }

                if (row.Cells["SatuanUmur"].Value != DBNull.Value)
                {
                    string satuanUmur = row.Cells["SatuanUmur"].Value.ToString();
                    cmbSatuanUmur.SelectedItem = satuanUmur;
                }
                else
                {
                    cmbSatuanUmur.SelectedIndex = 0; // Atau sesuaikan dengan nilai default yang diinginkan
                }
            }
        }

        private void ClearForm()
        {
            txtIDHewan.Clear();
            txtIDPemilik.Clear();
            txtNama.Clear();
            txtJenis.Clear();
            txtUmur.Clear();
            cmbSatuanUmur.SelectedIndex = 0;
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Hewan";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridViewHewan.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data: " + ex.Message);
            }
        }

        private void Hewan_Load(object sender, EventArgs e)
        {
            // Pastikan ComboBox diisi saat form dimuat
            if (cmbSatuanUmur.Items.Count == 0)
            {
                cmbSatuanUmur.Items.AddRange(new string[] { "Hari", "Bulan", "Tahun" });
                cmbSatuanUmur.SelectedIndex = 0;
            }
        }
    }
}
