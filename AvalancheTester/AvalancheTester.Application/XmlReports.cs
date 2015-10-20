using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AvalancheTester.Application
{
    public static class XmlReports
    {
        private const string path = "../../../XML reports/";

        public static void LastMonth(Place place, DateTime today)
        {
            AvalancheTestsDbEntities db = new AvalancheTestsDbEntities();

            var startDate = today.AddMonths(-1);
            List<Test> tests = db.Tests
                .Where(t => t.Place.Name == place.Name)
                .Where(t => (t.Date >= startDate && t.Date <= today))
                .ToList();

            XmlSerializeResult(tests, "monthly-tests-" + place.Name + ".xml");
        }

        public static void LastWeek(Place place, DateTime today)
        {
            AvalancheTestsDbEntities db = new AvalancheTestsDbEntities();

            var startDate = today.AddDays(-7);
            List<Test> tests = db.Tests
                .Where(t => t.Place.Name == place.Name)
                .Where(t => (t.Date >= startDate && t.Date <= today))
                .ToList();

            XmlSerializeResult(tests, "weekly-tests-" + place.Name + ".xml");
        }

        public static void LastYear(Place place, DateTime today)
        {
            AvalancheTestsDbEntities db = new AvalancheTestsDbEntities();

            var startDate = today.AddYears(-1);
            var tests = db.Tests
                .Where(t => t.Place.Name == place.Name)
                .Where(t => (t.Date >= startDate && t.Date <= today))
                .GroupBy(gr => new { gr.Date.Year, gr.Date.Month })
                .ToList();

            XmlSerializeResult(tests, "yearly-tests-" + place.Name + ".xml");
        }

        private static void XmlSerializeResult(dynamic tests, string fileName)
        {
            var xmlSerializer = new XmlSerializer(tests.GetType());
            StreamWriter file = new StreamWriter(path + fileName);

            xmlSerializer.Serialize(file, tests);
        }
    }
}
