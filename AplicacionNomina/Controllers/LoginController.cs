//Importar librerias para utilizar los modelos de la aplicacion
using AplicacionNomina.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
//Importar librerias para conectar a SQL Server
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
//liberias para uso de funcionlalidades
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace AplicacionNomina.Controllers
{
    public class LoginController : Controller
    {
        //Metodos tipo GET 
        public ActionResult Autenticar()
        {
            return View();
        }
        public ActionResult Registrarse()
        {
            return View();
        }
        // Metodos tipo POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Autenticar(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand("AutenticarUsuario", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Hashear la clave en texto plano
                    byte[] claveHashBinaria = HashPasswordToBytes(model.Clave);

                    cmd.Parameters.AddWithValue("@usuario", model.Usuario);
                    cmd.Parameters.Add("@claveHash", SqlDbType.VarBinary, 256).Value = claveHashBinaria;

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        int autenticado = Convert.ToInt32(reader["Autenticado"]);
                        string mensaje = reader["Mensaje"].ToString();

                        if (autenticado == 1)
                        {
                            // Puedes almacenar info de sesión aquí si quieres
                            Session["Usuario"] = model.Usuario;

                            return RedirectToAction("Inicio", "Menu");
                        }
                        else
                        {
                            ModelState.AddModelError("", mensaje);
                            return View(model);
                        }
                    }

                    ModelState.AddModelError("", "Error inesperado al autenticar.");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al autenticar: " + ex.Message);
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Registrarse(RegistroViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                {
                    cn.Open();
                    ViewBag.Conexion = "Conexión exitosa a la base de datos.";

                    using (SqlCommand cmd = new SqlCommand("sp_RegistrarEmpleadoCompleto", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Ci", model.Ci);
                        cmd.Parameters.AddWithValue("@BirthDate", model.BirthDate);
                        cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", model.LastName);
                        cmd.Parameters.AddWithValue("@Gender", model.Gender);
                        cmd.Parameters.AddWithValue("@HireDate", model.HireDate);
                        cmd.Parameters.AddWithValue("@Correo", model.Correo);
                        cmd.Parameters.AddWithValue("@Usuario", model.Usuario);
                        cmd.Parameters.AddWithValue("@Rol", model.Rol);

                        // Encriptar la clave
                        byte[] claveHash = HashPasswordToBytes(model.Clave);
                        cmd.Parameters.Add("@ClaveHash", SqlDbType.VarBinary, 256).Value = claveHash;

                        cmd.ExecuteNonQuery();
                        return RedirectToAction("Autenticar", "Login");

                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al registrar: " + ex.Message;
                return View(model);
            }
        }

        public static byte[] HashPasswordToBytes(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }




    }
}