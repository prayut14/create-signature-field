using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace NTIPDF
{
    /// <summary>
    /// Class name: Utility
    /// Description: Utility for PDF
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// Create Signature field AcroForm
        /// </summary>
        /// <param name="signatureName">Signature field name</param>
        /// <param name="x">x (ตำแหน่งจากด้านซ้าย)</param>
        /// <param name="y">y (ตำแหน่งจากด้านล่าง)</param>
        /// <param name="width">Width (ความกว้าง)</param>
        /// <param name="height">Height (ความสูง)</param>
        /// <param name="pageNumber">Page number (เลขหน้า)</param>
        /// <returns></returns>
        public static byte[] CreateSignatureField(string signatureName, float x, float y, float width, float height,int pageNumber)
        {
            try
            {

                MemoryStream ms = new MemoryStream();
                Document document = new Document(PageSize.A4);
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                {
                    document.Open();
                    document.Add(new Paragraph("Example Document"));
                    PdfAcroForm acroForm = new PdfAcroForm(writer);
                    PdfFormField field = acroForm.AddSignature(signatureName, x, y, x + width, y + height);
                    field.MkBorderColor = BaseColor.White;
                    field.SetHighlighting(PdfAnnotation.HighlightNone);
                    field.SetFieldFlags(PdfAnnotation.FLAGS_READONLY | PdfAnnotation.FLAGS_PRINT);
                    field.FieldName = signatureName;
                    field.PlaceInPage = pageNumber;
                    writer.AddAnnotation(field);
                    document.Close();
                    writer.Close();
                    ms.Close();
                }

                return ms.GetBuffer();

            }
            catch (Exception ex) {
                return null;
            }
        }

    }
}
