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
                    Name = "Витоша"
                };
                db.Places.Add(vitosha);

                Place pirin = new Place()
                {
                    PlaceId = 2,
                    Name = "Пирин"
                };
                db.Places.Add(pirin);

                Place rila = new Place()
                {
                    PlaceId = 3,
                    Name = "Рила"
                };
                db.Places.Add(rila);

                User ivan = new User()
                {
                    UserId = 1,
                    Name = "Бай Иван Хижаря"
                };
                db.Users.Add(ivan);

                User petar = new User()
                {
                    UserId = 2,
                    Name = "Петър Попангелов",
                };
                db.Users.Add(petar);

                Organization rilaSport = new Organization()
                {
                    OrganizationId = 1,
                    Name = "Рила Спорт"
                };
                rilaSport.Users.Add(petar);
                db.Organizations.Add(rilaSport);

                Organization hutAleko = new Organization()
                {
                    OrganizationId = 2,
                    Name = "Хижа Алеко"
                };
                hutAleko.Users.Add(ivan);
                db.Organizations.Add(hutAleko);

                Test ivanTest = new Test()
                {
                    TestId = 1,
                    TestResults = "Компресионен тест 3 повторения: Слой 163-120 см. – СТ1, СТ3...",
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
                    TestResults = "Компресионен тест 2 повторения: Слой 154-120 см. – СТ1, СТ3...",
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