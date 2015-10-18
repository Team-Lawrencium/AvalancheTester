namespace PdfReport
{
    using System;
    using System.IO;
    using iTextSharp.text;
    using iTextSharp.text.pdf;

    public static class PdfReport
    {
        public static void CreatePdf(string message)
        {
            FileStream fileStream = new FileStream("../../AvalancheTestReport.pdf", FileMode.Create, FileAccess.Write, FileShare.None);

            Document document = new Document();

            PdfWriter pdfWriter = PdfWriter.GetInstance(document, fileStream);

            document.Open();

            var paragraph = new Paragraph(message);
            document.Add(paragraph);

            document.Close();
        }
    }
}
