using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Device.Location;
using System.Linq;

namespace AvalancheTester.Application.DbHandlers.MongoDb
{
    public static class MongoDbDataGenerator
    {
        const string DatabaseHost = "mongodb://127.0.0.1";
        const string DatabaseName = "AvalancheTestsDb";

        public static void Populate(MongoDatabase db)
        {
            // Remove old entries to avoid duplication
            ClearDatabase(db);

            MongoCollection<MongoUser> users = db.GetCollection<MongoUser>("Users");

            users.Insert(new MongoUser()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = "Пешо Петров"
            });

            users.Insert(new MongoUser()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = "Христо Христосков"
            });

            //check entries
            var usersList = users.FindAll().ToList();

            MongoCollection<MongoTest> tests = db.GetCollection<MongoTest>("Tests");

            //places are pased from xml later
            tests.Insert(new MongoTest()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                UserId = usersList[0].Id,
                TestResult = "Компресионен тест 2 повторения. Слой 163-154 см. – СТ1, СТ3; Сл...",
                DangerLevel = 2,
                Time = new DateTime(2015, 1, 13, 9, 48, 0),
                //Position = new GeoCoordinate(42.34356654335, 23.34356654335),
                Slope = 23
            });

            tests.Insert(new MongoTest()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                UserId = usersList[1].Id,
                TestResult = "Компресионен тест: Слой 163-120 см. – СТ1, СТ3...",
                DangerLevel = 5,
                Time = new DateTime(2015, 2, 15, 9, 48, 0),
                //Position = new GeoCoordinate(42.34356654335, 23.34356654335),
                Slope = 23

            });

            MongoCollection<MongoPlace> places = db.GetCollection<MongoPlace>("Places");

            places.Insert(new MongoPlace()
            {
                Id = 4,
                Name = "Стара Планина"
            });

            places.Insert(new MongoPlace()
            {
                Id = 5,
                Name = "Родопи"
            });
        }

        public static void ClearDatabase(MongoDatabase db)
        {
            db.Drop();
        }

        public static MongoDatabase GetDatabase(string name=DatabaseName, string fromHost=DatabaseHost)
        {
            var mongoClient = new MongoClient(fromHost);
            var server = mongoClient.GetServer();
            return server.GetDatabase(name);
        }
    }
}
