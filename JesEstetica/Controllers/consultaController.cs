using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JesEstetica.Controllers
{
    public class consultaController : Controller
    {

        private repositorioServico repositorioServico;
        private repositorioConsulta repositorioConsulta;
        private repositorioUsuario repositorioUsuario;
        private repositorioStatus repositorioStatus;

        public consultaController()
        {
            repositorioServico = new repositorioServico();
            repositorioConsulta = new repositorioConsulta();
            repositorioUsuario = new repositorioUsuario();
            repositorioStatus = new repositorioStatus();
        }
        // GET: consulta
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult listaConsultas(consultar _obj)
        {
            try
            {

                var retorno = repositorioConsulta.Proc(new consultar() { statusID = Convert.ToInt32(_obj.statusID)  , usuarioResponsavelID = Convert.ToInt32(_obj.usuarioResponsavelID) ,dataInicio= _obj.dataInicio,dataFim =_obj.dataFim });
               
                return PartialView(retorno);

            }
            catch (Exception ex)
            {
                return Json(new { status = "Erro" });

            }
        }

        public ActionResult Busca()
        {
            var status = repositorioStatus.SelectDataReader("select * from status ");
            var usuario = repositorioUsuario.SelectDataReader("select * from usuarios");
            var retorno = new consultar(usuario, status);
            return View(retorno);
            
        }

        public ActionResult Cadastrar()
        {
            var listadeservicos = repositorioServico.SelectDataReader("select id , concat(nome, ' ( R$ ', concat( valor ,' )') ) as nome ,status ,valor from servicos where status = 1  ");
            var usuario = repositorioUsuario.SelectDataReader("select * from usuarios");
            consultar _consulta = new consultar(listadeservicos,usuario) ;
            return View(_consulta);
        }

        public ActionResult Salvar(consultar _obj)
        {
            try
            {

                repositorioConsulta.Insert(_obj);

                return Json(new { status = "Sucesso" });
            }
            catch (Exception ex)
            {
                return Json(new { status = "Erro" });

            }
        }

        public ActionResult AtualizarConsulta()
        {
            var status = repositorioStatus.SelectDataReader("select * from status ");
            var usuario = repositorioUsuario.SelectDataReader("select * from usuarios");
            var retorno = new consultar(usuario, status);
            return PartialView(retorno);
        }
        public ActionResult atualizarDados(consultar obj)
        {
            try
            {
                repositorioConsulta.update(obj);

                return Json(new { status = "Sucesso" });
            }
            catch (Exception ex)
            {
                return Json(new { status = "Erro" });

            }
        }
        
    }
}