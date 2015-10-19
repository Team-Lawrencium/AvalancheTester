using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Device.Location;

namespace AvalancheTester.Application.DbHandlers.MongoDb
{
    // TODO: constraints
    public class MongoTest
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string UserId { get; set; }

        public string PlaceId { get; set; }

        public string TestResult { get; set; }

        // Between 1 and 5
        public int DangerLevel { get; set; }

        public DateTime Time { get; set; }

        public GeoCoordinate Position { get; set; }

        public int Slope { get; set; }
    }
}
