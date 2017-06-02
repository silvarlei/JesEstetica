using System.Data;
using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

public class repositorioStatus
{

    private string connString;
    private MySqlConnection connection;
    private MySqlCommand command;
    private DataSet mDataSet;
    private MySqlDataAdapter mAdapter;

    public repositorioStatus()
    {
        mDataSet = new DataSet();
        connString = "Server=localhost;Database=jesEstetica;Uid=root;Pwd=1234";
        connection = new MySqlConnection(connString);
        command = connection.CreateCommand();
        // mAdapter = new MySqlDataAdapter();

    }

    public List<status> SelectDataReader(string comando)
    {
        var obj = new List<status>();
        try
        {
            connection.Open();
            command.CommandText = comando;
            var rd = command.ExecuteReader();




            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    obj.Add(new status()
                    {
                        id = Convert.ToInt32( rd["id"]),
                        nome = rd["nome"].ToString(),
                        ativo = Convert.ToBoolean( rd["status"] ),
                        finaliza = Convert.ToBoolean(rd["finaliza"])
                    });
                }
            }
        }
        catch (System.Exception e)
        {
            mDataSet = null;
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();

        }
        return obj;
        //verificva se a conexão esta aberta

    }
}
