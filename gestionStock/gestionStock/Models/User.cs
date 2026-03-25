namespace gestionStock.Models
{
    public enum RoleType
    {
        ADMIN, EMPLOYE
    }
    public class User
    {
        public int Id { get; set;}
        public string Email { get; set;}
        public string Password { get; set;}
        public bool IsArchived{ get; set;} = true;
        public RoleType role { get; set; }
    }
}