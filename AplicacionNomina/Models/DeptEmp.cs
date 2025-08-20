using System;

namespace EmpleadosReutilizacion.Models
{
    public partial class DeptEmp
    {
        public int EmpNo { get; set; }
        public int DeptNo { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public virtual Employee EmpNoNavigation { get; set; } = null!;
        public virtual Department DeptNoNavigation { get; set; } = null!;
    }
}
