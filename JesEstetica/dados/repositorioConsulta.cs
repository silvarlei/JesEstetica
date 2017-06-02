using System.Data;
using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;



public class repositorioConsulta
{
    private string connString;
    private MySqlConnection connection;
    private MySqlCommand command;
    private DataSet mDataSet;
    private MySqlDataAdapter mAdapter;

    public repositorioConsulta()
    {
        mDataSet = new DataSet();
        connString = "Server=localhost;Database=jesEstetica;Uid=root;Pwd=1234";
        connection = new MySqlConnection(connString);
        command = connection.CreateCommand();
        // mAdapter = new MySqlDataAdapter();
    }

    public List<consultar> Proc(consultar _obj)
    {

        var entidade = new List<consultar>();
        try
        {
            var _parametros = String.Format("call usp_sel_ListaConsultas({0} ,{1} ,{2} ,{3})", _obj.statusID, _obj.usuarioResponsavelID, _obj.dataInicio==null ?"null": "'"+Convert.ToDateTime( _obj.dataInicio).ToString("yyyy-MM-dd hh:mm:ss")+"'", _obj.dataFim == null ? "null" :"'"+ Convert.ToDateTime(_obj.dataFim).ToString("yyyy-MM-dd hh:mm:ss"+"'"));
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
                          nome = rd["nomeStatus"].ToString(),
                          finaliza = Convert.ToBoolean(rd["finaliza"])
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
                        servico = servico,
                        status = status,
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



    public void Insert(consultar _obj)
    {


        try
        {
            var _parametros = String.Format("INSERT INTO consultas (clienteID, data , servicoID ,dataConsulta , statusID , sessao , usuarioResponsavelID , obs ) VALUES('{0}' ,'{1}' ,'{2}','{3}' ,{4} , {5},  {6} , '{7}')", _obj.clienteID, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), _obj.servicoID ,_obj.dataConsulta.ToString("yyyy-MM-dd hh:mm:ss") , _obj.statusID , _obj.sessao , _obj.usuarioResponsavelID , _obj.obs);
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

    public void update(consultar _obj)
    {


        try
        {
            var _parametros = String.Format("update consultas set statusID= {0} , usuarioResponsavelID= {1},dataFinalizaConsulta='{2}' where id ={3}", _obj.statusID ,_obj.usuarioResponsavelID, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), _obj.id);
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