namespace ParsingXML
{
    using System;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Xml.Linq;
    using AvalancheTester.Application;

    public static class XmlImporter
    {
        public static void ImportToDb(string location)
        {
            var db = new AvalancheTestsDbEntities();

            XDocument document = XDocument.Load(location);

            var places = from place in document.Descendants("Place")
                         select new
                         {
                             Name = place.Attribute("name").Value,
                             Area = place.Attribute("area").Value
                         };

            foreach (var place in places)
            {
                var currentPlace = new Place()
                {
                    PlaceId = new Random().Next(1, 10000),
                    Name = place.Name,
                    Area = DbGeometry.FromText(place.Area)
                };

                db.Places.Add(currentPlace);
            }

            db.SaveChanges();
            db.Dispose();
        }
    }
}
