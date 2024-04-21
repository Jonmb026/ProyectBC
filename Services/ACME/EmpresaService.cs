using DataAccess.ACME;
using Models.ACME;

namespace Services.ACME
{
    public class EmpresaService
    {
        public bool Crear(EmpresaEntidad empresaEntidad)
        {
            EmpresaDA empresaDA = new EmpresaDA();

            try
            {
                empresaDA.Insertar(empresaEntidad);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Modificar(EmpresaEntidad empresaEntidad)
        {
            EmpresaDA empresaDA = new EmpresaDA();

            try
            {
                empresaDA.Modificar(empresaEntidad);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public EmpresaEntidad? BuscarxID(int IDEmpresa)
        {
            EmpresaDA? empresaDA = new EmpresaDA();

            try
            {
                return empresaDA.BuscarID(IDEmpresa);
            }
            catch
            {
                return null;
            }
        }

        public List<EmpresaEntidad>? Listar()
        {
            EmpresaDA? empresaDA = new EmpresaDA();

            try
            {
                return empresaDA.Listar();
            }
            catch
            {
                return null;
            }
        }

        public bool Eliminar(EmpresaEntidad empresaEntidad)
        {
            EmpresaDA empresaDA = new EmpresaDA();

            try
            {
                empresaDA.Anular(empresaEntidad);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
