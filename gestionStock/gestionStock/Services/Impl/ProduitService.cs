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

        public void AddProduit(Produit produit)
        {
            if (produit == null)
                return;

            if (produit.Quantite == 0)
            {
                produit.etat = EtatProduit.EN_RUPTURE;
            }
            else
            {
                produit.etat = EtatProduit.EN_STOCK;
            }

            produit.IsArchived = false;

            var categorie = _context.Categories.Find(produit.CategorieId);

            if (categorie != null)
            {
                produit.categorie = categorie;

                _context.Produits.Add(produit);
                _context.SaveChanges();
            }
        }
        }
}