using Microsoft.AspNetCore.Mvc;
using gestionStock.Models;
using gestionStock.Services;

namespace gestionStock.Controllers
{
    public class CategorieController : Controller
    {
        private readonly ICategorieService _categorieService;

        public CategorieController(ICategorieService categorieService)
        {
            _categorieService = categorieService;
        }

        // afficher formulaire
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // ajout
        [HttpPost]
        public IActionResult Create(Categorie categorie)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Veuillez fournir un libelle";
                return View(categorie);
            }

            var result = _categorieService.AddCategorie(categorie);

            if (result)
                TempData["success"] = "Catégorie ajoutée avec succès";
            else
                TempData["error"] = "Erreur lors de l'ajout";

            return RedirectToAction("Create");
        }
    }
}