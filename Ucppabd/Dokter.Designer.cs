namespace Ucppabd
{
    partial class Dokter
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtSpesialisasi = new System.Windows.Forms.TextBox();
            this.txtTelepon = new System.Windows.Forms.TextBox();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.dataGridViewDokter = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDokter)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(162, 314);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Telepon";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(162, 237);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Spesialisasi";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(162, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Nama";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(162, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "ID";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(290, 80);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(320, 22);
            this.txtID.TabIndex = 5;
            // 
            // txtSpesialisasi
            // 
            this.txtSpesialisasi.Location = new System.Drawing.Point(290, 231);
            this.txtSpesialisasi.Name = "txtSpesialisasi";
            this.txtSpesialisasi.Size = new System.Drawing.Size(320, 22);
            this.txtSpesialisasi.TabIndex = 6;
            // 
            // txtTelepon
            // 
            this.txtTelepon.Location = new System.Drawing.Point(290, 314);
            this.txtTelepon.Name = "txtTelepon";
            this.txtTelepon.Size = new System.Drawing.Size(320, 22);
            this.txtTelepon.TabIndex = 7;
            // 
            // txtNama
            // 
            this.txtNama.Location = new System.Drawing.Point(290, 148);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(320, 22);
            this.txtNama.TabIndex = 8;
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(1022, 89);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(104, 40);
            this.btnTambah.TabIndex = 10;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(1022, 237);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(104, 42);
            this.btnUpdate.TabIndex = 11;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(1022, 154);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(104, 41);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "Hapus";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // dataGridViewDokter
            // 
            this.dataGridViewDokter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDokter.Location = new System.Drawing.Point(262, 389);
            this.dataGridViewDokter.Name = "dataGridViewDokter";
            this.dataGridViewDokter.RowHeadersWidth = 51;
            this.dataGridViewDokter.RowTemplate.Height = 24;
            this.dataGridViewDokter.Size = new System.Drawing.Size(755, 129);
            this.dataGridViewDokter.TabIndex = 13;
            this.dataGridViewDokter.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewDokter_CellContentClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(593, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 29);
            this.label5.TabIndex = 14;
            this.label5.Text = "Dokter";
            // 
            // Dokter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1194, 517);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dataGridViewDokter);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnTambah);
            this.Controls.Add(this.txtNama);
            this.Controls.Add(this.txtTelepon);
            this.Controls.Add(this.txtSpesialisasi);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Name = "Dokter";
            this.Text = "Dokter";
            this.Load += new System.EventHandler(this.Dokter_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDokter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtSpesialisasi;
        private System.Windows.Forms.TextBox txtTelepon;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridView dataGridViewDokter;
        private System.Windows.Forms.Label label5;
    }
}