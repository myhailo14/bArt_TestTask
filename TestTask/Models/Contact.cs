using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Text.Json.Serialization;
namespace TestTask.Models
{

    public class Contact
    {
        [Key]
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [JsonIgnore]
        public Account? Account { get; set; }

        [NotMapped]
        public string AccountName => Account == null ? "No connected account" : Account.Name;
       
    }

}
