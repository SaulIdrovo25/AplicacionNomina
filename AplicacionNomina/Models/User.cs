namespace EmpleadosReutilizacion.Models
{
    public partial class User
    {
        public int EmpNo { get; set; }
        public string Usuario { get; set; } = null!;
        public byte[] ClaveHash { get; set; } = null!;
        public string Rol { get; set; } = "RRHH";

        public virtual Employee EmpNoNavigation { get; set; } = null!;
    }
}
