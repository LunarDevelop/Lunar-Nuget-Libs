namespace MongoDbService.Models;

/// <summary>
/// Settings for MongoDb and to be used in and around this module
/// </summary>
public class MongoDbSettings
{
    public readonly string ConnString;
    public readonly string DatabaseName;

    /// <param name="connString">use format "mongodb+srv://user:password@url" as this has been tested</param>
    /// <param name="databaseName">name of the database to access</param>
    public MongoDbSettings(string connString, string databaseName)
    {
        ConnString = connString;
        DatabaseName = databaseName;
    }
}