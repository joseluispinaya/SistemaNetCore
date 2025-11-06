using Microsoft.AspNetCore.Mvc;
using Sistema.BLL;
using Sistema.Entity;

namespace Sistema.Web.Controllers
{
    public class CategoriaController(CategoriaService categoriaService) : Controller
    {
        private readonly CategoriaService _categoriaService = categoriaService;
        public IActionResult Index()
        {
            var categorias = _categoriaService.GetAllCategorias();
            return View(categorias);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var respuesta = _categoriaService.AddCategoria(categoria);
            if (respuesta)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        public IActionResult Edit(int id)
        {
            Categoria categoria = _categoriaService.GetCategoriaById(id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        [HttpPost]
        public IActionResult Edit(Categoria categoria)
        {
            if (!ModelState.IsValid)
                return View();


            var respuesta = _categoriaService.UpdateCategoria(categoria);

            if (respuesta)
                return RedirectToAction(nameof(Index));
            else
                return View();
        }


    }
}
