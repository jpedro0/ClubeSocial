namespace ClubeSocial.Models
{
    public class Dependente: Pessoa
    {
        public int DependenteId { get; set; }
        public int SocioId { get; set; }
        public Socio Socio { get; set; }
    }
}