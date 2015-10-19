using System;
using System.Collections.Generic;

namespace AvalancheTester.Application.DbHandlers.MongoDb
{
    public class PlaceEntitiesController
    {
        private const string DbName = "AvalancheTestsDb";
        private const string Host = "mongodb://127.0.0.1";

        private MongoDbController dbController = new MongoDbController(DbName, Host);

        public void UploadMongoPlacesToSql()
        {
            List<MongoPlace> placesCollection = dbController.GetPlacesDataCollection();

            using (var db = new AvalancheTestsDbEntities())
            {
                foreach (var place in placesCollection)
                {
                    db.Places.Add(GetPersonFromPersonMongo(place));
                }

                db.SaveChanges();
                Console.WriteLine("Success!");
            }
            Console.WriteLine("Press Enter");
            Console.ReadLine();
        }

        private Place GetPersonFromPersonMongo(MongoPlace mongoPlace)
        {
            var place = new Place();
            place.Name = mongoPlace.Name;

            return place;
        } 
    }
}