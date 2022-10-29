using Google.Cloud.Firestore;

namespace Eafit.Bike.Repository.Data
{
    public interface IFirestoreDbClientFactory
    {
        FirestoreDb GetFirestoreDb();
    }
}
