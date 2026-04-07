using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace gestionStock.Models
{
    public enum EtatProduit
    {
        EN_STOCK, EN_RUPTURE
    }
    public class Produit
    {
        public int Id { get; set;}

        [Required(ErrorMessage = "Le libellé est obligatoire")]
        [StringLength(100, ErrorMessage = "Max 100 caractères")]
        public string Libelle { get; set; }

        [Required(ErrorMessage = "La quantité est obligatoire")]
        [Range(1, int.MaxValue, ErrorMessage = "La quantité doit être > 0")]
        public int Quantite { get; set; }

        [Required(ErrorMessage = "La catégorie est obligatoire")]
        public int CategorieId { get; set; }
        
        public bool IsArchived{ get; set;} = false;
        public EtatProduit etat { get; set;} = EtatProduit.EN_STOCK;
        public Categorie categorie { get; set;}
        
    }
}