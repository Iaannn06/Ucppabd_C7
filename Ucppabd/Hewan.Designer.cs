namespace Ucppabd
{
    partial class Hewan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtIDHewan = new System.Windows.Forms.TextBox();
            this.txtJenis = new System.Windows.Forms.TextBox();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.txtIDPemilik = new System.Windows.Forms.TextBox();
            this.dataGridViewHewan = new System.Windows.Forms.DataGridView();
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.cmbSatuanUmur = new System.Windows.Forms.ComboBox();
            this.txtUmur = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHewan)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(535, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hewan";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(116, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "ID_Hewan";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(116, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "ID_Pemilik";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(116, 261);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Jenis";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(116, 202);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Nama";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(116, 309);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Umur";
            // 
            // txtIDHewan
            // 
            this.txtIDHewan.Location = new System.Drawing.Point(233, 83);
            this.txtIDHewan.Name = "txtIDHewan";
            this.txtIDHewan.Size = new System.Drawing.Size(282, 22);
            this.txtIDHewan.TabIndex = 6;
            // 
            // txtJenis
            // 
            this.txtJenis.Location = new System.Drawing.Point(233, 259);
            this.txtJenis.Name = "txtJenis";
            this.txtJenis.Size = new System.Drawing.Size(282, 22);
            this.txtJenis.TabIndex = 8;
            // 
            // txtNama
            // 
            this.txtNama.Location = new System.Drawing.Point(233, 202);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(282, 22);
            this.txtNama.TabIndex = 9;
            // 
            // txtIDPemilik
            // 
            this.txtIDPemilik.Location = new System.Drawing.Point(233, 141);
            this.txtIDPemilik.Name = "txtIDPemilik";
            this.txtIDPemilik.Size = new System.Drawing.Size(282, 22);
            this.txtIDPemilik.TabIndex = 10;
            // 
            // dataGridViewHewan
            // 
            this.dataGridViewHewan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewHewan.Location = new System.Drawing.Point(369, 351);
            this.dataGridViewHewan.Name = "dataGridViewHewan";
            this.dataGridViewHewan.RowHeadersWidth = 51;
            this.dataGridViewHewan.RowTemplate.Height = 24;
            this.dataGridViewHewan.Size = new System.Drawing.Size(412, 150);
            this.dataGridViewHewan.TabIndex = 11;
            this.dataGridViewHewan.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewHewan_CellContentClick_1);
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(975, 83);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(143, 60);
            this.btnTambah.TabIndex = 12;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(975, 242);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(143, 60);
            this.btnEdit.TabIndex = 13;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnHapus
            // 
            this.btnHapus.Location = new System.Drawing.Point(975, 164);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(143, 60);
            this.btnHapus.TabIndex = 14;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            this.btnHapus.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // cmbSatuanUmur
            // 
            this.cmbSatuanUmur.FormattingEnabled = true;
            this.cmbSatuanUmur.Location = new System.Drawing.Point(233, 309);
            this.cmbSatuanUmur.Name = "cmbSatuanUmur";
            this.cmbSatuanUmur.Size = new System.Drawing.Size(282, 24);
            this.cmbSatuanUmur.TabIndex = 15;
            // 
            // txtUmur
            // 
            this.txtUmur.Location = new System.Drawing.Point(233, 310);
            this.txtUmur.Name = "txtUmur";
            this.txtUmur.Size = new System.Drawing.Size(261, 22);
            this.txtUmur.TabIndex = 16;
            // 
            // Hewan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 503);
            this.Controls.Add(this.txtUmur);
            this.Controls.Add(this.cmbSatuanUmur);
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnTambah);
            this.Controls.Add(this.dataGridViewHewan);
            this.Controls.Add(this.txtIDPemilik);
            this.Controls.Add(this.txtNama);
            this.Controls.Add(this.txtJenis);
            this.Controls.Add(this.txtIDHewan);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Hewan";
            this.Text = "Hewan";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHewan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtIDHewan;
        private System.Windows.Forms.TextBox txtJenis;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.TextBox txtIDPemilik;
        private System.Windows.Forms.DataGridView dataGridViewHewan;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.ComboBox cmbSatuanUmur;
        private System.Windows.Forms.TextBox txtUmur;
    }
}