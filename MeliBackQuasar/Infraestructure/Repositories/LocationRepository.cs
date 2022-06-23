using Google.Cloud.Datastore.V1;
using Infraestructure.Models;

namespace Infraestructure.Repositories;

public class LocationRepository : ILocationRepository
{
    private readonly DatastoreDb db;
    const string Location = "Location";

    public LocationRepository()
    {
        db = new FirebaseContext().fireStoreDb;
    }

    public bool CreateOrUpdate(LocationModel location)
    {
        var entity = GetLocationForName(location);
        if (entity == null)
        {
            return Create(location);
        }
        else
        {
            return Update(entity, location);
        }

    }

    private bool Update(Entity entity, LocationModel location)
    {
        using (DatastoreTransaction transaction = db.BeginTransaction())
        {
            entity[nameof(LocationModel.Distance)] = location.Distance;
            entity[nameof(LocationModel.Message)] = location.Message.ToArray();
            transaction.Update(entity);
            transaction.Commit();
        }

        return true;
    }

    public bool Create(LocationModel location)
    {
        KeyFactory keyFactory = db.CreateKeyFactory(Location);
        Entity entity = new Entity
        {
            Key = keyFactory.CreateIncompleteKey(),
            [nameof(LocationModel.Distance)] = location.Distance,
            [nameof(LocationModel.Message)] = location.Message.ToArray(),
            [nameof(LocationModel.Name)] = location.Name,
        };

        using (DatastoreTransaction transaction = db.BeginTransaction())
        {
            transaction.Insert(entity);
            CommitResponse commitResponse = transaction.Commit();
        }

        return true;
    }

    public Entity? GetLocationForName(LocationModel location)
    {
        Query query = new Query(Location)
        {
            Filter = Filter.Equal(nameof(location.Name), location.Name)
        };

        var entity = db.RunQueryLazily(query).FirstOrDefault();
        return entity;
    }

    public List<LocationModel> GetLocation()
    {
        List<LocationModel> locations = new List<LocationModel>();
        Query query = new Query(Location)
        {
            Order = { { nameof(LocationModel.Name), PropertyOrder.Types.Direction.Descending } }
        };
        foreach (Entity entity in db.RunQueryLazily(query))
        {
            LocationModel loc = new LocationModel
            {
                Distance = (double)entity[nameof(LocationModel.Distance)],

                Name = (string)entity[nameof(LocationModel.Name)]
            };

            loc.Message = new List<string>();
            var array = entity[nameof(LocationModel.Message)].ArrayValue;
            foreach (var item in array.Values)
            {
                loc.Message.Add((string)item);
            }

            locations.Add(loc);
        }

        return locations;
    }
}

