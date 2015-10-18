using AvalancheTester.Application.DbHandlers.MongoDb;
using MongoDB.Driver;
using System.Linq;

namespace AvalancheTester.Application
{
    public class EntryPoint
    {
        static void Main()
        {
            MongoDatabase mongoDb = MongoDb.GetDatabase();
            MongoDb.Populate(mongoDb);

            var tests = mongoDb.GetCollection<Test>("Tests").FindAll().ToList();
            /*
             * sqlServer.getConection()
             * mongoDb.getConnection()
             * var mongoData = mongo.getData()
             * sqlServer.putData(mongoData)
             * var excelData = excel.getData()
             * sqlServer.putData(excelData)
             * 
             * Json.export(sqlServer.getData(query))
             * PDF.export(sqlServer.getData(query))
             * XML.export(sqlServer.getData(query))
             * 
             * Console.WriteLine("Done"); */

            //PdfReport.PdfReport.CreatePdf("Testing PDF report!");
        }
    }
}
