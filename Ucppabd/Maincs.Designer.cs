namespace Ucppabd
{
    partial class Maincs
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
            this.btnDokter = new System.Windows.Forms.Button();
            this.btnVaksin = new System.Windows.Forms.Button();
            this.btnPemilik = new System.Windows.Forms.Button();
            this.btnRekamMedis = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnHewan = new System.Windows.Forms.Button();
            this.btnJanjiTemu = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDokter
            // 
            this.btnDokter.Location = new System.Drawing.Point(193, 131);
            this.btnDokter.Name = "btnDokter";
            this.btnDokter.Size = new System.Drawing.Size(145, 60);
            this.btnDokter.TabIndex = 1;
            this.btnDokter.Text = "Dokter";
            this.btnDokter.UseVisualStyleBackColor = true;
            this.btnDokter.Click += new System.EventHandler(this.btnDokter_Click);
            // 
            // btnVaksin
            // 
            this.btnVaksin.Location = new System.Drawing.Point(193, 231);
            this.btnVaksin.Name = "btnVaksin";
            this.btnVaksin.Size = new System.Drawing.Size(143, 59);
            this.btnVaksin.TabIndex = 2;
            this.btnVaksin.Text = "Vaksin";
            this.btnVaksin.UseVisualStyleBackColor = true;
            this.btnVaksin.Click += new System.EventHandler(this.btnVaksin_Click);
            // 
            // btnPemilik
            // 
            this.btnPemilik.Location = new System.Drawing.Point(193, 326);
            this.btnPemilik.Name = "btnPemilik";
            this.btnPemilik.Size = new System.Drawing.Size(143, 59);
            this.btnPemilik.TabIndex = 3;
            this.btnPemilik.Text = "Pemilik";
            this.btnPemilik.UseVisualStyleBackColor = true;
            this.btnPemilik.Click += new System.EventHandler(this.btnPemilik_Click);
            // 
            // btnRekamMedis
            // 
            this.btnRekamMedis.Location = new System.Drawing.Point(466, 131);
            this.btnRekamMedis.Name = "btnRekamMedis";
            this.btnRekamMedis.Size = new System.Drawing.Size(133, 59);
            this.btnRekamMedis.TabIndex = 4;
            this.btnRekamMedis.Text = "Rekam Medis";
            this.btnRekamMedis.UseVisualStyleBackColor = true;
            this.btnRekamMedis.Click += new System.EventHandler(this.btnRekamMedis_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(294, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 32);
            this.label1.TabIndex = 5;
            this.label1.Text = "Menu Pilihan";
            this.label1.Click += new System.EventHandler(this.btnDokter_Click);
            // 
            // btnHewan
            // 
            this.btnHewan.Location = new System.Drawing.Point(466, 231);
            this.btnHewan.Name = "btnHewan";
            this.btnHewan.Size = new System.Drawing.Size(133, 59);
            this.btnHewan.TabIndex = 6;
            this.btnHewan.Text = "Hewan";
            this.btnHewan.UseVisualStyleBackColor = true;
            this.btnHewan.Click += new System.EventHandler(this.btnHewan_Click);
            // 
            // btnJanjiTemu
            // 
            this.btnJanjiTemu.Location = new System.Drawing.Point(466, 326);
            this.btnJanjiTemu.Name = "btnJanjiTemu";
            this.btnJanjiTemu.Size = new System.Drawing.Size(133, 59);
            this.btnJanjiTemu.TabIndex = 7;
            this.btnJanjiTemu.Text = "Janji Temu";
            this.btnJanjiTemu.UseVisualStyleBackColor = true;
            this.btnJanjiTemu.Click += new System.EventHandler(this.btnJanjiTemu_Click);
            // 
            // Maincs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnJanjiTemu);
            this.Controls.Add(this.btnHewan);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRekamMedis);
            this.Controls.Add(this.btnPemilik);
            this.Controls.Add(this.btnVaksin);
            this.Controls.Add(this.btnDokter);
            this.Name = "Maincs";
            this.Text = "Maincs";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnDokter;
        private System.Windows.Forms.Button btnVaksin;
        private System.Windows.Forms.Button btnPemilik;
        private System.Windows.Forms.Button btnRekamMedis;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnHewan;
        private System.Windows.Forms.Button btnJanjiTemu;
    }
}