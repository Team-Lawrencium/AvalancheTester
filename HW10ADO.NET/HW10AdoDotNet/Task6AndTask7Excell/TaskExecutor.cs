using System;
using System.Data;
using System.Data.OleDb;

namespace Task6AndTask7Excell
{
    // NOTE: To execute the task you may be need of MS Access Database Engine 2010(for MS Office 2010)
    // or other version of MS Office 
    public class TaskExecutor
    {
        private const string Filepath = "../../Table.xlsx";
        private const string ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="
                                                + Filepath + ";Extended Properties='Excel 12.0 xml;HDR=Yes';";
        public static void Main()
        {
            OleDbConnection excelConnection = new OleDbConnection(ConnectionString);

            using (excelConnection)
            {
                excelConnection.Open();

                DataTable tableSchema = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                string sheetName = tableSchema.Rows[0]["TABLE_NAME"].ToString();

                ReadFromExcel(sheetName, excelConnection);

                WriteInExcel(sheetName, excelConnection, "Robinson Crusoe", "32");

                ReadFromExcel(sheetName, excelConnection);
            }
        }

        private static void ReadFromExcel(string sheetName, OleDbConnection excelConnection)
        {
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
                        Console.WriteLine("{0} has a score of {1}", reader["Name"], reader["Score"]);
                    }
                }
            }
        }

        private static void WriteInExcel(string sheetName, OleDbConnection excelConnection, string name, string score)
        {
            OleDbCommand excelDbCommand = new OleDbCommand("INSERT INTO [" + sheetName + "] VALUES (@name, @score)", excelConnection);

            excelDbCommand.Parameters.AddWithValue("@name", name);
            excelDbCommand.Parameters.AddWithValue("@score", score);

            excelDbCommand.ExecuteNonQuery();

            Console.WriteLine("Added new row ===> {0} has a score of {1}", name, score);
        }
    }
}