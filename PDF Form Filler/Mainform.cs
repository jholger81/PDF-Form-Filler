using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDF_Form_Filler
{
    public partial class MainForm : Form
    {
        public Config config = new Config();
        public MainForm()
        {
            InitializeComponent();
        }

        private void btn_pdfload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.Filter = "pdf files (*.pdf)|*.pdf|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.RestoreDirectory = true;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    this.tb_pdfload.Text = openFileDialog1.FileName;
                    string savedir = Path.GetDirectoryName(openFileDialog1.FileName) + $"\\{openFileDialog1.SafeFileName}";
                    savedir = savedir.Substring(0, savedir.LastIndexOf('.'));
                    this.tb_pathoutput.Text = savedir;
                }
            }
        }

        private void btn_pathoutput_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    this.tb_pathoutput.Text = openFileDialog1.FileName;
                }
            }
        }

        private void btn_overview_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(this.tb_pdfload.Text) || String.IsNullOrWhiteSpace(this.tb_pathoutput.Text))
                return;
            Directory.CreateDirectory(this.tb_pathoutput.Text);
            Utility.PdfTools.ShowFieldsInPDF(this.tb_pdfload.Text, this.tb_pathoutput.Text);
        }

        private void btn_configcreate_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(this.tb_pdfload.Text) || String.IsNullOrWhiteSpace(this.tb_pathoutput.Text))
                return;
            Directory.CreateDirectory(this.tb_pathoutput.Text);
            Utility.PdfTools.CreateConfig(this.tb_pdfload.Text, this.tb_pathoutput.Text);
            var fileName = $"{Path.Combine(this.tb_pathoutput.Text, Path.GetFileNameWithoutExtension(this.tb_pdfload.Text))}.json";
            this.tb_configload.Text = fileName;
        }

        private void btn_quit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_configedit_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(this.tb_pdfload.Text) || String.IsNullOrWhiteSpace(this.tb_pathoutput.Text))
                return;
            var fileName = $"{Path.Combine(this.tb_pathoutput.Text, Path.GetFileNameWithoutExtension(this.tb_pdfload.Text))}.json";
            if (!File.Exists(fileName))
                return;
            Process.Start(fileName);
        }

        private void btn_configload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.Filter = "json files (*.json)|*.json|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.RestoreDirectory = true;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    this.tb_configload.Text = openFileDialog1.FileName;
                }
            }
        }

        private void btn_fillpdf_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(this.tb_pdfload.Text) || String.IsNullOrWhiteSpace(this.tb_pathoutput.Text))
                return;
            var fileName = $"{Path.Combine(this.tb_pathoutput.Text, Path.GetFileNameWithoutExtension(this.tb_pdfload.Text))}.json";
            if (!File.Exists(fileName))
                return;
            config.Load(fileName);
            Utility.PdfTools.FillPDF(this.tb_pdfload.Text, this.tb_pathoutput.Text, config);
        }

        private void btn_help_Click(object sender, EventArgs e)
        {
            // TODO Hilfe Form erstellen und öffnen
            Console.WriteLine();
        }
    }
}
