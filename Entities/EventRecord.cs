using ApiTemplate.Abstracts;

namespace ApiTemplate.Entities
{
    public class EventRecord : EntityBase
    {
        public string DocumentType { get; set; } = string.Empty;
        public string Document { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public string Registration { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int Injured { get; set; }
        public int Dead { get; set; }
        public DateTime EventDate { get; set; }
        public string Photo { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
    }
}
