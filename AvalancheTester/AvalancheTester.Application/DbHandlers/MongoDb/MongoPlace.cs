using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AvalancheTester.Application.DbHandlers.MongoDb
{
    // Populate from XML file
    public class MongoPlace
    {
        [BsonRepresentation(BsonType.Int32)]
        public int Id { get; set; }

        public string Name { get; set; }
        
        // May remain unused
        public List<PointF> Area { get; set; }
    }
}
