using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace TestTask.Models
{
    public class Incident
    {
        public ulong Id;
        [Key]
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Account> Accounts { get; set; }

        public Incident()
        {
            Accounts = new HashSet<Account>();
        }

    }

}
