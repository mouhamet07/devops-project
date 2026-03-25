using gestionStock.Models;
namespace gestionStock.Services
{
    public interface IProduitService
    {
        void AddProduit(Produit produit);
        User? GetProduitById(int id);
        int CountTotal( string categorie ="all",string etat = "all" );
        List<Produit> GetAllProduits(int page = 1, string categorie ="all",string etat = "all" );
        void UpdateProduit(Produit produit);

    }
}