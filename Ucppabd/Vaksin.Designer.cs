namespace Ucppabd
{
    partial class Vaksin
    {
        // ... (bagian deklarasi variabel kontrol tetap sama)

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtIDVaksin = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNamaVaksin = new System.Windows.Forms.TextBox();
            this.txtTanggalKadaluarsa = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.dataGridViewVaksin = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVaksin)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F,
                System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(548, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vaksin";
            // 
            // txtIDVaksin
            // 
            this.txtIDVaksin.Location = new System.Drawing.Point(379, 131);
            this.txtIDVaksin.Name = "txtIDVaksin";
            this.txtIDVaksin.Size = new System.Drawing.Size(253, 22);
            this.txtIDVaksin.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F,
                System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(219, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "ID_Vaksin";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F,
                System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(219, 256);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "TanggalKadaluarsa";
            // 
            // txtNamaVaksin
            // 
            this.txtNamaVaksin.Location = new System.Drawing.Point(379, 194);
            this.txtNamaVaksin.Name = "txtNamaVaksin";
            this.txtNamaVaksin.Size = new System.Drawing.Size(253, 22);
            this.txtNamaVaksin.TabIndex = 4;
            // 
            // txtTanggalKadaluarsa
            // 
            this.txtTanggalKadaluarsa.Location = new System.Drawing.Point(379, 254);
            this.txtTanggalKadaluarsa.Name = "txtTanggalKadaluarsa";
            this.txtTanggalKadaluarsa.Size = new System.Drawing.Size(253, 22);
            this.txtTanggalKadaluarsa.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F,
                System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(219, 196);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "NamaVaksin";
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(759, 119);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(117, 47);
            this.btnTambah.TabIndex = 8;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // btnHapus
            // 
            this.btnHapus.Location = new System.Drawing.Point(759, 184);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(117, 47);
            this.btnHapus.TabIndex = 9;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            this.btnHapus.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(759, 244);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(117, 47);
            this.btnUpdate.TabIndex = 10;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // dataGridViewVaksin
            // 
            this.dataGridViewVaksin.ColumnHeadersHeightSizeMode =
                System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewVaksin.Location = new System.Drawing.Point(261, 376);
            this.dataGridViewVaksin.Name = "dataGridViewVaksin";
            this.dataGridViewVaksin.RowHeadersWidth = 51;
            this.dataGridViewVaksin.RowTemplate.Height = 24;
            this.dataGridViewVaksin.Size = new System.Drawing.Size(565, 150);
            this.dataGridViewVaksin.TabIndex = 11;
            // **Di sini kita ganti handler lama “_CellContentClick_2” menjadi yang ada di Vaksin.cs:**
            this.dataGridViewVaksin.CellContentClick +=
                new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewVaksin_CellContentClick);
            // 
            // Vaksin (Form)
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1191, 560);
            this.Controls.Add(this.dataGridViewVaksin);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.btnTambah);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtTanggalKadaluarsa);
            this.Controls.Add(this.txtNamaVaksin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtIDVaksin);
            this.Controls.Add(this.label1);
            this.Name = "Vaksin";
            this.Text = "Vaksin";
            // **Ganti juga handler Load agar memanggil method Vaksin_Load yang sudah ada:**
            this.Load += new System.EventHandler(this.Vaksin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVaksin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIDVaksin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNamaVaksin;
        private System.Windows.Forms.TextBox txtTanggalKadaluarsa;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.DataGridView dataGridViewVaksin;
    }
}
