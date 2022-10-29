using Eafit.Bike.Repository.ContextDb;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eafit.Bike.Repository.Data
{
    public class FirestoreDbClientFactory : IFirestoreDbClientFactory
    {
        public FirestoreDbClientFactory()
        {

        }
        //public FirestoreDb FirestoreDb()
        //{
        //    var firestoreDb = new FirestoreDbRepository("eafit-arch");
        //}

        public FirestoreDb GetFirestoreDb()
        {
            //throw new NotImplementedException();
            //var firestoreDb = new FirestoreDbRepository("eafit-arch");
            return null;
        }
    }
}
