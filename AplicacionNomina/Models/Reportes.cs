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
        public string Usuario { get; set; }
        public decimal Salario { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Rol { get; set; }
    }
}