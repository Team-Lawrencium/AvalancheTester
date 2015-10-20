


namespace AvalancheTester.Application
{
    //using ParsingXML;

    public class EntryPoint
    {
        static void Main()
        {
            // Problem 1
            // Adding some data to the tables

            // Firts - generate some data in MongoDb
            //MongoDatabase mongoDb = MongoDbDataGenerator.GetDatabase();
            //MongoDbDataGenerator.Populate(mongoDb);

            // Second - generate some data in SQL Server Database
            //var dataGenerator = new ManualDataGenerator();
            //dataGenerator.GenerateData();


            // Reading form Excel 2003 files in Zip archive and adding data to SQL Server Database
            /*var excelTableHandler = new ExcelTableHandler();
            excelTableHandler.InputDataToDatabase();*/


            // Reading from MongoDb and writing to SQL Server db
            /*var mongoPlaceEntitiesController = new PlaceEntitiesController();
            mongoPlaceEntitiesController.UploadMongoPlacesToSql();*/

            // Problem 2 - Create PDF report
            //PdfReport.CreatePdf();

            // Problem 3 - Generate XML report - doesn't work
            //XmlReports.LastMonth(new Place { Name = "Rila" }, DateTime.Now);

            // Problem 4 - Json Reports
            //JsonReports.CreateReports();

            // Problem 5 - Load data form XML
            //XmlImporter.ImportToDb("../../XML/places.xml");

            // Problem 6
            // Write manually added data in Excel 2007 file
            /*ExcelTableHandler.WriteInExcel("Бай Иван Хижара", "Рила", "Маркуджиците", 23, "10.02.2015",
                "Компресионен тест 3 повторения: Слой 163-120 см. – СТ1, СТ3...");*/
        }
    }
}
