namespace AvalancheTester.Application
{
    using System;
    using System.IO;
    using System.Linq;
    using iTextSharp.text;
    using iTextSharp.text.pdf;

    public static class PdfReport
    {
        public static void CreatePdf()
        {
            var db = new AvalancheTestsDbEntities();

            var groupsTests = db.Tests.Select(t => new
            {
                Name = t.User.Name,
                UserMemberships = t.Organizations.Select(o => new
                {
                    OrganizationName = o.Name
                }),
                Locations = t.Place.Name,
                Date = t.Date,
                DangerLevel = t.DangerLevel
            })
                .GroupBy(gr => gr.Date)
                .ToList();

            FileStream fileStream = new FileStream("../../../PDF Reports/AvalancheTestReport.pdf", FileMode.Create, FileAccess.Write, FileShare.None);
            Document doc = new Document();
            PdfWriter pdfWriter = PdfWriter.GetInstance(doc, fileStream);

            doc.Open();

            var table = new PdfPTable(1);
            table.AddCell("Annual User Tests Report");
            doc.Add(Chunk.NEWLINE);

            foreach (var gr in groupsTests)
            {
                table.AddCell(gr.Key.ToString());
                doc.Add(Chunk.NEWLINE);

                var innerTable = new PdfPTable(5);

                innerTable.AddCell("User Name");
                innerTable.AddCell("User Memberships");
                innerTable.AddCell("Locations");
                innerTable.AddCell("Date");
                innerTable.AddCell("Danger Level");

                foreach (var item in gr)
                {
                    innerTable.AddCell(item.Name);
                    innerTable.AddCell(string.Join(", ", item.UserMemberships));
                    innerTable.AddCell(string.Join(", ", item.Locations));
                    innerTable.AddCell(item.Date.ToString());
                    innerTable.AddCell(item.DangerLevel.ToString());
                }

                table.AddCell(innerTable);
            }

            doc.Add(table);
            doc.Close();
        }

    }
}
