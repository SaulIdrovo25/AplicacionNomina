using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AplicacionNomina.Models
{
    public class Empleado
    {
        [Key]
        [Display(Name = "Código")]
        public int Codigo { get; set; } // emp_no

        [Required(ErrorMessage = "La cédula es obligatoria.")]
        [StringLength(50, ErrorMessage = "La cédula no puede tener más de 50 caracteres.")]
        [Display(Name = "Cédula")]
        public string Cedula { get; set; } // ci

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime FechaNacimiento { get; set; } // birth_date

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
        [Display(Name = "Nombres")]
        public string Nombres { get; set; } // first_name

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(50, ErrorMessage = "El apellido no puede tener más de 50 caracteres.")]
        [Display(Name = "Apellidos")]
        public string Apellidos { get; set; } // last_name

        [Required(ErrorMessage = "El género es obligatorio.")]
        [Display(Name = "Género")]
        [RegularExpression("M|F", ErrorMessage = "El género debe ser 'M' (masculino) o 'F' (femenino).")]
        public string Genero { get; set; } // gender

        [Required(ErrorMessage = "La fecha de contratación es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de contratación")]
        public DateTime FechaContratacion { get; set; } // hire_date

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Ingresa un correo electrónico válido.")]
        [StringLength(100, ErrorMessage = "El correo no puede tener más de 100 caracteres.")]
        [Display(Name = "Correo electrónico")]
        public string Correo { get; set; } // correo

        [Display(Name = "Activo")]
        public bool Activo { get; set; } = true; // is_active
    }
}
