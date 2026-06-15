using System.Text.Json.Serialization;

namespace ERP.Models
{
    public class Student
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("age")]
        public int Age { get; set; }
        [JsonPropertyName("email")]
        public string? Email { get; set; }
    }
}
