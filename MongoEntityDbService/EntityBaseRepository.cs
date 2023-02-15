using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbService.Models;

namespace MongoDbService;

public class EntityBaseRepository<TEntityBase> : IEntityBaseRepository<TEntityBase>
    where TEntityBase : class, IEntityBase, new()
{
    private readonly IMongoCollection<TEntityBase> _collection;

    public EntityBaseRepository(MongoDbSettings settings)
    {
        var client = new MongoClient(settings.ConnString);
        var database = client.GetDatabase(settings.DatabaseName);
        _collection = database.GetCollection<TEntityBase>(typeof(TEntityBase).Name.Split("Model")[0]);
    }

    /// <summary>
    /// Adds a new entity to the collection
    /// </summary>
    /// <param name="entity">The entity to add</param>
    /// <returns>true or false depending on if it was a success</returns>
    public bool Insert(TEntityBase entity)
    {
        entity.Id = ObjectId.GenerateNewId();
        var task = _collection.InsertOneAsync(entity);
        task.Wait();
        return task.IsCompleted;
    }

    /// <summary>
    /// Update an element of the collection
    /// </summary>
    /// <param name="entity">the updated element</param>
    /// <returns>true if updated/created else false due to server error</returns>
    public bool Update(TEntityBase entity)
    {
        if (entity.Id == ObjectId.Empty)
            return Insert(entity);

        return _collection.ReplaceOne(x => x.Id == entity.Id, entity, new UpdateOptions() { IsUpsert = true })
            .ModifiedCount > 0;
    }

    /// <summary>
    /// Delete an element from the collection
    /// </summary>
    /// <param name="entity">the element to delete</param>
    /// <returns>true if delete else false</returns>
    public bool Delete(TEntityBase entity)
    {
        return _collection.DeleteOne(x => x.Id == entity.Id).DeletedCount > 0;
    }

    /// <summary>
    ///     Get all documents from a collection
    /// </summary>
    /// <returns>A list of documents in a collection</returns>
    public virtual List<TEntityBase> GetAll()
    {
        return _collection.AsQueryable().ToList();
    }

    /// <summary>
    /// Get a single element from a collection using an id
    /// </summary>
    /// <param name="id">the _id number of the element</param>
    /// <returns>the entity which matches the _id number</returns>
    public async Task<TEntityBase?> GetSingle(ObjectId id)
    {
        var obj = await _collection.FindAsync(x => x.Id == id);
        return obj.First();
    }

    /// <summary>
    /// Get the amount of elements in the collection
    /// </summary>
    /// <returns>amount of documents</returns>
    public virtual long Count()
    {
        return _collection.Find(_ => true).CountDocuments();
    }

    /// <summary>
    /// Search for a particular element using a lambda expression
    /// </summary>
    /// <param name="expression">the lambda expression for the result</param>
    /// <returns>the entity to match the expression</returns>
    public IList<TEntityBase> SearchFor(Expression<Func<TEntityBase, bool>> expression)
    {
        return _collection.AsQueryable().Where(expression.Compile()).ToList();
    }
}