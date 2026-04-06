using System.ComponentModel.DataAnnotations;
namespace gestionStock.Models
{
    public class Categorie
    {
        public int Id {get; set;}
        [Required(ErrorMessage = "Le libellé est obligatoire")]
        [StringLength(100, ErrorMessage = "Le libellé ne doit pas dépasser 100 caractères")]
        public string Libelle {get; set;}
        public bool IsArchived {get; set;} = false;
    }
}