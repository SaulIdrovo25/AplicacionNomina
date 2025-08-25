using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AplicacionNomina.Models
{
    public class SalarioRegistro
    {
        [Required]
        [Display(Name = "Empleado")]
        public int EmpNo { get; set; }

        [Required]
        [Display(Name = "Monto del salario")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El salario debe ser mayor que cero.")]
        public decimal Monto { get; set; }

        [Required]
        [Display(Name = "Fecha de inicio")]
        [DataType(DataType.Date)]
        public DateTime Desde { get; set; }
    }
}