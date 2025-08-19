using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AplicacionNomina.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El usuario es obligatorio.")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        public string Clave { get; set; }
    }
}
