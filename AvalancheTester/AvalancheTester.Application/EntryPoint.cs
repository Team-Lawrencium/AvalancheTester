
namespace AvalancheTester.Application
{
    public class EntryPoint
    {
        static void Main()
        {
            //MongoDatabase mongoDb = MongoDb.GetDatabase();
            //MongoDb.Populate(mongoDb);

            //Get all tests from collection "Tests" 
            //var tests = mongoDb.GetCollection<Test>("Tests").FindAll().ToList();
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

            //ExcelTableHandler excel = new ExcelTableHandler();

            //ExcelTableHandler.ReadFromExcel();

            /*ExcelTableHandler.WriteInExcel("Бай Иван Хижара", "Рила", "Маркуджиците", 23.4f, "10.02.2015",
                "Компресионен тест 3 повторения: Слой 163-120 см. – СТ1, СТ3...");*/


            // Adding some data to the tables
            /*var dataGenerator = new ManualDataGenerator();

            dataGenerator.GenerateData();*/

            var handler = new NewExcelTableHandler();

            handler.InputDataToDatabase();
        }
    }
}
