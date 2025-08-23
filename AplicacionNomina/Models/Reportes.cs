using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AplicacionNomina.Models
{
    public class Reportes
    {
        public Reportes() 
        {
            conexionDB = "Server=DESKTOP-OJHCQ14; Database = EmpleadosReutilizacion; Integrated Security=True; ";
        }

        public string conexionDB { get; set; }
        public string usuario { get; set; }
        public string salaraio { get; set; }
        public string fecha { get; set; }
        public string cargo { get; set; }

        public System.Data.DataTable[] EjecutarConsulta(string procedure)
        {
            DataTable TablaDatos = new DataTable();
            using (SqlConnection connection = new SqlConnection(conexionDB))
            { 
                connection.Open();
                SqlCommand sqlCmd = new SqlCommand(procedure, connection);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader reades = sqlCmd.ExecuteReader())
                {
                    TablaDatos.Load(reades);
                }
                return new System.Data.DataTable[] { TablaDatos };
            }
        }
    }
}