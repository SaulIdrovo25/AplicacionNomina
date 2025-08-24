using AplicacionNomina.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicacionNomina.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Settings()
        {
            return View();
        }
        public ActionResult Profile()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Login", "Cuenta");

            string usuario = Session["Usuario"].ToString();
            Empleado empleado = null;

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                cn.Open();

                // Consulta que une ambas tablas
                string query = @"
            SELECT e.emp_no, e.ci, e.birth_date, e.first_name, e.last_name, e.gender,
                   e.hire_date, e.correo, e.is_active
            FROM users u
            INNER JOIN employees e ON u.emp_no = e.emp_no
            WHERE u.usuario = @usuario";

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@usuario", usuario);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    empleado = new Empleado
                    {
                        Codigo = Convert.ToInt32(reader["emp_no"]),
                        Cedula = reader["ci"].ToString(),
                        FechaNacimiento = Convert.ToDateTime(reader["birth_date"]),
                        Nombres = reader["first_name"].ToString(),
                        Apellidos = reader["last_name"].ToString(),
                        Genero = reader["gender"].ToString(),
                        FechaContratacion = Convert.ToDateTime(reader["hire_date"]),
                        Correo = reader["correo"].ToString(),
                        Activo = Convert.ToBoolean(reader["is_active"])
                    };
                }
            }

            if (empleado == null)
                return HttpNotFound("Empleado no encontrado.");

            return View(empleado);
        }


    }
}