using System.ComponentModel.DataAnnotations;
namespace Models.ACME
{
    public class EmpresaEntidad
    {
        public int IDEmpresa { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Debe seleccionar una empresa.")]
        [Display(Name = "Código")]

        public int? IDTipoEmpresa { get; set; } // int? significa que acepta valores nulos
        [Required(ErrorMessage = "Debe seleccionar un tipo de empresa.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un tipo de empresa.")]
        [Display(Name = "Tipo empresa")]

        public string Empresa { get; set; } = string.Empty;
        [Required(ErrorMessage = "El nombre de la empresa es obligatorio.")]
        [Display(Name = "Nombre empresa")]

        public string Direccion { get; set; } = string.Empty;
        [Required(ErrorMessage = "La dirección de la empresa es obligatoria.")]
        [Display(Name = "Dirección")]

        public string RUC { get; set; } = string.Empty;
        [Required(ErrorMessage = "El RUC de la empresa es obligatorio.")]
        [Display(Name = "RUC")]

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Debe ingresar la fecha de creación.")]
        [Display(Name = "Fecha creación")]

        public decimal Presupuesto { get; set; }
        [Required(ErrorMessage = "Debe ingresar el presupuesto.")]
        [Display(Name = "Presupuesto")]

        public bool Activo { get; set; } = true;

        // Propiedad de navegación a TipoEmpresa
        public TipoEmpresaEntidad? TipoEmpresaEntidad { get; set; }
    }
}
