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
        // afficher tous les produits
        [HttpGet]
        public IActionResult List(int page = 1, string search = "", string category = "", string state = "")
        {
            const int pageSize = 6;
            
            // Récupérer TOUS les produits filtrés SANS pagination
            var allProduits = _produitService.GetAllProduitsNoPage(
                category == "" ? "all" : category, 
                state == "" ? "all" : state
            );
            
            // Filtrer par recherche sur le libellé
            if (!string.IsNullOrEmpty(search))
            {
                allProduits = allProduits.Where(p => p.Libelle.ToLower().Contains(search.ToLower())).ToList();
            }

            // Calculer la pagination
            var totalCount = allProduits.Count();
            var totalPages = totalCount > 0 ? (int)Math.Ceiling(totalCount / (double)pageSize) : 1;
            
            // Valider le numéro de page
            if (page < 1) page = 1;
            if (page > totalPages && totalPages > 0) page = totalPages;
            
            // Appliquer la pagination
            var produits = allProduits
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.Categories = _categorieService.ListCategories();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentSearch = search;
            ViewBag.CurrentCategory = category;
            ViewBag.CurrentState = state;
            ViewBag.TotalCount = totalCount;
            ViewBag.PageSize = pageSize;

            return View(produits);
        }

        // afficher les produits par categorie
        [HttpGet]
        public IActionResult ByCategorie(int categorieId)
        {
            var produits = _produitService.GetProduitsByCategorie(categorieId);
            return View(produits);  
        }
        // afficher produit par nom
        [HttpGet]
        public IActionResult ByName(string name)
        {
            var produit = _produitService.GetProduitByName(name);
            if (produit == null)            {
                TempData["Error"] = "Produit non trouvé.";
                return RedirectToAction("Index");
            }
            return View(produit);
        }
    }
}
