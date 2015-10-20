using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace AvalancheTester.Application
{
    public static class JsonReports
    {
        private const string path = "../../Json reports/JasonReport.json";
        public static void CreateReports()
        {
            var db = new AvalancheTestsDbEntities();

            var userReports = db.Users.Select(u => new
            {
                UserId = u.UserId,
                Name = u.Name
            })
            .ToList();

            var serializer = new JsonSerializer();

            foreach (var user in userReports)
            {
                StreamWriter file = new StreamWriter(path);
                serializer.Serialize(file, user);
                file.Close();
            }

            Console.WriteLine("Json report generated!");
        }
    }
}