using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;

namespace Task1ToTask5Northwind
{
    public class TaskExecutor
    {
        private const string ConnectionString = "Server=.; Database=Northwind; Integrated Security=true";
        private const string ImageFileName = @"..\..\Images\Image-{0}.bmp";
        

        static void Main()
        {
            SqlConnection dbConnection = new SqlConnection(ConnectionString);

            dbConnection.Open();

            using (dbConnection)
            {
                Console.WriteLine("Task 1");
                ExecuteTask1(dbConnection);
                Console.WriteLine();

                Console.WriteLine("Task 2");
                ExecuteTask2(dbConnection);
                Console.WriteLine();

                Console.WriteLine("Task 3");
                ExecuteTask3(dbConnection);
                Console.WriteLine();

                Console.WriteLine("Task 4");
                ExecuteTask4(dbConnection, "New Product", false);
                Console.WriteLine();

                Console.WriteLine("Task 5");
                ExecuteTask5(dbConnection);
                Console.WriteLine();

                Console.WriteLine("Task 8");
                Console.WriteLine("Please, insert some string to search for products int database");
                string inputString = Console.ReadLine();
                ExecuteTask8(dbConnection, inputString);
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Write a program that retrieves from the Northwind sample database in MS SQL Server the number
        /// of  rows in the Categories table.
        /// </summary>
        private static void ExecuteTask1(SqlConnection dbConnection)
        {
            string commandText = "SELECT COUNT(*) FROM Categories";

            SqlCommand query = new SqlCommand(commandText, dbConnection);

            int numberOfRows = (int) query.ExecuteScalar();

            Console.WriteLine("Northwind database rows in the Categories table are: {0}", numberOfRows);
        }

        /// <summary>
        /// Write a program that retrieves the name and description of all categories in the Northwind DB.
        /// </summary>
        private static void ExecuteTask2(SqlConnection dbConnection)
        {
            string commandText = "SELECT CategoryName, Description FROM Categories";

            SqlCommand query = new SqlCommand(commandText, dbConnection);

            SqlDataReader reader = query.ExecuteReader();

            using (reader)
            {
                while (reader.Read())
                {
                    Console.WriteLine("Category: {0} - Entities: {1}", reader["CategoryName"], reader["Description"]);
                }
            }
        }

        /// <summary>
        /// Write a program that retrieves from the Northwind database all product categories and the names of the products
        ///  in each category.
        ///  Can you do this with a single SQL query (with table join)?
        /// Answer: Yes, I can do it!
        /// </summary>
        private static void ExecuteTask3(SqlConnection dbConnection)
        {
            string commandText = @"SELECT c.CategoryName, p.ProductName
                                    FROM Categories c
                                    JOIN Products p
                                    ON c.CategoryID = p.CategoryID";

            SqlCommand query = new SqlCommand(commandText, dbConnection);

            SqlDataReader reader = query.ExecuteReader();

            using (reader)
            {
                using (reader)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("Category name: {0} - Product name: {1}", reader["CategoryName"], reader["ProductName"]);
                    }
                }
            }
        }

        /// <summary>
        /// Write a method that adds a new product in the products table in the Northwind database.
        /// Use a parameterized SQL command.
        /// </summary>
        private static void ExecuteTask4(SqlConnection dbConnection, string productName, bool discontinued,
                                    int? supplierID = null, int? categoryID = null,
                                    string quantityPerUnit = null, decimal? unitPrice = null,
                                    int? unitsInStock = null, int? unitsInOrder = null, int? reorderLevel = null)
        {
            string commandText = @"INSERT INTO Products
                                            VALUES(@productName, @supplierID, @categoryID, @quantityPerUnit,
                                                    @unitPrice, @unitsInStock, @unitsInOrder, @reorderLevel, @discontinued)";

            SqlCommand query = new SqlCommand(commandText, dbConnection);

            query.Parameters.AddWithValue("@productName", productName);

            SqlParameter suppliedIDParam = new SqlParameter("@supplierID", supplierID);
            if (supplierID == null)
            {
                suppliedIDParam.Value = DBNull.Value;
            }
            query.Parameters.Add(suppliedIDParam);

            SqlParameter categoryIDParam = new SqlParameter("@categoryID", categoryID);
            if (categoryID == null)
            {
                categoryIDParam.Value = DBNull.Value;
            }
            query.Parameters.Add(categoryIDParam);

            SqlParameter quantityPerUnitParam = new SqlParameter("@quantityPerUnit", quantityPerUnit);
            if (quantityPerUnit == null)
            {
                quantityPerUnitParam.Value = DBNull.Value;
            }
            query.Parameters.Add(quantityPerUnitParam);


            SqlParameter unitPriceParameter = new SqlParameter("@unitPrice", unitPrice);
            if (unitPrice == null)
            {
                unitPriceParameter.Value = DBNull.Value;
            }
            query.Parameters.Add(unitPriceParameter);

            var unitsInStockParam = new SqlParameter("@unitsInStock", unitsInStock);
            if (unitsInStock == null)
            {
                unitsInStockParam.Value = DBNull.Value;
            }
            query.Parameters.Add(unitsInStockParam);

            var unitsInOrderParam = new SqlParameter("@unitsInOrder", unitsInOrder);
            if (unitsInOrder == null)
            {
                unitsInOrderParam.Value = DBNull.Value;
            }
            query.Parameters.Add(unitsInOrderParam);

            var reorderLevelParam = new SqlParameter("@reorderLevel", reorderLevel);
            if (reorderLevel == null)
            {
                reorderLevelParam.Value = DBNull.Value;
            }
            query.Parameters.Add(reorderLevelParam);

            query.Parameters.AddWithValue("@discontinued", discontinued);

            query.ExecuteNonQuery();

            Console.WriteLine("Product added: " + productName);
        }

        /// <summary>
        /// Write a program that retrieves the images for all categories in the Northwind database and stores them as
        /// JPG files in the file system.
        /// </summary>
        /// <param name="dbConnection"></param>
        private static void ExecuteTask5(SqlConnection dbConnection)
        {
            string commandText = "SELECT CategoryID, Picture FROM Categories";
            SqlCommand query = new SqlCommand(commandText, dbConnection);

            var reader = query.ExecuteReader();

            using (reader)
            {
                while (reader.Read())
                {
                    Byte[] imageAsByteArray = (byte[])reader["Picture"];

                    var memoryStream = new MemoryStream(imageAsByteArray, 78, imageAsByteArray.Length - 78);

                    using (memoryStream)
                    {
                        var image = Image.FromStream(memoryStream);

                        using (image)
                        {

                            image.Save(string.Format(ImageFileName, (int)reader["CategoryID"]));
                        }
                    }
                }
            }

            Console.WriteLine("Please, look in the Images folder, to check for the added images");
            Console.WriteLine();
        }

        /// <summary>
        /// Write a program that reads a string from the console and finds all products that contain this string.
        ///  Ensure you handle correctly characters like ', %, ", \ and _.
        /// </summary>
        private static void ExecuteTask8(SqlConnection dbConnection, string inputString)
        {
            Console.WriteLine("All products containing " + inputString + " :");

            var sqlCommand = new SqlCommand(@"SELECT ProductName FROM Products
                                              WHERE CHARINDEX(@pattern, ProductName) > 0", dbConnection);

            sqlCommand.Parameters.AddWithValue("@pattern", inputString);

            using (var resultsReader = sqlCommand.ExecuteReader())
            {
                while (resultsReader.Read())
                {
                    Console.WriteLine((string)resultsReader["ProductName"]);
                }
            }
        }
    }
}
