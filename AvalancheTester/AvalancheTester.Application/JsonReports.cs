using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace AvalancheTester.Application
{
    public static class JsonReports
    {
        private const string path = "../../../Json reports/";
        public static void CreateReports()
        {
            var db = new AvalancheTestsDbEntities();

            var userReports = db.Users.Select(u => new
            {
                u.UserId,
                Name = u.Name,
                u.Organizations
            })
            .ToList();

            var serializer = new JsonSerializer();
            using (UserReportEntities MySqlDb = new UserReportEntities())
            {
                foreach (var user in userReports)
                {
                    StreamWriter file = new StreamWriter(path + user.Name + ".json");
                    serializer.Serialize(file, user);
                    file.Close();
                    //add to MySQL
                    userorganisations userOrganisations = new userorganisations()
                    {
                        UserId=user.UserId,
                        UserName=user.Name,
                        UserOrganisations1=string.Join(", ", user.Organizations)
                    };

                    //MySqlDb.userorganisations.Add(userOrganisations);
                    
                }

                //MySqlDb.SaveChanges();
            }
        }
    }
}
