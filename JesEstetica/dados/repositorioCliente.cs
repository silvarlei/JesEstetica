using System.Data;
using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

public class BaseDados
{

    private string connString;
    private MySqlConnection connection;
    private MySqlCommand command;
    private DataSet mDataSet;
    private MySqlDataAdapter mAdapter;

    public BaseDados()
    {
        mDataSet = new DataSet();
        connString = "Server=localhost;Database=jesEstetica;Uid=root;Pwd=1234";
        connection = new MySqlConnection(connString);
        command = connection.CreateCommand();
        // mAdapter = new MySqlDataAdapter();

    }
    public void con()
    {


        try
        {

            connection.Open();
            command.CommandText = "INSERT INTO TABELA1 (CAMPO1) VALUES ('VALOR1')";
            command.ExecuteNonQuery();
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
    }

    public void InsertCliente(cliente _obj)
    {


        try
        {
            var _parametros = String.Format("INSERT INTO clientes (nome , cpf , rg , telefone1 , telefone2 , email) VALUES('{0}' ,'{1}' ,'{2}' ,'{3}' ,'{4}' ,'{5}')", _obj.nome, _obj.CPF, _obj.RG, _obj.telefone1, _obj.telefone2, _obj.email);
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

    public void Update(cliente _obj)
    {


        try
        {
            var _parametros = String.Format("update clientes  set nome ='{0}' , cpf='{1}' , rg='{2}' , telefone1='{3}' , telefone2='{4}' , email ='{5}'  where idClientes = {6}", _obj.nome, _obj.CPF, _obj.RG, _obj.telefone1, _obj.telefone2, _obj.email, _obj.idClientes);
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
    public DataSet Select(string comando)
    {
        try
        {
            //abre a conexao
            connection.Open();

            if (connection.State == ConnectionState.Open)
            {
                //cria um adapter usando a instrução SQL para acessar a tabela Clientes
                mAdapter = new MySqlDataAdapter("SELECT * FROM Clientes", connection);
                //preenche o dataset via adapter
                mAdapter.Fill(mDataSet);

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
        return mDataSet;
        //verificva se a conexão esta aberta
    }


    public List<cliente> SelectDataReader(string comando)
    {
        var cliente = new List<cliente>();
        try
        {
            connection.Open();
            command.CommandText = comando;
            var rd = command.ExecuteReader();




            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    cliente.Add(new cliente()
                    {
                        idClientes = (long)rd["idClientes"],
                        nome = rd["nome"].ToString(),
                        CPF = rd["CPF"].ToString(),
                        RG = rd["RG"].ToString(),
                        telefone1 = rd["telefone1"].ToString(),
                        telefone2 = rd["telefone2"].ToString(),
                        email = rd["email"].ToString()
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
        return cliente;
        //verificva se a conexão esta aberta

    }

    /////servico 

    public List<consultar> Proc(cliente _obj)
    {

        var entidade = new List<consultar>();
        try
        {
            var _parametros = String.Format("call usp_sel_historicoCliente('{0}')", _obj.idClientes);
            connection.Open();
            command.CommandText = _parametros;
            var rd = command.ExecuteReader();

            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    var cliente =
                        new cliente()
                        {
                            nome = rd["nome"].ToString(),
                            CPF = rd["CPF"].ToString(),
                            telefone1 = rd["telefone1"].ToString(),
                            telefone2 = rd["telefone2"].ToString()
                        };

                    var usuario =
                       new usuario()
                       {
                           nome = rd["responsavel"].ToString()
                         
                       };

                    var servico =
                        new servico()
                        {
                            nome = rd["nomeServico"].ToString()
                        };

                    var status =
                      new status()
                      {
                          nome = rd["nomeStatus"].ToString() ,
                          finaliza =Convert.ToBoolean( rd["finaliza"])
                      };

                    DateTime? datafim = null;
                    if (rd["datafinalizaconsulta"] != null && rd["datafinalizaconsulta"].ToString() != "")
                        datafim = (DateTime)rd["datafinalizaconsulta"];

                    entidade.Add(new consultar()
                    {
                        id = (long)rd["id"],
                        data = (DateTime)rd["data"],
                        dataConsulta = (DateTime)rd["dataConsulta"],
                        dataFinalizaConsulta = datafim,
                        cliente = cliente,
                        servico = servico ,
                        status = status ,
                        usuario = usuario ,
                        obs = rd["obs"].ToString()
                    });
                }
            }
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
        return entidade;
    }


}
