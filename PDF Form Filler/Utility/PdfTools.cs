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
                            //foreach (var mystring in strings)
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
            //List<string> strings = new List<string>();
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
                                    //strings.Add(field.GetFieldName().ToString());
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Fehler", "Keine Felder im .pdf gefunden.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Fehler", "Das .pdf enthält kein interaktives  Feld.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            //return strings;
            return fieldList;
        }

        public static void CreateConfig(string filePath, string outputDir/*, string configPath*/)
        {
            Config config = new Config();
            var originalFilepath = $"{Path.Combine(outputDir, Path.GetFileNameWithoutExtension(filePath))}_original.pdf";
            var fields = ReadPdfForms(filePath);
            var outputFilepath = $"{Path.Combine(outputDir, Path.GetFileNameWithoutExtension(filePath))}.json";

            CheckForOriginal(filePath, originalFilepath);

            config.Fields = fields;
            config.Save(outputFilepath);
        }


        //static void CreateEmptyConfig(string filePath, string outputFilepath)
        //{
        //    Settings settings = new Settings();




        //    //var outputFilepath = $"{filePath.Split(".pdf")[0]}_modified.pdf";
        //    using (PdfReader pdfReader = new PdfReader(filePath))
        //    {
        //        using (PdfWriter pdfWriter = new PdfWriter(outputFilepath))
        //        {
        //            using (PdfDocument pdfDocument = new PdfDocument(pdfReader, pdfWriter))
        //            {
        //                PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDocument, true);

        //                if (form != null)
        //                {
        //                    // static fields from config: Kunde
        //                    PdfFormField field_Vorname_Name1 = form.GetField("E1_Text1");
        //                    PdfFormField field_Vorname_Name2 = form.GetField("E1_Text5");
        //                    PdfFormField field_Vorname_Name3 = form.GetField("E1_Text9");
        //                    PdfFormField field_Vorname_Name4 = form.GetField("E2_Text1");
        //                    PdfFormField field_Vorname_Name5 = form.GetField("E3_Text1");
        //                    field_Vorname_Name1.SetValue($"{settings.Kunde.Single(x => x.Key == "Vorname").Value} {settings.Kunde.Single(x => x.Key == "Name").Value}");
        //                    field_Vorname_Name2.SetValue($"{settings.Kunde.Single(x => x.Key == "Vorname").Value} {settings.Kunde.Single(x => x.Key == "Name").Value}");
        //                    field_Vorname_Name3.SetValue($"{settings.Kunde.Single(x => x.Key == "Vorname").Value} {settings.Kunde.Single(x => x.Key == "Name").Value}");
        //                    field_Vorname_Name4.SetValue($"{settings.Kunde.Single(x => x.Key == "Vorname").Value} {settings.Kunde.Single(x => x.Key == "Name").Value}");
        //                    field_Vorname_Name5.SetValue($"{settings.Kunde.Single(x => x.Key == "Vorname").Value} {settings.Kunde.Single(x => x.Key == "Name").Value}");

        //                    PdfFormField field_Str_Hausnr1 = form.GetField("E1_Text2");
        //                    PdfFormField field_Str_Hausnr2 = form.GetField("E1_Text6");
        //                    PdfFormField field_Str_Hausnr3 = form.GetField("E1_Text10");
        //                    PdfFormField field_Str_Hausnr4 = form.GetField("E2_Text2");
        //                    PdfFormField field_Str_Hausnr5 = form.GetField("E3_Text2");
        //                    field_Str_Hausnr1.SetValue($"{settings.Kunde.Single(x => x.Key == "Strasse").Value} {settings.Kunde.Single(x => x.Key == "Hausnummer").Value}");
        //                    field_Str_Hausnr2.SetValue($"{settings.Kunde.Single(x => x.Key == "Strasse").Value} {settings.Kunde.Single(x => x.Key == "Hausnummer").Value}");
        //                    field_Str_Hausnr3.SetValue($"{settings.Kunde.Single(x => x.Key == "Strasse").Value} {settings.Kunde.Single(x => x.Key == "Hausnummer").Value}");
        //                    field_Str_Hausnr4.SetValue($"{settings.Kunde.Single(x => x.Key == "Strasse").Value} {settings.Kunde.Single(x => x.Key == "Hausnummer").Value}");
        //                    field_Str_Hausnr5.SetValue($"{settings.Kunde.Single(x => x.Key == "Strasse").Value} {settings.Kunde.Single(x => x.Key == "Hausnummer").Value}");

        //                    PdfFormField field_PLZ_Ort1 = form.GetField("E1_Text3");
        //                    PdfFormField field_PLZ_Ort2 = form.GetField("E1_Text7");
        //                    PdfFormField field_PLZ_Ort3 = form.GetField("E1_Text11");
        //                    PdfFormField field_PLZ_Ort4 = form.GetField("E2_Text3");
        //                    PdfFormField field_PLZ_Ort5 = form.GetField("E3_Text3");
        //                    field_PLZ_Ort1.SetValue($"{settings.Kunde.Single(x => x.Key == "PLZ").Value} {settings.Kunde.Single(x => x.Key == "Ort").Value}");
        //                    field_PLZ_Ort2.SetValue($"{settings.Kunde.Single(x => x.Key == "PLZ").Value} {settings.Kunde.Single(x => x.Key == "Ort").Value}");
        //                    field_PLZ_Ort3.SetValue($"{settings.Kunde.Single(x => x.Key == "PLZ").Value} {settings.Kunde.Single(x => x.Key == "Ort").Value}");
        //                    field_PLZ_Ort4.SetValue($"{settings.Kunde.Single(x => x.Key == "PLZ").Value} {settings.Kunde.Single(x => x.Key == "Ort").Value}");
        //                    field_PLZ_Ort5.SetValue($"{settings.Kunde.Single(x => x.Key == "PLZ").Value} {settings.Kunde.Single(x => x.Key == "Ort").Value}");

        //                    PdfFormField field_Tel_Mail1 = form.GetField("E1_Text4");
        //                    PdfFormField field_Tel_Mail2 = form.GetField("E1_Text8");
        //                    PdfFormField field_Tel_Mail3 = form.GetField("E1_Text12");
        //                    field_Tel_Mail1.SetValue($"{settings.Kunde.Single(x => x.Key == "Telefon").Value} {settings.Kunde.Single(x => x.Key == "Email").Value}");
        //                    field_Tel_Mail2.SetValue($"{settings.Kunde.Single(x => x.Key == "Telefon").Value} {settings.Kunde.Single(x => x.Key == "Email").Value}");
        //                    field_Tel_Mail3.SetValue($"{settings.Kunde.Single(x => x.Key == "Telefon").Value} {settings.Kunde.Single(x => x.Key == "Email").Value}");


        //                    // static fields from config: Kunde
        //                    PdfFormField Selbst_Firma_Ort1 = form.GetField("E1_Text13");
        //                    PdfFormField Selbst_Firma_Ort2 = form.GetField("E3_Text4");
        //                    Selbst_Firma_Ort1.SetValue($"{settings.Selbst.Single(x => x.Key == "Firma").Value} {settings.Selbst.Single(x => x.Key == "Ort").Value}");
        //                    Selbst_Firma_Ort2.SetValue($"{settings.Selbst.Single(x => x.Key == "Firma").Value} {settings.Selbst.Single(x => x.Key == "Ort").Value}");

        //                    PdfFormField Selbst_Eintragungsnummer1 = form.GetField("E1_Text14");
        //                    Selbst_Eintragungsnummer1.SetValue($"{settings.Selbst.Single(x => x.Key == "Eintragungsnummer").Value}");

        //                    PdfFormField Selbst_Netzbetreiber1 = form.GetField("E1_Text15");
        //                    Selbst_Netzbetreiber1.SetValue($"{settings.Selbst.Single(x => x.Key == "Netzbetreiber").Value}");

        //                    PdfFormField Selbst_Einrichter_Telefon1 = form.GetField("E3_Text5");
        //                    Selbst_Einrichter_Telefon1.SetValue($"{settings.Selbst.Single(x => x.Key == "Einrichter_Telefon").Value}");

        //                    PdfFormField Selbst_Einrichter_Email1 = form.GetField("E3_Text6");
        //                    Selbst_Einrichter_Email1.SetValue($"{settings.Selbst.Single(x => x.Key == "Einrichter_Email").Value}");


        //                    // Textfields
        //                    foreach (var keyvaluepair in settings.Textfields)
        //                    {
        //                        PdfFormField field = form.GetField(keyvaluepair.Key);

        //                        if (field != null)
        //                        {
        //                            field.SetValue(keyvaluepair.Value);
        //                        }
        //                    }
        //                    // Checkboxes
        //                    foreach (var keyvaluepair in settings.Checkboxes)
        //                    {
        //                        PdfFormField field = form.GetField(keyvaluepair.Key);

        //                        if (field != null)
        //                        {
        //                            field.SetValue(keyvaluepair.Value);
        //                        }
        //                    }
        //                    // Groups
        //                    foreach (var keyvaluepair in settings.Groups)
        //                    {
        //                        PdfFormField field = form.GetField(keyvaluepair.Key);

        //                        if (field != null)
        //                        {
        //                            field.SetValue(keyvaluepair.Value);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

    }
}
