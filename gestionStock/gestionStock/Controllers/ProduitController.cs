using Microsoft.AspNetCore.Mvc;
using gestionStock.Models;
using gestionStock.Services;

namespace gestionStock.Controllers
{
    public class ProduitController : Controller
    {
        private readonly IProduitService _produitService;
        private readonly ICategorieService _categorieService;
        public ProduitController(IProduitService produitService, ICategorieService categorieService)
        {
            _produitService = produitService;
            _categorieService = categorieService;
        }

        // afficher formulaire
        [HttpGet]
        public IActionResult Create()
        {
            var categories = _categorieService.ListCategories();
            ViewBag.Categories = categories;
            return View();
        }

        //  ajouter produit
        [HttpPost]
        public IActionResult Create(Produit produit)
        {
            _produitService.AddProduit(produit);
            return RedirectToAction("Create");
        }
    }
}