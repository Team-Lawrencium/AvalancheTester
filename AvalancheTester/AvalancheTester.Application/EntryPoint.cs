

using AvalancheTester.Application.DbHandlers.MongoDb;
using MongoDB.Driver;
using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace AvalancheTester.Application
{
    //using ParsingXML;

    public class EntryPoint
    {
        static void Main()
        {
            

            /*using (UserReportsEntities db=new UserReportsEntities())
            {

            }*/
            //Get all tests from collection "Tests" 
            //var tests = mongoDb.GetCollection<Test>("Tests").FindAll().ToList();
             /** sqlServer.getConection()
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

            // Adding some data to the tables

            //var dataGenerator = new ManualDataGenerator();

            //dataGenerator.GenerateData();

            /*var dataGenerator = new ManualDataGenerator();

            dataGenerator.GenerateData();*/

            // Reading form Excel 2003 files in Zip archive and adding data to SQL Server Database
            /*var excelTableHandler = new ExcelTableHandler();
            excelTableHandler.InputDataToDatabase();*/

            // Write manually added data in Excel 2007 file
            /*ExcelTableHandler.WriteInExcel("Бай Иван Хижара", "Рила", "Маркуджиците", 23, "10.02.2015",
                "Компресионен тест 3 повторения: Слой 163-120 см. – СТ1, СТ3...");*/
            //-------------------------------------------------------------------


            // Reading from MongoDb and writing to SQL Server db
            // Firts - generate some data
            /*MongoDatabase mongoDb = MongoDbDataGenerator.GetDatabase();
            MongoDbDataGenerator.Populate(mongoDb);*/

            /*var mongoPlaceEntitiesController = new PlaceEntitiesController();
            mongoPlaceEntitiesController.UploadMongoPlacesToSql();*/

            // Problem 2 - Create PDF report
            //PdfReport.CreatePdf();

            // Problem 3 - Generate XML report - doesn't work
            //XmlReports.LastMonth(new Place { Name = "Rila" }, DateTime.Now);

            // Problem 4 - Json Reports
            JsonReports.CreateReports();

            //XmlImporter.ImportToDb("../../XML/places.xml");
        }
    }
}
