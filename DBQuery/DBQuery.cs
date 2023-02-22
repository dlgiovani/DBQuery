using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Npgsql;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Data;
using System.Net.Http.Headers;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

public class DBQuery
{
    public static string connectionType;
    public static string server;
    public static string instance;
    public static string database;
    public static string user;
    public static string password;
    public static string commandType;
    public static string addtionalConnectionString;

    public static string[] tablesToIndex = new string[] {"-"};

    public static int port;
    public static int timeout;

    private class IndexTables
    {
        public string[] tables = tablesToIndex;
    public string serialize()
    {
        string output = " ";
        for (int i = 0; i < tablesToIndex.Length; i++)
        {
                if (connectionType == "mysql")
                    output += "'" + tablesToIndex[i] + "'" + (i == tablesToIndex.Length - 1 ? " " : ", ");
                if (connectionType == "sqlserver")
                    output += tablesToIndex[i] + (i == tablesToIndex.Length - 1 ? " " : ", ");

            }
            return output;
    }

}

private static string connectionString;

private static DataTable getQueryConfiguration()
{
    IndexTables indexTables = new IndexTables();

    DataTable schemaTable = new DataTable();
    DataTable dataTable = new DataTable();


        switch (DBQuery.connectionType.ToLower())
    {
        case "sqlserver":
            connectionString = string.Format("Data Source={0};Initial Catalog={1};user id={2};Password={3}",
                (object)DBQuery.server, (object)DBQuery.database, (object)DBQuery.user, (object)DBQuery.password);

            SqlConnection sqlserverConnection = new SqlConnection(connectionString);
            SqlCommand sqlserverCommand = new SqlCommand("select name, " +
                "system_type_name " +
                "from sys.dm_exec_describe_first_result_set " +
                "(N'select * from " +
                indexTables.serialize() +
                "', N'', 0)", sqlserverConnection);

                SqlDataAdapter sqlserverAdapter = new SqlDataAdapter(sqlserverCommand);
                sqlserverAdapter.Fill(schemaTable);

            break;

        case "postgresql":
            connectionString = string.Format("Server={0};Port={1};Database={2};user id={3};Password={4};{5}",
                (object)DBQuery.server, (object)DBQuery.port, (object)DBQuery.database, (object)DBQuery.user, (object)DBQuery.password, (object)DBQuery.addtionalConnectionString);
            break;

        case "mysql":
            connectionString = string.Format("SERVER={0}; Port={1}; DATABASE={2}; UID={3}; PASSWORD={4};convert zero datetime=True;",
                (object)DBQuery.server, (object)DBQuery.port, (object)DBQuery.database, (object)DBQuery.user, (object)DBQuery.password);

                MySqlConnection mysqlConnection = new MySqlConnection(connectionString);
                MySqlCommand mysqlCommand = new MySqlCommand("SELECT COLUMN_NAME, " +
                    "DATA_TYPE " +
                    "from INFORMATION_SCHEMA.columns where TABLE_NAME IN (" +
                    indexTables.serialize() + ")", mysqlConnection);

                MySqlDataAdapter mysqlAdapter = new MySqlDataAdapter(mysqlCommand);
                mysqlAdapter.Fill(schemaTable);
                //return mysqlCommand.CommandText;

                break;

    }

        //string jsonSchema = JsonConvert.SerializeObject(schemaTable);
        return schemaTable;
        
}

    private string mapDbType(string dbType)
    {

        SqlDbType returnType = new SqlDbType();
        string sizeOfType = dbType.Split('(', ')')[1];
        if (sizeOfType.Contains("."))
        {
            int size = (int) Convert.ToInt64(sizeOfType);
        }

        //switch
        //    {
        //        case (dbType)
        //    }


        return null;
    }

    private int mapDbSize(string dbType)
    {

        string sizeOfType = dbType.Split('(', ')')[1];
        int size = (int)Convert.ToInt64(sizeOfType);

        return size;
    }

    public static string ExecuteQuery(string query, SqlParameter[] parameters)
    {
        DataTable dataTable = getQueryConfiguration();

        Console.WriteLine(dataTable.Columns[0]);
        Console.WriteLine(dataTable.Columns[1]);

        foreach (SqlParameter parameter in parameters)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    if (parameter.SqlValue == row[0])
                    {
                        //parameter.SqlDbType = mapDbType(row[1]);
                        //string parm = row[1];
                        //parameter.Size      = mapDbSize((string) row[1]);
                    }
                }
            }
        }

        return "a";
    }

}