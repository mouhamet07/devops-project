namespace gestionStock.Models
{
    public class Categorie
    {
        public int Id {get; set;}
        public string Libelle {get; set;}
        public bool IsArchived {get; set;} = true;
    }
}