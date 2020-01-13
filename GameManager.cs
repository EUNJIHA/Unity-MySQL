using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 이거 추가함.
using MySql.Data.MySqlClient;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("LoginPanel")]
    public InputField IDInputField;
    public InputField PWInputField;

    [Header("CreateAccountPanel")]
    public InputField New_IDInputField;
    public InputField New_PWInputField;
    public GameObject CreateAccountPanelObj;

    // public string LoginUrl;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void insertQuery()
    {
        //MySQLConnector conn = GameObject.Find("MyObject").GetComponent<MySQLConnector>();

        string id = New_IDInputField.text;
        string pw = New_PWInputField.text;

        MySqlCommand comm = MySQLConnector.dbConnection.CreateCommand();
        comm.CommandText = "INSERT INTO login VALUES(@id, @pw)";
        comm.Parameters.AddWithValue("@id", id);
        comm.Parameters.AddWithValue("@pw", pw);
        comm.ExecuteNonQuery();
    }

    public void LoginBtn()
    {
        StartCoroutine(LoginGo());
    }

    IEnumerator LoginGo()
    {
        chkQuery();
        yield return null;
    }

    public void chkQuery()
    {

        string id = IDInputField.text;
        string pw = PWInputField.text;

        string chkString = "select loginPw from login where loginId='" + id + "';";

        MySqlCommand comm = MySQLConnector.dbConnection.CreateCommand();
        comm.CommandText = chkString;
        MySqlDataReader reader = comm.ExecuteReader();

        while (reader.Read())
        {
            string loginPw = reader["loginPw"].ToString();
            if (loginPw == pw)
            {
                SceneManager.LoadScene("Main");
            }

        }

    }

    public void OpenCreateAccountBtn()
    {
        CreateAccountPanelObj.SetActive(true);

    }

    public void CreateAccountBtn()
    {
        StartCoroutine(CreateGo());
    }

    IEnumerator CreateGo()
    {
        insertQuery();

        Debug.Log(IDInputField.text);
        Debug.Log(PWInputField.text);
        yield return null;
    }


}
