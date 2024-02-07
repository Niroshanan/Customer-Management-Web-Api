using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CustomerApi.Data.Entities
{
    public class Address
    {
        [ForeignKey("Customer")]
        public int AddressId { get; set; }
        [JsonPropertyName("number")]
        public int Number { get; set; }
        [JsonPropertyName("street")]
        public string Street { get; set; }
        [JsonPropertyName("city")]
        public string City { get; set; }
        [JsonPropertyName("state")]
        public string State { get; set; }
        [JsonPropertyName("zipcode")]
        public int Zipcode { get; set; }

        [JsonIgnore]
        public virtual Customer Customer { get; set; }
    }
}
