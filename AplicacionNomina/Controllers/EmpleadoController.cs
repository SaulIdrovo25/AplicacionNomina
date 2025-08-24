using AplicacionNomina.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace AplicacionNomina.Controllers
{
    public class EmpleadoController : Controller
    {
        private string conexion = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        // GET: Empleados
        public ActionResult Index()
        {
            List<Empleado> lista = new List<Empleado>();

            try
            {
                using (SqlConnection cn = new SqlConnection(conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_ListarEmpleadosActivos", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lista.Add(new Empleado
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
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // Puedes registrar el error o mostrar un mensaje
                ViewBag.Error = "Error al cargar empleados: " + ex.Message;
            }

            return View("Index", lista);
        }


        // GET: Empleado/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Empleado/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Empleado model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (SqlConnection cn = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_CrearEmpleado", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ci", model.Cedula);
                cmd.Parameters.AddWithValue("@birth_date", model.FechaNacimiento);
                cmd.Parameters.AddWithValue("@first_name", model.Nombres);
                cmd.Parameters.AddWithValue("@last_name", model.Apellidos);
                cmd.Parameters.AddWithValue("@gender", model.Genero);
                cmd.Parameters.AddWithValue("@hire_date", model.FechaContratacion);
                cmd.Parameters.AddWithValue("@correo", model.Correo);

                cn.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // GET: Empleado/Edit/5
        public ActionResult Edit(int id)
        {
            Empleado model = null;

            using (SqlConnection cn = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerEmpleadoPorId", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@emp_no", id);

                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    model = new Empleado
                    {
                        Codigo = Convert.ToInt32(reader["emp_no"]),
                        Cedula = reader["ci"].ToString(),
                        FechaNacimiento = Convert.ToDateTime(reader["birth_date"]),
                        Nombres = reader["first_name"].ToString(),
                        Apellidos = reader["last_name"].ToString(),
                        Genero = reader["gender"].ToString(),
                        FechaContratacion = Convert.ToDateTime(reader["hire_date"]),
                        Correo = reader["correo"].ToString(),
                        Activo = reader["is_active"].ToString() == "1"
                    };
                }
            }

            if (model == null)
                return HttpNotFound();

            return View(model);
        }

        // POST: Empleado/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Empleado model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (SqlConnection cn = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_ModificarEmpleado", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", model.Codigo);
                cmd.Parameters.AddWithValue("@ci", model.Cedula);
                cmd.Parameters.AddWithValue("@birth_date", model.FechaNacimiento);
                cmd.Parameters.AddWithValue("@first_name", model.Nombres);
                cmd.Parameters.AddWithValue("@last_name", model.Apellidos);
                cmd.Parameters.AddWithValue("@gender", model.Genero);
                cmd.Parameters.AddWithValue("@hire_date", model.FechaContratacion);
                cmd.Parameters.AddWithValue("@correo", model.Correo);

                cn.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // GET: Empleado/Details/5
        public ActionResult Details(int id)
        {
            Empleado model = null;

            using (SqlConnection cn = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerEmpleadoPorId", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@emp_no", id);

                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    model = new Empleado
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

            if (model == null)
                return HttpNotFound();

            return View(model);
        }


        // GET: Empleado/Desactivar/5
        public ActionResult Desactivar(int id)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_DesactivarEmpleado", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@emp_no", id);

                    cn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        TempData["Error"] = "No se encontró el empleado o ya está desactivado.";
                    }
                    else
                    {
                        TempData["Mensaje"] = "Empleado desactivado correctamente.";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Ocurrió un error al desactivar el empleado.";
                // Aquí podrías registrar el error si usas logging
            }

            return RedirectToAction("Index");
        }

        public ActionResult Inactivos()
        {
            List<Empleado> lista = new List<Empleado>();

            using (SqlConnection cn = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_ListarEmpleadosInactivos", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Empleado
                    {
                        Codigo = Convert.ToInt32(reader["emp_no"]),
                        Cedula = reader["ci"].ToString(),
                        FechaNacimiento = Convert.ToDateTime(reader["birth_date"]),
                        Nombres = reader["first_name"].ToString(),
                        Apellidos = reader["last_name"].ToString(),
                        Genero = reader["gender"].ToString(),
                        FechaContratacion = Convert.ToDateTime(reader["hire_date"]),
                        Correo = reader["correo"].ToString(),
                        Activo = false
                    });
                }
            }

            return View(lista);
        }


        public ActionResult Reactivar(int id)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_ReactivarEmpleado", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@emp_no", id);

                    cn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        TempData["Error"] = "No se pudo reactivar el empleado. Verifica que esté inactivo.";
                    }
                    else
                    {
                        TempData["Mensaje"] = "Empleado reactivado correctamente.";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Ocurrió un error al intentar reactivar el empleado.";
                // Puedes registrar el error si usas logging
            }

            return RedirectToAction("Inactivos");
        }



    }
}

