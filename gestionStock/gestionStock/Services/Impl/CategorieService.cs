using gestionStock.Data;
using gestionStock.Models;
namespace gestionStock.Services.Impl
{
    public class CategorieService : ICategorieService
    {
        private readonly AppDbContext _context;
        public CategorieService(AppDbContext context)
        {
            _context = context;
        }
        public List<Categorie> ListCategories()
        {
            return _context.Categories
                .Where(c => !c.IsArchived)
                .ToList();
        }
        public bool AddCategorie(Categorie categorie)
        {
            if (categorie != null && FindById(categorie.Id) == null)
            {
                _context.Categories.Add(categorie);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public Categorie? FindById(int id)
        {
            return _context.Categories
                .FirstOrDefault(c => c.Id == id && !c.IsArchived);
        }

        public Categorie? GetCategorieByName(string name)
        {
            return _context.Categories
                .FirstOrDefault(c => c.Libelle == name && !c.IsArchived);
        }
    }
}