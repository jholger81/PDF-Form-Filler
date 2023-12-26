namespace PDF_Form_Filler
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_pdfload = new System.Windows.Forms.Button();
            this.btn_configcreate = new System.Windows.Forms.Button();
            this.btn_configedit = new System.Windows.Forms.Button();
            this.tb_configload = new System.Windows.Forms.TextBox();
            this.btn_quit = new System.Windows.Forms.Button();
            this.btn_configload = new System.Windows.Forms.Button();
            this.btn_overview = new System.Windows.Forms.Button();
            this.btn_pathoutput = new System.Windows.Forms.Button();
            this.tb_pdfload = new System.Windows.Forms.TextBox();
            this.tb_pathoutput = new System.Windows.Forms.TextBox();
            this.btn_fillpdf = new System.Windows.Forms.Button();
            this.btn_help = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_pdfload
            // 
            this.btn_pdfload.Location = new System.Drawing.Point(8, 7);
            this.btn_pdfload.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_pdfload.Name = "btn_pdfload";
            this.btn_pdfload.Size = new System.Drawing.Size(93, 49);
            this.btn_pdfload.TabIndex = 0;
            this.btn_pdfload.Text = "pdf laden";
            this.btn_pdfload.UseVisualStyleBackColor = true;
            this.btn_pdfload.Click += new System.EventHandler(this.btn_pdfload_Click);
            // 
            // btn_configcreate
            // 
            this.btn_configcreate.Location = new System.Drawing.Point(108, 143);
            this.btn_configcreate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_configcreate.Name = "btn_configcreate";
            this.btn_configcreate.Size = new System.Drawing.Size(93, 49);
            this.btn_configcreate.TabIndex = 3;
            this.btn_configcreate.Text = "config erstellen";
            this.btn_configcreate.UseVisualStyleBackColor = true;
            this.btn_configcreate.Click += new System.EventHandler(this.btn_configcreate_Click);
            // 
            // btn_configedit
            // 
            this.btn_configedit.Location = new System.Drawing.Point(207, 143);
            this.btn_configedit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_configedit.Name = "btn_configedit";
            this.btn_configedit.Size = new System.Drawing.Size(93, 49);
            this.btn_configedit.TabIndex = 9;
            this.btn_configedit.Text = "config anpassen";
            this.btn_configedit.UseVisualStyleBackColor = true;
            this.btn_configedit.Click += new System.EventHandler(this.btn_configedit_Click);
            // 
            // tb_configload
            // 
            this.tb_configload.Location = new System.Drawing.Point(108, 258);
            this.tb_configload.Margin = new System.Windows.Forms.Padding(4);
            this.tb_configload.Name = "tb_configload";
            this.tb_configload.Size = new System.Drawing.Size(343, 22);
            this.tb_configload.TabIndex = 8;
            // 
            // btn_quit
            // 
            this.btn_quit.Location = new System.Drawing.Point(359, 299);
            this.btn_quit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_quit.Name = "btn_quit";
            this.btn_quit.Size = new System.Drawing.Size(93, 49);
            this.btn_quit.TabIndex = 7;
            this.btn_quit.Text = "Programm beenden";
            this.btn_quit.UseVisualStyleBackColor = true;
            this.btn_quit.Click += new System.EventHandler(this.btn_quit_Click);
            // 
            // btn_configload
            // 
            this.btn_configload.Location = new System.Drawing.Point(8, 245);
            this.btn_configload.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_configload.Name = "btn_configload";
            this.btn_configload.Size = new System.Drawing.Size(93, 49);
            this.btn_configload.TabIndex = 6;
            this.btn_configload.Text = "config laden";
            this.btn_configload.UseVisualStyleBackColor = true;
            this.btn_configload.Click += new System.EventHandler(this.btn_configload_Click);
            // 
            // btn_overview
            // 
            this.btn_overview.Location = new System.Drawing.Point(8, 143);
            this.btn_overview.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_overview.Name = "btn_overview";
            this.btn_overview.Size = new System.Drawing.Size(93, 49);
            this.btn_overview.TabIndex = 2;
            this.btn_overview.Text = "Übersicht erstellen";
            this.btn_overview.UseVisualStyleBackColor = true;
            this.btn_overview.Click += new System.EventHandler(this.btn_overview_Click);
            // 
            // btn_pathoutput
            // 
            this.btn_pathoutput.Location = new System.Drawing.Point(8, 75);
            this.btn_pathoutput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_pathoutput.Name = "btn_pathoutput";
            this.btn_pathoutput.Size = new System.Drawing.Size(93, 49);
            this.btn_pathoutput.TabIndex = 1;
            this.btn_pathoutput.Text = "ordner saves";
            this.btn_pathoutput.UseVisualStyleBackColor = true;
            // 
            // tb_pdfload
            // 
            this.tb_pdfload.Location = new System.Drawing.Point(108, 21);
            this.tb_pdfload.Margin = new System.Windows.Forms.Padding(4);
            this.tb_pdfload.Name = "tb_pdfload";
            this.tb_pdfload.Size = new System.Drawing.Size(343, 22);
            this.tb_pdfload.TabIndex = 4;
            // 
            // tb_pathoutput
            // 
            this.tb_pathoutput.Location = new System.Drawing.Point(108, 89);
            this.tb_pathoutput.Margin = new System.Windows.Forms.Padding(4);
            this.tb_pathoutput.Name = "tb_pathoutput";
            this.tb_pathoutput.Size = new System.Drawing.Size(343, 22);
            this.tb_pathoutput.TabIndex = 5;
            // 
            // btn_fillpdf
            // 
            this.btn_fillpdf.Location = new System.Drawing.Point(8, 299);
            this.btn_fillpdf.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_fillpdf.Name = "btn_fillpdf";
            this.btn_fillpdf.Size = new System.Drawing.Size(93, 49);
            this.btn_fillpdf.TabIndex = 10;
            this.btn_fillpdf.Text = "pdf füllen";
            this.btn_fillpdf.UseVisualStyleBackColor = true;
            this.btn_fillpdf.Click += new System.EventHandler(this.btn_fillpdf_Click);
            // 
            // btn_help
            // 
            this.btn_help.Location = new System.Drawing.Point(260, 299);
            this.btn_help.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_help.Name = "btn_help";
            this.btn_help.Size = new System.Drawing.Size(93, 49);
            this.btn_help.TabIndex = 11;
            this.btn_help.Text = "???";
            this.btn_help.UseVisualStyleBackColor = true;
            this.btn_help.Click += new System.EventHandler(this.btn_help_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 356);
            this.Controls.Add(this.btn_help);
            this.Controls.Add(this.btn_fillpdf);
            this.Controls.Add(this.btn_configedit);
            this.Controls.Add(this.tb_configload);
            this.Controls.Add(this.btn_quit);
            this.Controls.Add(this.btn_configload);
            this.Controls.Add(this.tb_pathoutput);
            this.Controls.Add(this.tb_pdfload);
            this.Controls.Add(this.btn_configcreate);
            this.Controls.Add(this.btn_overview);
            this.Controls.Add(this.btn_pathoutput);
            this.Controls.Add(this.btn_pdfload);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Text = "PDF Form Filler";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_pdfload;
        private System.Windows.Forms.Button btn_configcreate;
        private System.Windows.Forms.Button btn_configedit;
        private System.Windows.Forms.TextBox tb_configload;
        private System.Windows.Forms.Button btn_quit;
        private System.Windows.Forms.Button btn_configload;
        private System.Windows.Forms.Button btn_overview;
        private System.Windows.Forms.Button btn_pathoutput;
        private System.Windows.Forms.TextBox tb_pdfload;
        private System.Windows.Forms.TextBox tb_pathoutput;
        private System.Windows.Forms.Button btn_fillpdf;
        private System.Windows.Forms.Button btn_help;
    }
}

