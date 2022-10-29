using Google.Cloud.Firestore;

namespace Eafit.Bike.Repository.Data
{
    public interface IFirestoreRepository
    {
        FirestoreDb GetClient(string collectionName);
    }
}
