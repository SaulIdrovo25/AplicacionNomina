using System;

namespace EmpleadosReutilizacion.Models
{
    public partial class Salary
    {
        public int EmpNo { get; set; }
        public decimal SalaryAmount { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public virtual Employee EmpNoNavigation { get; set; } = null!;
    }
}
