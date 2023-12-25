using iText.Forms.Fields;
using iText.Forms;
using iText.Kernel.Pdf;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using PDF_Form_Filler.Properties;
using PDF_Form_Filler.Models;
using System.Runtime;

namespace PDF_Form_Filler.Utility
{
    public class PdfTools
    {
        public enum Readmode
        {
            ReadAll = 0,
            ReadTextfields = 1,
            ReadCheckBoxes = 2,
            ReadGroups = 3,
        }

        public static void ShowFieldsInPDF(string filePath, string outputDir)
        {
            var originalFilepath = $"{Path.Combine(outputDir, Path.GetFileNameWithoutExtension(filePath))}_original.pdf";
            var fields = ReadPdfForms(filePath);
            var outputFilepath = $"{Path.Combine(outputDir, Path.GetFileNameWithoutExtension(filePath))}_overview.pdf";

            CheckForOriginal(filePath, originalFilepath);

            using (PdfReader pdfReader = new PdfReader(filePath))
            {
                using (PdfWriter pdfWriter = new PdfWriter(outputFilepath))
                {
                    using (PdfDocument pdfDocument = new PdfDocument(pdfReader, pdfWriter))
                    {
                        PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDocument, true);

                        if (form != null)
                        {
                            foreach (var item in fields)
                            {
                                PdfFormField field = form.GetField(item.Name);

                                if (field != null)
                                {
                                    field.SetValue(item.Name);
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void CheckForOriginal(string filePath, string originalFilepath)
        {
            if (!File.Exists(originalFilepath))
            {
                File.Copy(filePath, originalFilepath);
            }
        }

        public static List<PdfField> ReadPdfForms(string filePath, Readmode readmode = Readmode.ReadAll)
        {
            List<PdfField> fieldList = new List<PdfField>();

            using (PdfReader pdfReader = new PdfReader(filePath))
            {
                using (PdfDocument pdfDocument = new PdfDocument(pdfReader))
                {
                    PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDocument, false);

                    if (form != null)
                    {
                        var fields = form.GetAllFormFields();

                        if (fields != null && fields.Count > 0)
                        {
                            // Text fields
                            foreach (var field in fields.Values)
                            {
                                // Text fields
                                if ((readmode == Readmode.ReadAll)
                                    || (readmode == Readmode.ReadTextfields && field.GetFormType().ToString() == "/Tx")
                                    || (readmode == Readmode.ReadCheckBoxes && field.GetFormType().ToString() == "/Btn")
                                //|| (readmode == Readmode.ReadGroups && field.GetFormType().ToString() == "/Btn" && field.GetValue().ToString().StartsWith("/"))
                                )
                                {
                                    var newField = new PdfField();
                                    newField.Name = field.GetFieldName().ToString();
                                    newField.Value = field.GetValue()?.ToString() != null ? field.GetValue()?.ToString() : "";
                                    newField.Type = field.GetFormType().ToString();
                                    fieldList.Add(newField);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Keine Felder im .pdf gefunden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Das .pdf enthält kein interaktives  Feld.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            return fieldList;
        }

        public static void CreateConfig(string filePath, string outputDir)
        {
            Config config = new Config();
            var originalFilepath = $"{Path.Combine(outputDir, Path.GetFileNameWithoutExtension(filePath))}_original.pdf";
            var fields = ReadPdfForms(filePath);
            var outputFilepath = $"{Path.Combine(outputDir, Path.GetFileNameWithoutExtension(filePath))}.json";

            CheckForOriginal(filePath, originalFilepath);

            config.Fields = fields;
            config.Save(outputFilepath);
        }

        public static void FillPDF(string filePath, string outputDir, Config config)
        {
            var originalFilepath = $"{Path.Combine(outputDir, Path.GetFileNameWithoutExtension(filePath))}_original.pdf";
            CheckForOriginal(filePath, originalFilepath);
            var outputFilePath = $"{Path.Combine(outputDir, Path.GetFileNameWithoutExtension(filePath))}_modified.pdf";
            File.Copy(filePath, outputFilePath, true);


            using (PdfReader pdfReader = new PdfReader(filePath))
            {
                using (PdfWriter pdfWriter = new PdfWriter(outputFilePath))
                {
                    using (PdfDocument pdfDocument = new PdfDocument(pdfReader, pdfWriter))
                    {
                        PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDocument, true);

                        if (form != null)
                        {
                            foreach (PdfField field in config.Fields)
                            {
                                PdfFormField pdfField = form.GetField(field.Name);

                                if (pdfField != null)
                                {
                                    pdfField.SetValue(field.Value);
                                }
                            }

                        }
                    }
                }
            }
        }
    }
}
