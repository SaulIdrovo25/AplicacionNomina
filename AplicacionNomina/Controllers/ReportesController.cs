using AplicacionNomina.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace AplicacionNomina.Controllers
{
    public class ReportesController : Controller
    {
        private string conexion = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        public ActionResult Index()
        {
            List<Reportes> lista = new List<Reportes>();

            try
            {
                using (SqlConnection cn = new SqlConnection(conexion))
                {
                    SqlCommand cmd = new SqlCommand("CargosUsuarios", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lista.Add(new Reportes
                        {
                            Usuario = reader["Usuario"].ToString(),
                            Salario = Convert.ToDecimal(reader["Salario"]),
                            FechaIngreso = Convert.ToDateTime(reader["Fecha_ingreso"]),
                            Rol = reader["Rol"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al cargar reportes: " + ex.Message;
            }

            return View(lista);
        }
        public ActionResult DescargarExcel()
        {
            List<Reportes> lista = new List<Reportes>();

            try
            {
                using (SqlConnection cn = new SqlConnection(conexion))
                {
                    SqlCommand cmd = new SqlCommand("CargosUsuarios", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lista.Add(new Reportes
                        {
                            Usuario = reader["Usuario"].ToString(),
                            Salario = Convert.ToDecimal(reader["Salario"]),
                            FechaIngreso = Convert.ToDateTime(reader["Fecha_ingreso"]),
                            Rol = reader["Rol"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al generar el archivo: " + ex.Message;
                return RedirectToAction("Index");
            }

            var sb = new System.Text.StringBuilder();
            sb.AppendLine("Usuario,Salario,FechaIngreso,Rol");

            foreach (var item in lista)
            {
                sb.AppendLine($"{item.Usuario},{item.Salario},{item.FechaIngreso:yyyy-MM-dd},{item.Rol}");
            }

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            return File(buffer, "text/csv", "ReporteUsuarios.csv");
        }


    }
}

