using System;
using System.Collections.Generic;

namespace EmpleadosReutilizacion.Models
{
    public partial class Employee
    {
        public int EmpNo { get; set; }
        public string Ci { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public DateTime HireDate { get; set; }
        public string Correo { get; set; } = null!;
        public bool IsActive { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<Salary> Salaries { get; set; } = new List<Salary>();
        public virtual ICollection<Title> Titles { get; set; } = new List<Title>();
        public virtual ICollection<DeptEmp> DeptEmps { get; set; } = new List<DeptEmp>();
        public virtual ICollection<DeptManager> DeptManagers { get; set; } = new List<DeptManager>();
        public virtual ICollection<LogAuditoriaSalario> LogAuditoriaSalarios { get; set; } = new List<LogAuditoriaSalario>();
    }
}
