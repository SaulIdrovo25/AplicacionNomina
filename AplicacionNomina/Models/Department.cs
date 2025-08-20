using System.Collections.Generic;

namespace EmpleadosReutilizacion.Models
{
    public partial class Department
    {
        public int DeptNo { get; set; }
        public string DeptName { get; set; } = null!;
        public bool IsActive { get; set; }

        public virtual ICollection<DeptEmp> DeptEmps { get; set; } = new List<DeptEmp>();
        public virtual ICollection<DeptManager> DeptManagers { get; set; } = new List<DeptManager>();
    }
}
