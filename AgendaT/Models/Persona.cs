using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaT.Models
{
    public class Persona
    {
        public int PersonaId { get; set; }

        [Required(ErrorMessage = "{0} Es Obligatorio")]
        [StringLength(40, ErrorMessage = "Use menos caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "{0} Es Obligatorio")]
        [StringLength(40, ErrorMessage = "Use menos caracteres")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "{0} Es Obligatorio")]
        [Range(18, int.MaxValue, ErrorMessage = "Edad Invalida")]
        public int Edad { get; set; }

        [Required(ErrorMessage = "{0} Es Obligatorio")]
        [StringLength(40, ErrorMessage = "Use menos caracteres")]
        [EmailAddress(ErrorMessage = "Correo Invalido")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "{0} Es Obligatorio")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "{0} Es Obligatorio")]
        public TipoTelefono TipoTelefono { get; set; }

        public string Foto { get; set; }
    }

    public enum TipoTelefono
    {
        Personal, Comercial
    }
}
