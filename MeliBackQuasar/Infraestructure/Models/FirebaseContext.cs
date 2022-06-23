using Google.Cloud.Datastore.V1;
namespace Infraestructure.Models;

public class FirebaseContext
{
    const string PROJECTID = "meliappback";

    public DatastoreDb fireStoreDb = DatastoreDb.Create(PROJECTID);

    public FirebaseContext()
    {
        fireStoreDb = DatastoreDb.Create(PROJECTID);
    }
}

