using System.ComponentModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbService.Models;

public class EntityBase : IEntityBase
{

    /// <summary>
    /// the _id element of the elements
    /// </summary>
    [BsonId]
    public ObjectId Id { get; set; }
}