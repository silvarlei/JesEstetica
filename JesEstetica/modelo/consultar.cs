using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public class consultar
{
    public long id { get; set; }
    public long clienteID { get; set; }
    public DateTime data { get; set; }
    public int servicoID { get; set; }
    public int usuarioResponsavelID { get; set; }
    public DateTime dataConsulta { get; set; }
    public DateTime? dataFinalizaConsulta { get; set; }
    public string obs { get; set; }
    public int statusID { get; set; }
    public SelectList servicos { get; set; }
    public cliente cliente { get; set; }
    public servico servico { get; set; }
    public status status { get; set; }
    public usuario usuario { get; set; }
    public SelectList usuariosLista { get; set; }
    public SelectList statusLista { get; set; }
    public int sessao { get; set; }
    public DateTime? dataInicio { get; set; }
    public DateTime? dataFim { get; set; }

    public consultar()
    {

    }
    public consultar(List<servico> _servico)
    {
        servicos = new SelectList(_servico, "id", "nome");
    }

    public consultar(List<usuario> _usuario, List<status> _status)
    {
        usuariosLista = new SelectList(_usuario, "id", "nome");
        statusLista = new SelectList(_status, "id", "nome");
    }

    public consultar(List<servico> _servico , List<usuario> _usuario)
    {

        servicos = new SelectList(_servico, "id", "nome");
        usuariosLista = new SelectList(_usuario, "id", "nome");
    }
}
