using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JesEstetica.Controllers
{
    public class ClienteController : Controller
    {
        private BaseDados repositorio;

        public ClienteController()
        {
            repositorio = new BaseDados();
        }
        // GET: Cliente
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Listar()
        {

            var retorno = repositorio.SelectDataReader("select * from clientes");
            return PartialView(retorno);
        }


        public ActionResult Cadastrar()
        {

            return View();
        }

        public ActionResult Salvar(cliente _cliente)
        {
            try
            {

                repositorio.InsertCliente(_cliente);

                return Json(new { status = "Sucesso" });
            }
            catch (Exception ex)
            {
                return Json(new { status = "Erro" });

            }
        }
        public ActionResult Alterar(cliente _cliente)
        {
            try
            {

                repositorio.Update(_cliente);

                return Json(new { status = "Sucesso" });
            }
            catch (Exception ex)
            {
                return Json(new { status = "Erro" });

            }
        }
        public ActionResult Editar(long? id)
        {
            var parametro = String.Format("select * from  clientes where  idClientes ={0} ", id);
            var retorno = repositorio.SelectDataReader(parametro);
            return View(retorno.FirstOrDefault());
        }

        public ActionResult Carregar(long? id)
        {
            var parametro = String.Format("select * from  clientes where  idClientes ={0} ", id);
            var retorno = repositorio.SelectDataReader(parametro);
            return PartialView(retorno.FirstOrDefault());
        }

        public ActionResult Busca()
        {

            return View();
        }

        public ActionResult Pesquisar(cliente _obj)
        {
            try
            {
                var parametro = String.Format("select * from  clientes where  nome  LIKE  '{0}%'and cpf  LIKE '%{1}%'and rg LIKE '%{2}%' ", _obj.nome, _obj.CPF, _obj.RG);
                var retorno = repositorio.SelectDataReader(parametro);
                return PartialView("Listar", retorno);

            }
            catch (Exception ex)
            {
                return Json(new { status = "Erro" });

            }
        }

        public ActionResult ListarCliente(cliente _obj)
        {
            try
            {
                var parametro = String.Format("select * from  clientes where  nome  LIKE  '{0}%'and cpf  LIKE '%{1}%'and rg LIKE '%{2}%' ", _obj.nome, _obj.CPF, _obj.RG);
                var retorno = repositorio.SelectDataReader(parametro);
                return PartialView("ListarCliente", retorno);

            }
            catch (Exception ex)
            {
                return Json(new { status = "Erro" });

            }
        }
        public ActionResult historicoCliente(long? id  , bool finaliza)
        {
            try
            {
                
                var retorno = repositorio.Proc(new cliente() { idClientes=(long)id});
                if (finaliza == false)
                    retorno = retorno.Where(c => c.status.finaliza == false).ToList();
                return PartialView( retorno);

            }
            catch (Exception ex)
            {
                return Json(new { status = "Erro" });

            }
        }
    }
}