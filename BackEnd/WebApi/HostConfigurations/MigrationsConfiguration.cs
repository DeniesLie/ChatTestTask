using System.Data.Common;
using DataAccess;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace WebApi.HostConfigurations;

public static class MigrationsConfiguration
{
    public static void ApplyDbMigration(this WebApplication app)
    {
        app.Logger.Log(LogLevel.Information, "Trying to apply migrations...");
        using (var serviceScope = app.Services.CreateScope())
        {
            var dbContext = serviceScope.ServiceProvider.GetService<AppDbContext>();
            while (!dbContext.CanConnect())
            {
                app.Logger.Log(LogLevel.Information, "Waiting for db to deploy...");
                Thread.Sleep(5000);
            }
            dbContext?.Database.Migrate();
            app.Logger.Log(LogLevel.Information, "Migrations are applied");
        }
    }

    private static bool CanConnect(this DbContext dbContext)
    {
        var connection = dbContext.Database.GetDbConnection();
        var masterConnString = connection.ConnectionString.Replace("ChatDb", "master");
        var masterConnection = new SqlConnection(masterConnString);
        try
        {
            masterConnection.Open();
            masterConnection.Close();
        }
        catch (SqlException)
        {
            return false;
        }
        finally
        {
            masterConnection?.Dispose();
        }
        return true;
    }
}