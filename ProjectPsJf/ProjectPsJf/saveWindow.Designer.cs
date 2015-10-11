namespace ProjectPsJf
{
    partial class saveWindow
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
            this.lblNamn = new System.Windows.Forms.Label();
            this.tbKat = new System.Windows.Forms.TextBox();
            this.lblKat = new System.Windows.Forms.Label();
            this.tbUppd = new System.Windows.Forms.TextBox();
            this.lblUppd = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnSpara = new System.Windows.Forms.Button();
            this.tbNamn = new System.Windows.Forms.TextBox();
            this.lblUrl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblNamn
            // 
            this.lblNamn.AutoSize = true;
            this.lblNamn.Location = new System.Drawing.Point(23, 43);
            this.lblNamn.Name = "lblNamn";
            this.lblNamn.Size = new System.Drawing.Size(35, 13);
            this.lblNamn.TabIndex = 2;
            this.lblNamn.Text = "Namn";
            // 
            // tbKat
            // 
            this.tbKat.Location = new System.Drawing.Point(26, 98);
            this.tbKat.Name = "tbKat";
            this.tbKat.Size = new System.Drawing.Size(100, 20);
            this.tbKat.TabIndex = 5;
            // 
            // lblKat
            // 
            this.lblKat.AutoSize = true;
            this.lblKat.Location = new System.Drawing.Point(23, 82);
            this.lblKat.Name = "lblKat";
            this.lblKat.Size = new System.Drawing.Size(46, 13);
            this.lblKat.TabIndex = 4;
            this.lblKat.Text = "Kategori";
            // 
            // tbUppd
            // 
            this.tbUppd.Location = new System.Drawing.Point(26, 137);
            this.tbUppd.Name = "tbUppd";
            this.tbUppd.Size = new System.Drawing.Size(100, 20);
            this.tbUppd.TabIndex = 7;
            // 
            // lblUppd
            // 
            this.lblUppd.AutoSize = true;
            this.lblUppd.Location = new System.Drawing.Point(23, 121);
            this.lblUppd.Name = "lblUppd";
            this.lblUppd.Size = new System.Drawing.Size(106, 13);
            this.lblUppd.TabIndex = 6;
            this.lblUppd.Text = "Uppdateringsintervall";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(144, 140);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Ange i minuter";
            // 
            // btnSpara
            // 
            this.btnSpara.Location = new System.Drawing.Point(26, 179);
            this.btnSpara.Name = "btnSpara";
            this.btnSpara.Size = new System.Drawing.Size(75, 23);
            this.btnSpara.TabIndex = 9;
            this.btnSpara.Text = "Spara";
            this.btnSpara.UseVisualStyleBackColor = true;
            this.btnSpara.Click += new System.EventHandler(this.btnSpara_Click);
            // 
            // tbNamn
            // 
            this.tbNamn.Location = new System.Drawing.Point(26, 59);
            this.tbNamn.Name = "tbNamn";
            this.tbNamn.Size = new System.Drawing.Size(100, 20);
            this.tbNamn.TabIndex = 10;
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Location = new System.Drawing.Point(23, 21);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(32, 13);
            this.lblUrl.TabIndex = 11;
            this.lblUrl.Text = "URL;";
            // 
            // saveWindow
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.lblUrl);
            this.Controls.Add(this.tbNamn);
            this.Controls.Add(this.btnSpara);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tbUppd);
            this.Controls.Add(this.lblUppd);
            this.Controls.Add(this.tbKat);
            this.Controls.Add(this.lblKat);
            this.Controls.Add(this.lblNamn);
            this.Name = "saveWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

      
     
        private System.Windows.Forms.Label lblNamn;
        private System.Windows.Forms.TextBox tbKat;
        private System.Windows.Forms.Label lblKat;
        private System.Windows.Forms.TextBox tbUppd;
        private System.Windows.Forms.Label lblUppd;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnSpara;
        private System.Windows.Forms.TextBox tbNamn;
        private System.Windows.Forms.Label lblUrl;
    }
}