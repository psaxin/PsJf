namespace GUI
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
            this.lblKat = new System.Windows.Forms.Label();
            this.tbUppd = new System.Windows.Forms.TextBox();
            this.lblUppd = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnSpara = new System.Windows.Forms.Button();
            this.tbNamn = new System.Windows.Forms.TextBox();
            this.lblUrl = new System.Windows.Forms.Label();
            this.tbUrl = new System.Windows.Forms.TextBox();
            this.cbCategories = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblNamn
            // 
            this.lblNamn.AutoSize = true;
            this.lblNamn.Location = new System.Drawing.Point(23, 72);
            this.lblNamn.Name = "lblNamn";
            this.lblNamn.Size = new System.Drawing.Size(35, 13);
            this.lblNamn.TabIndex = 2;
            this.lblNamn.Text = "Namn";
            // 
            // lblKat
            // 
            this.lblKat.AutoSize = true;
            this.lblKat.Location = new System.Drawing.Point(23, 111);
            this.lblKat.Name = "lblKat";
            this.lblKat.Size = new System.Drawing.Size(46, 13);
            this.lblKat.TabIndex = 4;
            this.lblKat.Text = "Kategori";
            // 
            // tbUppd
            // 
            this.tbUppd.Location = new System.Drawing.Point(26, 166);
            this.tbUppd.MaxLength = 4;
            this.tbUppd.Name = "tbUppd";
            this.tbUppd.Size = new System.Drawing.Size(121, 20);
            this.tbUppd.TabIndex = 2;
            // 
            // lblUppd
            // 
            this.lblUppd.AutoSize = true;
            this.lblUppd.Location = new System.Drawing.Point(23, 150);
            this.lblUppd.Name = "lblUppd";
            this.lblUppd.Size = new System.Drawing.Size(106, 13);
            this.lblUppd.TabIndex = 6;
            this.lblUppd.Text = "Uppdateringsintervall";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(153, 173);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Ange i minuter";
            // 
            // btnSpara
            // 
            this.btnSpara.Location = new System.Drawing.Point(26, 192);
            this.btnSpara.Name = "btnSpara";
            this.btnSpara.Size = new System.Drawing.Size(75, 23);
            this.btnSpara.TabIndex = 3;
            this.btnSpara.Text = "Spara";
            this.btnSpara.UseVisualStyleBackColor = true;
            this.btnSpara.Click += new System.EventHandler(this.btnSpara_Click);
            // 
            // tbNamn
            // 
            this.tbNamn.Location = new System.Drawing.Point(26, 88);
            this.tbNamn.MaxLength = 15;
            this.tbNamn.Name = "tbNamn";
            this.tbNamn.Size = new System.Drawing.Size(121, 20);
            this.tbNamn.TabIndex = 0;
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Location = new System.Drawing.Point(23, 33);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(29, 13);
            this.lblUrl.TabIndex = 0;
            this.lblUrl.Text = "URL";
            // 
            // tbUrl
            // 
            this.tbUrl.Location = new System.Drawing.Point(26, 49);
            this.tbUrl.Name = "tbUrl";
            this.tbUrl.Size = new System.Drawing.Size(215, 20);
            this.tbUrl.TabIndex = 9;
            // 
            // cbCategories
            // 
            this.cbCategories.FormattingEnabled = true;
            this.cbCategories.Location = new System.Drawing.Point(26, 128);
            this.cbCategories.Name = "cbCategories";
            this.cbCategories.Size = new System.Drawing.Size(121, 21);
            this.cbCategories.TabIndex = 10;
            // 
            // saveWindow
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.cbCategories);
            this.Controls.Add(this.tbUrl);
            this.Controls.Add(this.lblUrl);
            this.Controls.Add(this.tbNamn);
            this.Controls.Add(this.btnSpara);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tbUppd);
            this.Controls.Add(this.lblUppd);
            this.Controls.Add(this.lblKat);
            this.Controls.Add(this.lblNamn);
            this.Name = "saveWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion



        private System.Windows.Forms.Label lblNamn;
        private System.Windows.Forms.Label lblKat;
        public System.Windows.Forms.TextBox tbUppd;
        private System.Windows.Forms.Label lblUppd;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnSpara;
        public System.Windows.Forms.TextBox tbNamn;
        private System.Windows.Forms.Label lblUrl;
        public System.Windows.Forms.TextBox tbUrl;
        private System.Windows.Forms.ComboBox cbCategories;
    }
}