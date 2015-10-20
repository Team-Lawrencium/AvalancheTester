using System;

namespace AvalancheTester.Application
{
    public class ManualDataGenerator
    {
        public void GenerateData()
        {
            var db = new AvalancheTestsDbEntities();

            using (db)
            {
                Place vitosha = new Place()
                {
                    PlaceId = 1,
                    Name = "Vitosha"
                };
                db.Places.Add(vitosha);

                Place pirin = new Place()
                {
                    PlaceId = 2,
                    Name = "Pirin"
                };
                db.Places.Add(pirin);

                Place rila = new Place()
                {
                    PlaceId = 3,
                    Name = "Rila"
                };
                db.Places.Add(rila);

                User ivan = new User()
                {
                    UserId = 1,
                    Name = "Ivan Georgiev"
                };
                db.Users.Add(ivan);

                User petar = new User()
                {
                    UserId = 2,
                    Name = "Petar Popangelov",
                };
                db.Users.Add(petar);

                Organization rilaSport = new Organization()
                {
                    OrganizationId = 1,
                    Name = "Rila Sport"
                };
                rilaSport.Users.Add(petar);
                db.Organizations.Add(rilaSport);

                Organization hutAleko = new Organization()
                {
                    OrganizationId = 2,
                    Name = "Aleko Hut"
                };
                hutAleko.Users.Add(ivan);
                db.Organizations.Add(hutAleko);

                Test ivanTest = new Test()
                {
                    TestId = 1,
                    TestResults = "Compression test 123",
                    DangerLevel = 3,
                    PlaceId = 1,
                    UserId = 1,
                    Date = new DateTime(2015, 1, 12),
                    Slope = 27,

                };
                ivanTest.Organizations.Add(hutAleko);
                db.Tests.Add(ivanTest);

                Test petarTest = new Test()
                {
                    TestId = 2,
                    TestResults = "Compression test 5642",
                    DangerLevel = 2,
                    PlaceId = 3,
                    UserId = 2,
                    Date = new DateTime(2015, 1, 12),
                    Slope = 28
                };
                petarTest.Organizations.Add(rilaSport);
                db.Tests.Add(petarTest);

                db.SaveChanges();
            }
        }
    }
}