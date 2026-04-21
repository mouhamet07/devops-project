using System.ComponentModel;
using gestionStock.Models;
namespace gestionStock.Services
{
    public interface ICategorieService
    {
        List<Categorie> ListCategories() ;
        Categorie? FindById(int id);
        Categorie? GetCategorieByName(string name);
        bool AddCategorie(Categorie categorie);
    }
}