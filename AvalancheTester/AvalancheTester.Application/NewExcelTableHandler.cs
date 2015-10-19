using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace AvalancheTester.Application
{
    public class NewExcelTableHandler
    {
        private const string UnzipedReportsDirectory = "../../Avalanche-Tests-Reports";

        private const string ZipFileName = "../../Avalanche-Tests-Reports.zip";

        private const string ExcelQuery = "SELECT * FROM [Sheet1$]";

        public NewExcelTableHandler()
            :this(".", "AvalancheTestsDB", "Tests")
        {
        }

        public NewExcelTableHandler(string serverName, string databaseName, string tableName)
        {
            this.ServerName = serverName;
            this.DatabaseName = databaseName;
            this.TableName = tableName;
        }

        public string ServerName { get; set; }

        public string DatabaseName { get; set; }

        public string TableName { get; set; }

        public void InputDataToDatabase()
        {
            var handler = new NewExcelTableHandler();

            Console.WriteLine("Extracting reports from zip file...");

            if (Directory.Exists(UnzipedReportsDirectory))
            {
                Directory.Delete(UnzipedReportsDirectory, true);
            }

            ZipFile.ExtractToDirectory(ZipFileName, UnzipedReportsDirectory);
            Console.WriteLine("Extracting completed!");

            string reportsFullPath = Path.GetFullPath(UnzipedReportsDirectory);
            foreach (var dir in Directory.GetDirectories(reportsFullPath))
            {
                Console.WriteLine(dir);
                handler.GetReportsFromDirectory(dir);
                Console.WriteLine("Reports adding in SQL Server database...");
            }

            Directory.Delete(reportsFullPath, true);

            Console.WriteLine("Adding Reports from zip file completed!");
        }

        private void GetReportsFromDirectory(string dir)
        {
            foreach (var file in Directory.GetFiles(dir))
            {
                Console.WriteLine("I am in");
                this.ImportDataFromExcel(file);
            }
        }

        private void ImportDataFromExcel(string excelFilePath)
        {
            string excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + excelFilePath
                                           + ";Extended Properties=\"Excel 8.0;HDR=YES;\"";
            string sqlConnectionString = "Server=" + this.ServerName + "; Database=" + this.DatabaseName
                                         + "; Integrated Security=true";

            using (var oleDbConnection = new OleDbConnection(excelConnectionString))
            using (var sqlBulkCopy = new SqlBulkCopy(sqlConnectionString, SqlBulkCopyOptions.Default))
            {
                oleDbConnection.Open();

                var oleDbCommand = new OleDbCommand(ExcelQuery, oleDbConnection);
                var oleDbDataReader = oleDbCommand.ExecuteReader();

                var table = new DataTable();
                if (oleDbDataReader != null)
                {
                    table.Load(oleDbDataReader);
                }

                //string directoryName = Path.GetDirectoryName(excelFilePath);
                //string alignmentPerkIdString = Regex.Match(directoryName, @"\d+$").Value;

                //table.Columns.Add("AlignmentPerkId", typeof(int)); 
                /*foreach (DataRow dataRow in table.Rows)
                {
                    dataRow["AlignmentPerkId"] = alignmentPerkIdString;
                }*/

                sqlBulkCopy.DestinationTableName = this.TableName;
                foreach (DataColumn col in table.Columns)
                {
                    sqlBulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                    Console.WriteLine(col.ColumnName);
                }

                try
                {
                    sqlBulkCopy.WriteToServer(table);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}