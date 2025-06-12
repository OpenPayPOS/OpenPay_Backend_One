namespace OpenPay.Data.Configuration;

public class DatabaseOptions
{
    public string DefaultConnection { get; set; } = string.Empty;
    
    public DatabaseOptions(string host, string port, string user, string password, string dbName)
    {
        DefaultConnection = $"Host={host};Port={port};Database={dbName};Username={user};Password={password};Include Error Detail=true";
    }
}
