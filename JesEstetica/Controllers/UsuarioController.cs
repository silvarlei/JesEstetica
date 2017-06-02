using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JesEstetica.Controllers
{
    public class UsuarioController : Controller
    {

        private repositorioUsuario repositorio;

        public UsuarioController()
        {
            repositorio = new repositorioUsuario();
        }
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cadastrar()
        {
            return View();
        }
        public ActionResult Editar(long? id)
        {
            var parametro = String.Format("select * from  usuarios where  id ={0} ", id);
            var retorno = repositorio.SelectDataReader(parametro);
            return View(retorno.FirstOrDefault());
        }

        public ActionResult Alterar(usuario _obj)
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

                var parametro = String.Format("select * from  usuarios ");
                var retorno = repositorio.SelectDataReader(parametro);
                return PartialView(retorno);

            }
            catch (Exception ex)
            {

                return PartialView();

            }

        }
        public ActionResult Busca()
        {
            return View();
        }
        public ActionResult Pesquisar(usuario _obj)
        {
            try
            {
                var parametro = String.Format("select * from  usuarios where  nome  LIKE  '%{0}%' ", _obj.nome);
                var retorno = repositorio.SelectDataReader(parametro);
                return PartialView("Listar", retorno);

            }
            catch (Exception ex)
            {
                return Json(new { status = "Erro" });

            }
        }
        public ActionResult Salvar(usuario _obj)
        {
            try
            {

                repositorio.Insert(_obj);

                return Json(new { status = "Sucesso" });
            }
            catch (Exception ex)
            {
                return Json(new { status = "Erro" });

            }
        }
    }
}