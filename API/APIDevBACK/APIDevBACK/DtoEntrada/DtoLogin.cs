using System.ComponentModel.DataAnnotations;

namespace APIDevBACK.DtoEntrada
{
    public class DtoLogin
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int Extension { get; set; }
        [Required]
        public int TipoMov {  get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Range(typeof(DateTime), "01/01/1900", "31/12/2100", ErrorMessage = "Fecha Incorrecta")]
        public DateTime Fecha { get; set; }

    }
}
