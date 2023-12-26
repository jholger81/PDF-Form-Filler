using iText.Forms.Fields;
using iText.Forms;
using iText.Kernel.Pdf;
using iText.Layout;
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
using System.Drawing;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Geom;
using System.Drawing.Printing;
using iText.Layout.Renderer;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Colors;

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
            var originalFilepath = $"{System.IO.Path.Combine(outputDir, System.IO.Path.GetFileNameWithoutExtension(filePath))}_original.pdf";
            var fields = ReadPdfForms(filePath);
            var outputFilepath = $"{System.IO.Path.Combine(outputDir, System.IO.Path.GetFileNameWithoutExtension(filePath))}_overview.pdf";

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
                                    if (item.Type.ToLower() == "/tx")
                                    {
                                        field.SetValue(item.Name);
                                    }
                                }
                            }
                        }
                    }
                }
                foreach (var item in fields)
                {
                    if (item.Type.ToLower() == "/btn")
                    {
                        WriteTextToPdf(outputFilepath, item);
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
                                    newField.Attributes = GetFieldPosition(filePath, newField.Name);
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
            var originalFilepath = $"{System.IO.Path.Combine(outputDir, System.IO.Path.GetFileNameWithoutExtension(filePath))}_original.pdf";
            var fields = ReadPdfForms(filePath);
            var outputFilepath = $"{System.IO.Path.Combine(outputDir, System.IO.Path.GetFileNameWithoutExtension(filePath))}.json";

            CheckForOriginal(filePath, originalFilepath);

            config.Fields = fields;
            config.Save(outputFilepath);
        }

        public static void FillPDF(string filePath, string outputDir, Config config)
        {
            var originalFilepath = $"{System.IO.Path.Combine(outputDir, System.IO.Path.GetFileNameWithoutExtension(filePath))}_original.pdf";
            CheckForOriginal(filePath, originalFilepath);
            var outputFilePath = $"{System.IO.Path.Combine(outputDir, System.IO.Path.GetFileNameWithoutExtension(filePath))}_modified.pdf";
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

        public static PdfFieldAttributes GetFieldPosition(string filePath, string fieldName)
        {
            PdfFieldAttributes attributes = new PdfFieldAttributes();

            using (PdfReader reader = new PdfReader(filePath))
            {
                using (PdfDocument pdfDoc = new PdfDocument(reader))
                {
                    PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDoc, false);
                    PdfFormField field = form.GetField(fieldName);
                    PdfPage page = field.GetWidgets().First().GetPage();
                    attributes.Page = pdfDoc.GetPageNumber(page);

                    PdfArray annotationRect = field.GetWidgets().First().GetRectangle();
                    attributes.X = annotationRect.GetAsNumber(0).FloatValue();
                    attributes.Y = annotationRect.GetAsNumber(1).FloatValue();
                }
            }
            return attributes;
        }

        public static void WriteTextToPdf(string outputFilePath, PdfField field)
        {
            string filePath = $"{outputFilePath}_temp";
            File.Copy(outputFilePath, filePath);

            PdfDocument srcDocument = new PdfDocument(new PdfReader(filePath));
            PdfDocument destDocument = new PdfDocument(new PdfWriter(outputFilePath));

            FontProgram fontProgram = FontProgramFactory.CreateFont();
            PdfFont calibri = PdfFontFactory.CreateFont(fontProgram, PdfEncodings.WINANSI);

            int pagesCount = srcDocument.GetNumberOfPages();
            for (int i = 1; i <= pagesCount; i++)
            {
                srcDocument.CopyPagesTo(i, i, destDocument);
                if (field.Attributes.Page != i)
                    continue;
                PdfCanvas pdfCanvas = new PdfCanvas(destDocument.GetPage(i));
                Canvas canvas = new Canvas(pdfCanvas, new iText.Kernel.Geom.Rectangle(field.Attributes.X, field.Attributes.Y-35, 100, 50)); // TODO : -35 ersetzen, Abstand/Höhe des footers?
                canvas.Add(new Paragraph(field.Name).SetRotationAngle(0).SetFont(calibri).SetFontSize(10).SetFontColor(ColorConstants.RED).SetBackgroundColor(ColorConstants.WHITE));
                canvas.Close();
            }
            srcDocument.Close();
            destDocument.Close();

            File.Delete(filePath);
        }

        public static void WriteTextToPdf(string outputFilePath, List<PdfField> fields)
        {
            throw new NotImplementedException();
        }
    }
}
