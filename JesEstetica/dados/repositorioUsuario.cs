using System.Data;
using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

public class repositorioUsuario
{
    private string connString;
    private MySqlConnection connection;
    private MySqlCommand command;
    private DataSet mDataSet;
    private MySqlDataAdapter mAdapter;

    public repositorioUsuario()
    {
        mDataSet = new DataSet();
        connString = "Server=localhost;Database=jesEstetica;Uid=root;Pwd=1234";
        connection = new MySqlConnection(connString);
        command = connection.CreateCommand();
        // mAdapter = new MySqlDataAdapter();
    }

    public void Update(usuario _obj)
    {


        try
        {
            var _parametros = String.Format("update usuarios  set nome ='{0}' , status={1} , CargoID={2}   where id = {3}", _obj.nome, _obj.status, _obj.cargoID, _obj.id);
            connection.Open();
            command.CommandText = _parametros;
            command.ExecuteNonQuery();
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
    }


    public List<usuario> SelectDataReader(string comando)
    {
        var _obj = new List<usuario>();
        try
        {
            connection.Open();
            command.CommandText = comando;
            var rd = command.ExecuteReader();




            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    _obj.Add(new usuario()
                    {
                        id = Convert.ToInt32(rd["id"]),
                        nome = rd["nome"].ToString(),
                        status = Convert.ToBoolean(rd["status"])
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
        return _obj;
        //verificva se a conexão esta aberta

    }

    public void Insert(usuario _obj)
    {


        try
        {
            var _parametros = String.Format("INSERT INTO usuarios (nome ,status , data , cargoID) VALUES('{0}' ,{1} ,'{2}',{3})", _obj.nome, _obj.status == true ? 1 : 0, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),_obj.cargoID);
            connection.Open();
            command.CommandText = _parametros;
            command.ExecuteNonQuery();
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
    }

    
}


