using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;

namespace Task9MySQL
{
    /// <summary>
    /// Download and install MySQL database, MySQL Connector/Net (.NET Data Provider for MySQL)
    ///  + MySQL Workbench GUI administration tool . Create a MySQL database to store Books (title, author, publish date and ISBN).
    ///  Write methods for listing all books, finding a book by name and adding a book.
    /// </summary>
 

    public class EntryPoint
    {
        public static void Main()
        {
            Console.Write("Enter pass: ");
            string pass = Console.ReadLine();

            string connectionStr = "Server=localhost;Database=books;Uid=root;Pwd=" + pass + ";";
            MySqlConnection connection = new MySqlConnection(connectionStr);

            while (true)
            {
                Console.WriteLine("Enter your choice:");
                Console.WriteLine("1.List all books.");
                Console.WriteLine("2.FInd book by title.");
                Console.WriteLine("3.Add new book.");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ListAllBooks(connection);
                        break;
                    case "2":
                        FindBookByName(connection);
                        break;
                    case "3":
                        AddNewBook(connection);
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }

        private static void AddNewBook(MySqlConnection connection)
        {
            Console.Write("\nEnter book title:");
            var title = Console.ReadLine();

            Console.Write("\nEnter book Author:");
            var author = Console.ReadLine();

            Console.Write("\nEnter book ISBN:");
            var isbn = Console.ReadLine();

            connection.Open();
            using (connection)
            {
                MySqlCommand command =
                    new MySqlCommand("INSERT INTO book (Title, Author, Isbn) VALUES(@title, @author, @isbn)", connection);
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@author", author);
                command.Parameters.AddWithValue("@isbn", isbn);

                command.ExecuteNonQuery();
                Console.WriteLine("\nThe book was added successfully");
            }

            connection.Close();

            Console.WriteLine("\nPress any key to continiue..");
            Console.ReadLine();
            Console.Clear();
        }

        private static void FindBookByName(MySqlConnection connection)
        {
            Console.Write("Enter patter for search:");
            var inputPattern = Console.ReadLine();
            inputPattern = ValidatePattern(inputPattern);

            connection.Open();
            using (connection)
            {
                MySqlCommand command = new MySqlCommand("SELECT * FROM book WHERE TITLE LIKE @pattern", connection);
                command.Parameters.AddWithValue("@pattern", inputPattern);
                MySqlDataReader reader = command.ExecuteReader();

                Console.WriteLine("Books: Title, Author, PublishDate, ISBN:\n");

                while (reader.Read())
                {
                    Console.WriteLine("{0}, {1}, {2}, {3}", reader["Title"], reader["Author"], reader["PublishDate"],
                        reader["Isbn"]);
                }
            }

            connection.Close();

            Console.WriteLine("\nPress any key to continiue..");
            Console.ReadLine();
            Console.Clear();
        }

        private static void ListAllBooks(MySqlConnection connection)
        {
            connection.Open();
            using (connection)
            {
                MySqlCommand command = new MySqlCommand("SELECT * FROM book", connection);
                MySqlDataReader reader = command.ExecuteReader();

                Console.WriteLine("Books: Title, Author, PublishDate, ISBN:\n");

                while (reader.Read())
                {
                    Console.WriteLine("{0}, {1}, {2}, {3}", reader["Title"], reader["Author"], reader["PublishDate"],
                        reader["Isbn"]);
                }
            }

            connection.Close();

            Console.WriteLine("\nPress any key to continiue..");
            Console.ReadLine();
            Console.Clear();
        }

        private static string ValidatePattern(string pattern)
        {
            var result = pattern;

            var evilHackersCharacters = new string[] {"'", "\"", "%", "\\", "_", "/"};

            foreach (var currentChar in evilHackersCharacters)
            {
                result = result.Replace(currentChar, string.Empty);
            }

            result = "%" + result + "%";
            return result;
        }
    }
}
