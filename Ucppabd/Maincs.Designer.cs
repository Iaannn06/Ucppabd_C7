// Maincs.Designer.cs
using System.Windows.Forms;

namespace Ucppabd
{
    partial class Maincs
    {
        private System.ComponentModel.IContainer components = null;

        private Button btnDokter;
        private Button btnRekamMedis;
        private Button btnVaksin;
        private Button btnHewan;
        private Button btnPemilik;
        private Button btnJanjiTemu;
    
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnDokter = new System.Windows.Forms.Button();
            this.btnRekamMedis = new System.Windows.Forms.Button();
            this.btnVaksin = new System.Windows.Forms.Button();
            this.btnHewan = new System.Windows.Forms.Button();
            this.btnPemilik = new System.Windows.Forms.Button();
            this.btnJanjiTemu = new System.Windows.Forms.Button();
            this.btnLaporan = new System.Windows.Forms.Button();
            this.btnTesKoneksi = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDokter
            // 
            this.btnDokter.Location = new System.Drawing.Point(100, 100);
            this.btnDokter.Name = "btnDokter";
            this.btnDokter.Size = new System.Drawing.Size(100, 40);
            this.btnDokter.TabIndex = 0;
            this.btnDokter.Text = "Dokter";
            this.btnDokter.UseVisualStyleBackColor = true;
            this.btnDokter.Click += new System.EventHandler(this.btnDokter_Click);
            // 
            // btnRekamMedis
            // 
            this.btnRekamMedis.Location = new System.Drawing.Point(260, 100);
            this.btnRekamMedis.Name = "btnRekamMedis";
            this.btnRekamMedis.Size = new System.Drawing.Size(100, 40);
            this.btnRekamMedis.TabIndex = 1;
            this.btnRekamMedis.Text = "Rekam Medis";
            this.btnRekamMedis.UseVisualStyleBackColor = true;
            this.btnRekamMedis.Click += new System.EventHandler(this.btnRekamMedis_Click);
            // 
            // btnVaksin
            // 
            this.btnVaksin.Location = new System.Drawing.Point(100, 160);
            this.btnVaksin.Name = "btnVaksin";
            this.btnVaksin.Size = new System.Drawing.Size(100, 40);
            this.btnVaksin.TabIndex = 2;
            this.btnVaksin.Text = "Vaksin";
            this.btnVaksin.UseVisualStyleBackColor = true;
            this.btnVaksin.Click += new System.EventHandler(this.btnVaksin_Click);
            // 
            // btnHewan
            // 
            this.btnHewan.Location = new System.Drawing.Point(260, 160);
            this.btnHewan.Name = "btnHewan";
            this.btnHewan.Size = new System.Drawing.Size(100, 40);
            this.btnHewan.TabIndex = 3;
            this.btnHewan.Text = "Hewan";
            this.btnHewan.UseVisualStyleBackColor = true;
            this.btnHewan.Click += new System.EventHandler(this.btnHewan_Click);
            // 
            // btnPemilik
            // 
            this.btnPemilik.Location = new System.Drawing.Point(100, 220);
            this.btnPemilik.Name = "btnPemilik";
            this.btnPemilik.Size = new System.Drawing.Size(100, 40);
            this.btnPemilik.TabIndex = 4;
            this.btnPemilik.Text = "Pemilik";
            this.btnPemilik.UseVisualStyleBackColor = true;
            this.btnPemilik.Click += new System.EventHandler(this.btnPemilik_Click);
            // 
            // btnJanjiTemu
            // 
            this.btnJanjiTemu.Location = new System.Drawing.Point(260, 220);
            this.btnJanjiTemu.Name = "btnJanjiTemu";
            this.btnJanjiTemu.Size = new System.Drawing.Size(100, 40);
            this.btnJanjiTemu.TabIndex = 5;
            this.btnJanjiTemu.Text = "Janji Temu";
            this.btnJanjiTemu.UseVisualStyleBackColor = true;
            this.btnJanjiTemu.Click += new System.EventHandler(this.btnJanjiTemu_Click);
            // 
            // btnLaporan
            // 
            this.btnLaporan.Location = new System.Drawing.Point(100, 266);
            this.btnLaporan.Name = "btnLaporan";
            this.btnLaporan.Size = new System.Drawing.Size(100, 53);
            this.btnLaporan.TabIndex = 6;
            this.btnLaporan.Text = "Laporan";
            this.btnLaporan.UseVisualStyleBackColor = true;
            this.btnLaporan.Click += new System.EventHandler(this.btnLaporan_Click);
            // 
            // btnTesKoneksi
            // 
            this.btnTesKoneksi.Location = new System.Drawing.Point(260, 273);
            this.btnTesKoneksi.Name = "btnTesKoneksi";
            this.btnTesKoneksi.Size = new System.Drawing.Size(100, 46);
            this.btnTesKoneksi.TabIndex = 7;
            this.btnTesKoneksi.Text = "Tes Koneksi";
            this.btnTesKoneksi.UseVisualStyleBackColor = true;
            this.btnTesKoneksi.Click += new System.EventHandler(this.btnTesKoneksi_Click);
            // 
            // Maincs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 360);
            this.Controls.Add(this.btnTesKoneksi);
            this.Controls.Add(this.btnLaporan);
            this.Controls.Add(this.btnJanjiTemu);
            this.Controls.Add(this.btnPemilik);
            this.Controls.Add(this.btnHewan);
            this.Controls.Add(this.btnVaksin);
            this.Controls.Add(this.btnRekamMedis);
            this.Controls.Add(this.btnDokter);
            this.Name = "Maincs";
            this.Text = "Menu Pilihan";
            this.ResumeLayout(false);

        }

        private Button btnLaporan;
        private Button btnTesKoneksi;
    }
}
