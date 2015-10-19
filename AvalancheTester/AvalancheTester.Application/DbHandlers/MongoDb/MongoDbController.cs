namespace AvalancheTester.Application.DbHandlers.MongoDb
{
    using MongoDB.Driver;
    using System.Collections.Generic;

    public class MongoDbController
    {
        private string dbName;
        private string host;

        public MongoDbController(string dbName, string host)
        {
            this.dbName = dbName;
            this.host = host;
        }

        public List<MongoPlace> GetPlacesDataCollection()
        {
            IMongoDatabase db = GetDatabase();
            var collection = db.GetCollection<MongoPlace>("Places");
            var placesCollection = collection.Find(x => true).ToListAsync<MongoPlace>().Result;

            return placesCollection;
        }

        public IMongoDatabase GetDatabase()
        {
            var mongoClient = new MongoClient(host);

            return mongoClient.GetDatabase(dbName);
        }
    }
}