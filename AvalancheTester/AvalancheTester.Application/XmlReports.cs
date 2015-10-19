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
        private const string path = "../../../XML reports/";

        public void LastMonth(Place place, DateTime today)
        {
            AvalancheTestsDbEntities db = new AvalancheTestsDbEntities();

            List<Test> tests = db.Tests
                .Where(t => t.Place.Name == place.Name)
                .Where(t => (t.Date >= today.AddMonths(-1) && t.Date <= today))
                .ToList();

            this.XmlSerializeResult(tests, "monthly-tests-"+place.Name+".xml");
        }

        public void LastWeek(Place place, DateTime today)
        {
            AvalancheTestsDbEntities db = new AvalancheTestsDbEntities();

            List<Test> tests = db.Tests
                .Where(t => t.Place.Name == place.Name)
                .Where(t => (t.Date >= today.AddDays(-7) && t.Date <= today))
                .ToList();

            this.XmlSerializeResult(tests, "weekly-tests-"+place.Name+".xml");
        }

        public void LastYear(Place place, DateTime today)
        {
            AvalancheTestsDbEntities db = new AvalancheTestsDbEntities();

            var tests = db.Tests
                .Where(t => t.Place.Name == place.Name)
                .Where(t => (t.Date >= today.AddYears(-1) && t.Date <= today))
                .GroupBy(gr => new {gr.Date.Year, gr.Date.Month })
                .ToList();

            this.XmlSerializeResult(tests, "yearly-tests-"+place.Name+".xml");
        }

        private void XmlSerializeResult(dynamic tests, string fileName)
        {
             var xmlSerializer=new XmlSerializer(tests.GetType());
            StreamWriter file = new StreamWriter(path);

            xmlSerializer.Serialize(file, tests);
        }
    }
}
