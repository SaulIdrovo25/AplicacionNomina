using AplicacionNomina.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace AplicacionNomina.Controllers
{
    public class SalarioController : Controller
    {
        private string conexion = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        public ActionResult Index()
        {
            List<Salario> lista = new List<Salario>();

            try
            {
                using (SqlConnection cn = new SqlConnection(conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_HistorialSalarios", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lista.Add(new Salario
                        {
                            EmpNo = Convert.ToInt32(reader["emp_no"]),
                            NombreCompleto = reader["NombreCompleto"].ToString(),
                            Monto = Convert.ToDecimal(reader["Monto"]),
                            Desde = Convert.ToDateTime(reader["Desde"]),
                            Hasta = reader["Hasta"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["Hasta"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar salarios: " + ex.Message;
            }

            return View(lista);
        }

        public ActionResult Create()
        {
            ViewBag.Empleados = ObtenerListaEmpleados();
            return View(new SalarioRegistro());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SalarioRegistro model)
        {
            ViewBag.Empleados = ObtenerListaEmpleados();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                using (SqlConnection cn = new SqlConnection(conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_InsertarSalario", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@emp_no", model.EmpNo);
                    cmd.Parameters.AddWithValue("@salary", model.Monto);
                    cmd.Parameters.AddWithValue("@from_date", model.Desde);
                    cmd.Parameters.AddWithValue("@usuario", Session["Usuario"]?.ToString() ?? "admin");//llama al nombre de usuario con el que se inició sesión


                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                TempData["Mensaje"] = "Salario registrado correctamente.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al registrar salario: " + ex.Message;
                return View(model);
            }
        }

        private List<SelectListItem> ObtenerListaEmpleados()
        {
            var empleados = new List<SelectListItem>();

            using (SqlConnection cn = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("SELECT emp_no, first_name + ' ' + last_name AS Nombre FROM employees WHERE is_active = 1", cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    empleados.Add(new SelectListItem
                    {
                        Value = reader["emp_no"].ToString(),
                        Text = reader["Nombre"].ToString()
                    });
                }
            }

            return empleados;
        }

        public ActionResult Auditoria()
        {
            List<AuditoriaSalario> auditoria = new List<AuditoriaSalario>();

            try
            {
                using (SqlConnection cn = new SqlConnection(conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_HistorialAuditoriaSalarios", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        auditoria.Add(new AuditoriaSalario
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Usuario = reader["usuario"].ToString(),
                            FechaActualizacion = Convert.ToDateTime(reader["fechaActualizacion"]),
                            DetalleCambio = reader["DetalleCambio"].ToString(),
                            Salario = Convert.ToDecimal(reader["salario"]),
                            EmpNo = Convert.ToInt32(reader["emp_no"]),
                            NombreCompleto = reader["NombreCompleto"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar auditoría: " + ex.Message;
            }

            return View(auditoria);
        }
        public void DescargarExcel()
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=HistorialSalarios.xls");
            Response.ContentType = "application/vnd.ms-excel";

            using (SqlConnection cn = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_HistorialSalarios", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                Response.Write("<table border='1'>");
                Response.Write("<tr><th>Empleado</th><th>Monto</th><th>Desde</th><th>Hasta</th></tr>");

                while (reader.Read())
                {
                    string nombre = reader["NombreCompleto"].ToString();
                    string empNo = reader["emp_no"].ToString();
                    string monto = Convert.ToDecimal(reader["Monto"]).ToString("C");
                    string desde = Convert.ToDateTime(reader["Desde"]).ToShortDateString();
                    string hasta = reader["Hasta"] == DBNull.Value ? "Actual" : Convert.ToDateTime(reader["Hasta"]).ToShortDateString();

                    Response.Write($"<tr><td>{nombre} ({empNo})</td><td>{monto}</td><td>{desde}</td><td>{hasta}</td></tr>");
                }

                Response.Write("</table>");
                Response.End();
            }
        }


    }
}

