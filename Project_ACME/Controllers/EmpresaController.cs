using Microsoft.AspNetCore.Mvc;
using Models.ACME;
using Services.ACME;

namespace Project_ACME.Controllers
{
    public class EmpresaController : Controller
    {
        private readonly EmpresaService _empresaService;

        public EmpresaController()
        {
            _empresaService = new EmpresaService();
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Listar las empresas//
            List<EmpresaEntidad>? listaEmpresaEntidad;
            listaEmpresaEntidad = _empresaService.Listar();

            if (listaEmpresaEntidad == null)
            {
                // Manejar el caso en que listaEmpresaEntidad es null, como redirigir a una vista de error.
                return RedirectToAction("Error", "Home");
            }

            return View(listaEmpresaEntidad);
        }

        [HttpGet]
        public IActionResult Modificar(int id)
        {
            try
            {
                EmpresaEntidad empresa = _empresaService.BuscarxID(id);
                return View(empresa);
            }
            catch (Exception ex)
            {
                // Manejar el error, puedes redirigir a una vista de error o devolver una vista con el error.
                return View("Error", ex);
            }
        }

        [HttpPost]
        public IActionResult Modificar(EmpresaEntidad empresaEntidad)
        {
            try
            {
                _empresaService.Modificar(empresaEntidad);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Manejar el error, puedes redirigir a una vista de error o devolver una vista con el error.
                return View("Error", ex);
            }
        }
    }
}
