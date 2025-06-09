namespace Ucppabd
{
    partial class Pemilik
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtIDPemilik = new System.Windows.Forms.TextBox();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtTelepon = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.dataGridViewPemilik = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPemilik)).BeginInit();
            this.SuspendLayout();
            // 
            // Label & TextBox Setups
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(158, 115);
            this.label1.Name = "label1";
            this.label1.Text = "ID_Pemilik";

            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(158, 173);
            this.label2.Name = "label2";
            this.label2.Text = "Nama";

            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(160, 233);
            this.label3.Name = "label3";
            this.label3.Text = "Email";

            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(153, 295);
            this.label4.Name = "label4";
            this.label4.Text = "Telepon";

            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F);
            this.label5.Location = new System.Drawing.Point(569, 9);
            this.label5.Name = "label5";
            this.label5.Text = "Pemilik";

            this.txtIDPemilik.Location = new System.Drawing.Point(290, 115);
            this.txtIDPemilik.Name = "txtIDPemilik";

            this.txtNama.Location = new System.Drawing.Point(290, 173);
            this.txtNama.Name = "txtNama";

            this.txtEmail.Location = new System.Drawing.Point(290, 233);
            this.txtEmail.Name = "txtEmail";

            this.txtTelepon.Location = new System.Drawing.Point(290, 293);
            this.txtTelepon.Name = "txtTelepon";

            this.btnTambah.Location = new System.Drawing.Point(1034, 98);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Text = "Tambah";
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);

            this.btnHapus.Location = new System.Drawing.Point(1034, 173);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Text = "Hapus";
            this.btnHapus.Click += new System.EventHandler(this.btnDelete_Click);

            this.btnUpdate.Location = new System.Drawing.Point(1034, 276);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);

            this.dataGridViewPemilik.Location = new System.Drawing.Point(404, 372);
            this.dataGridViewPemilik.Name = "dataGridViewPemilik";
            this.dataGridViewPemilik.Size = new System.Drawing.Size(460, 103);

            // 
            // Pemilik Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(1193, 476);
            this.Controls.Add(this.dataGridViewPemilik);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.btnTambah);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtTelepon);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtNama);
            this.Controls.Add(this.txtIDPemilik);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Pemilik";
            this.Text = "Pemilik";
            this.Load += new System.EventHandler(this.Pemilik_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPemilik)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox txtIDPemilik;
        internal System.Windows.Forms.TextBox txtNama;
        internal System.Windows.Forms.TextBox txtEmail;
        internal System.Windows.Forms.TextBox txtTelepon;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Button btnTambah;
        internal System.Windows.Forms.Button btnHapus;
        internal System.Windows.Forms.Button btnUpdate;
        internal System.Windows.Forms.DataGridView dataGridViewPemilik;
    }
}
