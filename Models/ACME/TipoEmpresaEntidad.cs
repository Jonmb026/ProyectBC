
namespace Models.ACME
{
    public class TipoEmpresaEntidad
    {
    
        public int IDTipoEmpresa { get; set; }
        public string TipoEmpresa { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Sigla { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
    }

}
