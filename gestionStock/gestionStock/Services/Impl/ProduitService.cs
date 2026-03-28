using gestionStock.Data;
using gestionStock.Models;

namespace gestionStock.Services.Impl
{
    public class ProduitService : IProduitService 
    {
        private readonly AppDbContext _context;
        public ProduitService(AppDbContext context)
        {
            _context = context;
        }

        public void AddProduit(Produit produit)
        {
            if (produit == null)
                throw new ArgumentNullException(nameof(produit));
            if (produit.Quantite <= 0)
            {
                produit.etat = EtatProduit.EN_RUPTURE;
                produit.Quantite = 0; 
            }
            else
            {
                produit.etat = EtatProduit.EN_STOCK;
            }

            produit.IsArchived = false;
        
            Categorie categorie = _context.Categories.Find(produit.categorie.Id);
            if (categorie == null)
                throw new Exception("Cette catégorie n'existe pas");

            produit.categorie = categorie;

            _context.Produits.Add(produit);
            _context.SaveChanges();
        }
    }
    }
