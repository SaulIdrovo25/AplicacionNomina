using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//Importar librerias para conectar a SQL Server
using System.Data;
using System.Data.SqlClient;
//Importar librerias para manejar funcionalidades

namespace AplicacionNomina.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Autenticar()
        {
            return View();
        }
        public ActionResult Registrarse()
        {
            return View();
        }
    }
}