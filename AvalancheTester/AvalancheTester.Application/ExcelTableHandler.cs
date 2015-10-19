using System;
using System.Data;
using System.Data.OleDb;

namespace AvalancheTester.Application
{
    public class ExcelTableHandler
    {
        private const string InputFilepath = "../../DailyTestReports.xlsx";

        private const string InputFileConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="
                                                + InputFilepath + ";Extended Properties='Excel 12.0 xml;HDR=Yes';";

        private const string OutputFilepath = "../../DailyTestReportsOutput.xlsx";

        private const string OutputFileConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="
                                                + OutputFilepath + ";Extended Properties='Excel 12.0 xml;HDR=Yes';";

        public static void ReadFromExcel()
        {
            OleDbConnection excelConnection = new OleDbConnection(InputFileConnectionString);

            using (excelConnection)
            {
                excelConnection.Open();

                DataTable tableSchema = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                string sheetName = tableSchema.Rows[0]["TABLE_NAME"].ToString();

                OleDbCommand command = new OleDbCommand("SELECT * FROM [" + sheetName + "]", excelConnection);

                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(command);

                using (dataAdapter)
                {
                    DataSet dataSet = new DataSet();

                    dataAdapter.Fill(dataSet);

                    using (var reader = dataSet.CreateDataReader())
                    {
                        while (reader.Read())
                        {
                            //Console.WriteLine("{0} has a score of {1}", reader["Name"], reader["Score"]);
                            Console.WriteLine(
                                string.Format("TesterName: {0}, PlaceName: {1}, PlaceArea: {2}, Slope: {3}, Date: {4}, TestResult: {5}",
                                reader["TesterName"], reader["PlaceName"], reader["PlaceArea"], reader["Slope"], reader["Date"], reader["TestResult"]));
                        }
                    }
                }
            }
        }

        public static void WriteInExcel(string testerName, string placeName, string placeArea, float slope, string date, string testResult)
        {
            OleDbConnection excelConnection = new OleDbConnection(OutputFileConnectionString);

            using (excelConnection)
            {
                excelConnection.Open();

                DataTable tableSchema = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                string sheetName = tableSchema.Rows[0]["TABLE_NAME"].ToString();

                OleDbCommand excelDbCommand = new OleDbCommand(
                    "INSERT INTO [" + sheetName + "] VALUES (@TesterName, @PlaceName, @PlaceArea, @Slope, @Date, @TestResult)", excelConnection);

                excelDbCommand.Parameters.AddWithValue("@TesterName", testerName);
                excelDbCommand.Parameters.AddWithValue("@PlaceName", placeName);
                excelDbCommand.Parameters.AddWithValue("@PlaceArea", placeArea);
                excelDbCommand.Parameters.AddWithValue("@Slope", slope);
                excelDbCommand.Parameters.AddWithValue("@Date", date);
                excelDbCommand.Parameters.AddWithValue("@testResult", testResult);

                excelDbCommand.ExecuteNonQuery();

                Console.WriteLine("Added new row!");
            }
            
        }
    }
}