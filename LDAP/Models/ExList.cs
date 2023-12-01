using Newtonsoft.Json;
namespace LDAP.Models
{
    public class ExList
    {
        [JsonProperty("Items")]
        public List<ExUsers> ExUsers { get; set; }
    }
}
