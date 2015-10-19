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
                UserMemberships = t.Organizations.Select(o =>  o.Name),
                Locations = t.User.Tests.Select(t2=>t2.Place.Name),
                Date = t.Date,
                UsersTestCount = t.User.Tests.Count
            })
                .GroupBy(gr => gr.Date.Year)
                .ToList();

            //Bad performance! Try without ToList.
            /*var groupsTests2 = db.Users.Select(u => new
            {
                UserName = u.Name,
                Memberships = u.Organizations.Select(o => o.Name),
                Locations = u.Tests.Select(t => t.Place.Name),
                Count=u.Tests.Count

            }).GroupBy();*/

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
                doc.Add(Chunk.NEWLINE);

                innerTable.AddCell("User Name");
                innerTable.AddCell("User Memberships");
                innerTable.AddCell("Locations");
                //innerTable.AddCell("Date");
                innerTable.AddCell("Tests count");

                foreach (var item in gr)
                {
                    innerTable.AddCell(item.Name);
                    innerTable.AddCell(string.Join(", ", item.UserMemberships));
                    innerTable.AddCell(string.Join(", ", item.Locations));
                    //innerTable.AddCell(item.Date.ToString());
                    innerTable.AddCell(item.UsersTestCount.ToString());
                }

                table.AddCell(innerTable);
            }

            doc.Add(table);
            doc.Close();
        }

    }
}
