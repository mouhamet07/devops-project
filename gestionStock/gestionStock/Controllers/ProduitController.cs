using Microsoft.AspNetCore.Mvc;
using gestionStock.Models;
using gestionStock.Services;
using System.Reflection.PortableExecutable;

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
            bool test= _produitService.AddProduit(produit);

            if (!test)
            {
                TempData["Error"] = "Le produit n'a pas pu être ajouté. Vérifiez la quantité.";
                ViewBag.Categories = _categorieService.ListCategories();

                return View();
            }
            TempData["Success"] = "Produit ajouté avec succès ";
            return RedirectToAction("Create");
        }
    }
}