using Google.Cloud.Firestore;
using Newtonsoft.Json;

namespace Eafit.Bike.Model
{
    [FirestoreData]
    public class BikeModel : Entity
    {
        //[JsonProperty(PropertyName = "Color")]
        [FirestoreProperty]
        public string? Color { get; set; }
        //[JsonProperty(PropertyName = "Model")]
        [FirestoreProperty]
        public string? Model { get; set; }
        [FirestoreProperty]
        //[JsonProperty(PropertyName = "IsActived")]
        public bool IsActived { get; set; }

        //[JsonProperty(PropertyName = "Position")]
        //[FirestoreProperty(Name = "Position")]
        //public Position? Position { get; set; }
        //[JsonProperty(PropertyName = "Lat")]
        [FirestoreProperty(Name = "Lat")]
        public double Latitud { get; set; }
        //[JsonProperty(PropertyName = "Lon")]
        [FirestoreProperty(Name = "Lon")]
        public double Longitud { get; set; }
    }
}