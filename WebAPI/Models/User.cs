using System.Text.Json.Serialization;

namespace WebAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        [JsonIgnore]public string Password { get; set; }

    }
}
