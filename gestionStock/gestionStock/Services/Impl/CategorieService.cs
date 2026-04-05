using gestionStock.Data;
using gestionStock.Models;
namespace gestionStock.Services.Impl
{
    public class CategorieService : ICategorieService 
    {
        private readonly AppDbContext _context;
        List<Categorie> ListCategories()
        {
            return _context.Categories.ToList();
        }
    }
}