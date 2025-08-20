using System;

namespace EmpleadosReutilizacion.Models
{
    public partial class LogAuditoriaSalario
    {
        public int Id { get; set; }
        public string Usuario { get; set; } = null!;
        public DateTime FechaActualizacion { get; set; }
        public string DetalleCambio { get; set; } = null!;
        public decimal Salario { get; set; }
        public int EmpNo { get; set; }

        public virtual Employee EmpNoNavigation { get; set; } = null!;
    }
}
