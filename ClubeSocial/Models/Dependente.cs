namespace ClubeSocial.Models
{
    public class Dependente: Pessoa
    {
        public int SocioId { get; set; }
        public Socio Socio { get; set; }
    }
}