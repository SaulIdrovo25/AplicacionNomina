using System;

namespace EmpleadosReutilizacion.Models
{
    public partial class Title
    {
        public int EmpNo { get; set; }
        public string TitleName { get; set; } = null!;
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public virtual Employee EmpNoNavigation { get; set; } = null!;
    }
}
