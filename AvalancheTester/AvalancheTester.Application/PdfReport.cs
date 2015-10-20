namespace AvalancheTester.Application
{
    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using System.IO;
    using System.Linq;

    public static class PdfReport
    {
        private const string PdfReportFilePath = "../../../PDF Reports/AvalancheTestReport.pdf";
        private const int NumberOfColumns = 1;

        public static void CreatePdf()
        {
            var db = new AvalancheTestsDbEntities();

            var groupsTests = db.Tests.Select(t => new
            {
                Name = t.User.Name,
                UserMemberships = t.Organizations.Select(o => o.Name),
                Locations = t.User.Tests.Select(t2 => t2.Place.Name),
                Date = t.Date,
                UsersTestCount = t.User.Tests.Count
            })
                .GroupBy(gr => new { gr.Date.Year, gr.Date.Month })
                .ToList();

            FileStream fileStream = new FileStream(PdfReportFilePath, FileMode.Create, FileAccess.Write, FileShare.None);
            Document doc = new Document();
            PdfWriter pdfWriter = PdfWriter.GetInstance(doc, fileStream);

            doc.Open();

            var table = new PdfPTable(NumberOfColumns);
            table.AddCell("Annual User Tests Report");
            doc.Add(Chunk.NEWLINE);

            foreach (var gr in groupsTests)
            {
                table.AddCell(gr.Key.Year.ToString() + "-" + gr.Key.Month.ToString());
                doc.Add(Chunk.NEWLINE);

                var innerTable = new PdfPTable(4);

                innerTable.AddCell("User Name");
                innerTable.AddCell("User Memberships");
                innerTable.AddCell("Locations");
                innerTable.AddCell("Tests count");

                foreach (var item in gr)
                {
                    innerTable.AddCell(item.Name.ToString());
                    innerTable.AddCell(string.Join(", ", item.UserMemberships) + " ");
                    innerTable.AddCell(string.Join(", ", item.Locations) + " ");
                    innerTable.AddCell(item.UsersTestCount.ToString());
                }

                table.AddCell(innerTable);
            }

            doc.Add(table);
            doc.Close();
        }
    }
}
