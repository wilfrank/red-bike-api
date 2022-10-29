using Google.Cloud.Firestore;
using Newtonsoft.Json;

namespace Eafit.Bike.Model
{
    public abstract class Entity
    {
        [JsonProperty(PropertyName = "Key")]
        [FirestoreProperty(Name = "Key")]
        public string? Id { get; set; }
    }
}
