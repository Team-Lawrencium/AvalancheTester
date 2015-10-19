using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AvalancheTester.Application
{
    public class XmlReports
    {
        private const string path = "../../../XML reports/xml-report";

        public void LastYear(AvalancheTestsDbEntities db, Place place, DateTime today)
        {
            List<Test> tests = db.Tests
                .Where(t => t.Place.Name == place.Name)
                .Where(t => (t.Date >= today.AddYears(-1) && t.Date <= today))
                .ToList();

            var xmlSerializer=new XmlSerializer(tests.GetType());
            StreamWriter file = new StreamWriter(path);

            xmlSerializer.Serialize(file, tests);
        }
    }
}
