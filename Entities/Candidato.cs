using ApiTemplate.Abstracts;

namespace ApiTemplate.Entities
{
    public class Candidato : EntityBase
    {
        public string Nombre { get; set; } = string.Empty;
        public string Partido { get; set; } = string.Empty;
        public int CantidadVotos { get; set; }
    }
}
