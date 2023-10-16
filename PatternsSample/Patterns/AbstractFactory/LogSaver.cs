using System.Data.Common;

namespace PatternsSample.Patterns.AbstractFactory;

public class LogSaver
{
    private readonly DbProviderFactory _factory;

    public LogSaver(DbProviderFactory factory)
    {
        _factory = factory;
    }

    public void Save(IEnumerable<LogEntry> logEntries)
    {
        using var dbConnection = _factory.CreateConnection();
        using var dbCommand = _factory.CreateCommand();

        SetCommandArgs(logEntries);
        SetConnectionString(dbConnection);

        dbCommand.ExecuteNonQuery();
    }
    
    private void SetConnectionString(DbConnection? dbConnection)
    {
        
    }

    private void SetCommandArgs(IEnumerable<LogEntry> logEntries)
    {
        
    }
}