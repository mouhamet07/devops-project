using Microsoft.AspNetCore.Mvc;
using gestionStock.Models;
using gestionStock.Services;

namespace gestionStock.Controllers
{
    public class ProduitController : Controller
    {
        private readonly IProduitService _produitService;

        public ProduitController(IProduitService produitService)
        {
            _produitService = produitService;
        }

        // afficher formulaire
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // 🔹 ajouter produit
        [HttpPost]
        public IActionResult Create(Produit produit)
        {
            _produitService.AddProduit(produit);
            return RedirectToAction("Index");
        }
    }
}