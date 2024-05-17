using ApiTemplate.Abstracts;

namespace ApiTemplate.Entities
{
    public class Voto : EntityBase
    {
        public string Cedula { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string CandidatoVotado { get; set; } = string.Empty;
    }
}
