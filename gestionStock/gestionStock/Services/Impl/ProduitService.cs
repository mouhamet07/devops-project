using gestionStock.Data;
using gestionStock.Models;

namespace gestionStock.Services.Impl
{
    public class ProduitService : IProduitService 
    {
        private readonly AppDbContext _context;
        private readonly ICategorieService _categorieService;
        public ProduitService(AppDbContext context, ICategorieService categorieService)
        {
            _context = context;
            _categorieService = _categorieService;
        }

        public bool AddProduit(Produit produit)
        {

            if (produit.Quantite <= 0)
            {
                return false;
            }

            var categorie = _context.Categories.Find(produit.CategorieId);

            if (categorie != null)
            {
                produit.categorie = categorie;

                _context.Produits.Add(produit);
                _context.SaveChanges();
            }
            return true;
        }
        }
        
}