using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionNomina.Models
{
    public class AuditoriaSalario
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public string DetalleCambio { get; set; }
        public decimal Salario { get; set; }
        public int EmpNo { get; set; }
        public string NombreCompleto { get; set; } // opcional, si haces JOIN
    }
}