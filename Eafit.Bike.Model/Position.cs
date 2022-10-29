using Google.Cloud.Firestore;
using Newtonsoft.Json;

namespace Eafit.Bike.Model
{
    public class Position
    {
        //[JsonProperty(PropertyName = "Lat")]
        [FirestoreProperty(Name = "Lat")]
        public double Latitud { get; set; }
        //[JsonProperty(PropertyName = "Lon")]
        [FirestoreProperty(Name = "Lon")]
        public double Longitud { get; set; }
    }
}
