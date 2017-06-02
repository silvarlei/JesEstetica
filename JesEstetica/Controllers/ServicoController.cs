using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JesEstetica.Controllers
{
    public class ServicoController : Controller
    {
        private repositorioServico repositorio;

        public ServicoController()
        {
            repositorio = new repositorioServico();
        }
        // GET: Servico
        public ActionResult Index()
        {
            return View();
        }

        // GET: Servico
        public ActionResult Cadastrar()
        {
            return View();
        }

        // GET: Servico

        public ActionResult Alterar(servico _obj)
        {
            try
            {

                repositorio.Update(_obj);

                return Json(new { status = "Sucesso" });
            }
            catch (Exception ex)
            {
                return Json(new { status = "Erro" });

            }
        }
        public ActionResult Listar()
        {
            try
            {

                var parametro = String.Format("select * from  servicos ");
                var retorno = repositorio.SelectDataReader(parametro);
                return PartialView(retorno);

            }
            catch (Exception ex)
            {

                return PartialView();

            }

        }

        public ActionResult Editar(long? id)
        {
            var parametro = String.Format("select * from  servicos where  id ={0} ", id);
            var retorno = repositorio.SelectDataReader(parametro);
            return View(retorno.FirstOrDefault());
        }

        public ActionResult Busca()
        {

            return View();
        }

        public ActionResult Salvar(servico _obj)
        {
        
            try
            {

                repositorio.InsertServico(_obj);

                return Json(new { status = "Sucesso" });
            }
            catch (Exception ex)
            {
                return Json(new { status = "Erro" });

            }

        }

        public ActionResult Pesquisar(servico _obj)
        {
            try
            {
                var parametro = String.Format("select * from  servicos where  nome  LIKE  '%{0}%' ", _obj.nome);
                var retorno = repositorio.SelectDataReader(parametro);
                return PartialView("Listar", retorno);

            }
            catch (Exception ex)
            {
                return Json(new { status = "Erro" });

            }
        }
    }
}