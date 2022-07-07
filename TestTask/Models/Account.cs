using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace TestTask.Models
{
    public class Account
    {
        public ulong Id;
        [Key]
        public string Name { get; set; }
        public ICollection<Contact> Contacts { get; set; }

        [JsonIgnore]
        public Incident? Incident { get; set; }

        [NotMapped]
        public string IncidentName => Incident == null ? "No connected incedent" : Incident.Name;

        public Account()
        {
            Contacts = new HashSet<Contact>();
        }

    }

}
