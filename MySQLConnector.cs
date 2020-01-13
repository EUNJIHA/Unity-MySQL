using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
public class MySQLConnector : MonoBehaviour
{
    // Global variables
    private static MySqlConnection dbConnection;
    private static MySQLConnector instance = null;
    private static string mystring = "select * from login;";

    public string id;
    public string pw;
    /*    private static string insertString = "insert into login values(" + id + "," + pw + ");";*/
    private static string insertString = "insert into login values(@id, @pw);";
    public static MySQLConnector Instance
    {
        get
        {
            if (instance == null)
            {
                lock (typeof(MySQLConnector))
                {
                    if (instance == null)
                    {
                        instance = new MySQLConnector();
                    }
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        openSqlConnection();
    }
    /*    public MySQLConnector()
        {
            openSqlConnection();
            doQuery(mystring);
        }
    */

    // Connect to database
    private static void openSqlConnection()
    {

        string connectionString = "Server=127.0.0.1;" +

            "Database=myschema;" +

            "User ID=root;" +

            "Password=5072;" +

            "Pooling=false";

        dbConnection = new MySqlConnection(connectionString);

        dbConnection.Open();

        Debug.Log("Connected to database.");

    }

    // MySQL Query
    public MySqlDataReader doQuery(string sqlQuery)
    {

        MySqlCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = sqlQuery;
        MySqlDataReader reader = dbCommand.ExecuteReader();

        while (reader.Read())
        {
            string loginID = reader["loginId"].ToString();
            Debug.Log(loginID);
        }


        dbCommand.Dispose();
        dbCommand = null;

        return reader;
    }


    private void OnDestroy()
    {
        closeSqlConnection();
    }
    // Disconnect from database
    private static void closeSqlConnection()
    {
        dbConnection.Close();

        dbConnection = null;
        Debug.Log("Disconnected from database.");
    }

    public void LoginBtn()
    {
        StartCoroutine(LoginGo());
    }

    IEnumerator LoginGo()
    {



        Debug.Log(IDInputField.text);
        Debug.Log(PWInputField.text);
        yield return null;

    }
}

