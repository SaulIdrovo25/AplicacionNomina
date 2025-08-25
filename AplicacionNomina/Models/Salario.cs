using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionNomina.Models
{
    public class Salario
    {
        public int EmpNo { get; set; }
        public string NombreCompleto { get; set; }
        public decimal Monto { get; set; }
        public DateTime Desde { get; set; }
        public DateTime? Hasta { get; set; }
    }
}