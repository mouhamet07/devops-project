using gestionStock.Models;
namespace gestionStock.Services
{
    public interface IProduitService
    {
        bool AddProduit(Produit produit);
        Produit? GetProduitByName(string name);
        List<Produit> GetProduitsByCategorie(int categorieId);
        int CountTotal(string categorie = "all", string etat = "all");
        List<Produit> GetAllProduits(int page = 1, string categorie = "all", string etat = "all");
        List<Produit> GetAllProduitsNoPage(string categorie = "all", string etat = "all");
        List<Produit> SearchProduits(string searchTerm);
        Produit? GetProduitById(int id);
        bool UpdateProduit(Produit produit);

    }
}