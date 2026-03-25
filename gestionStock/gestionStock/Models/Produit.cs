using System.ComponentModel;

namespace gestionStock.Models
{
    public enum EtatProduit
    {
        EN_STOCK, EN_RUPTURE
    }
    public class Produit
    {
        public int Id { get; set;}
        public string Libelle { get; set;}
        public int Quantite { get; set;}
        public bool IsArchived{ get; set;} = true;
        public EtatProduit etat { get; set;} = EtatProduit.EN_STOCK;
        public Categorie categorie { get; set;}
    }
}