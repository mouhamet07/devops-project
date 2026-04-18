using gestionStock.Data;
using gestionStock.Models;
using Microsoft.EntityFrameworkCore;

namespace gestionStock.Services.Impl
{
    public class ProduitService : IProduitService 
    {
        private readonly AppDbContext _context;
        private readonly ICategorieService _categorieService;
        public ProduitService(AppDbContext context, ICategorieService categorieService)
        {
            _context = context;
            _categorieService = categorieService;
        }

        public bool AddProduit(Produit produit)
        {
            

            var categorie = _context.Categories.Find(produit.CategorieId);

            if (produit.Quantite > 0 && categorie != null)
            {
                produit.categorie = categorie;

                _context.Produits.Add(produit);
                _context.SaveChanges();

                
                return true;
            }

            
            return false;
        }

        public List<Produit> GetAllProduits(int page = 1, string categorie = "all", string etat = "all")
        {
            var query = _context.Produits.Include(p => p.categorie).AsQueryable();

            if (categorie != "all")
            {
                var cat = _categorieService.GetCategorieByName(categorie);
                if (cat != null)
                {
                    query = query.Where(p => p.CategorieId == cat.Id);
                }
            }

            if (etat != "all")
            {
                bool isDisponible = etat == "disponible";
                query = query.Where(p => p.Quantite > 0 == isDisponible);
            }

            int pageSize = 10;
            return query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<Produit> GetAllProduitsNoPage(string categorie = "all", string etat = "all")
        {
            var query = _context.Produits.Include(p => p.categorie).AsQueryable();

            if (categorie != "all")
            {
                var cat = _categorieService.GetCategorieByName(categorie);
                if (cat != null)
                {
                    query = query.Where(p => p.CategorieId == cat.Id);
                }
            }

            if (etat != "all")
            {
                bool isDisponible = etat == "disponible";
                query = query.Where(p => p.Quantite > 0 == isDisponible);
            }

            return query
            .OrderByDescending(p => p.Id)
            .ToList();
        }

        public int CountTotal(string categorie = "all", string etat = "all")
        {
            var query = _context.Produits.AsQueryable();

            if (categorie != "all")
            {
                var cat = _categorieService.GetCategorieByName(categorie);
                if (cat != null)
                {
                    query = query.Where(p => p.CategorieId == cat.Id);
                }
            }

            if (etat != "all")
            {
                bool isDisponible = etat == "disponible";
                query = query.Where(p => p.Quantite > 0 == isDisponible);
            }

            return query.Count();
        }

        public List<Produit> SearchProduits(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return new List<Produit>();

            return _context.Produits
                .Where(p => p.Libelle.Contains(searchTerm))
                .ToList();
        }

        public List<Produit> GetProduitsByCategorie(int categorieId)
        {
            return _context.Produits.Where(p => p.CategorieId == categorieId).ToList();
        }

        public Produit? GetProduitByName(string name)
        {
            return _context.Produits.FirstOrDefault(p => p.Libelle == name);
        }

        public Produit? GetProduitById(int id)
        {
            return _context.Produits.FirstOrDefault(p => p.Id == id);
        }

        public bool UpdateProduit(Produit produit)
        {
            var existing = _context.Produits.FirstOrDefault(p => p.Id == produit.Id);

            if (existing == null)
                return false;

            var categorie = _context.Categories.Find(produit.CategorieId);

            if (categorie == null || produit.Quantite < 0)
                return false;

            existing.Libelle = produit.Libelle;
            existing.Quantite = produit.Quantite;
            existing.CategorieId = produit.CategorieId;
            existing.categorie = categorie;

            _context.SaveChanges();

            return true;
        }
    }
}