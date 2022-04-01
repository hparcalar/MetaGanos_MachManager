using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MachManager.Context;
using System;
public static class SchemaFactory {
    public static string ConnectionString { get; set; } = "";
    public static MetaGanosSchema CreateContext() {
        var optionsBuilder = new DbContextOptionsBuilder();
        optionsBuilder.UseNpgsql(ConnectionString);
        MetaGanosSchema nodeContext = new MetaGanosSchema(optionsBuilder.Options);
        return nodeContext;
    }

    public static void ApplyMigrations(){
        var nodeContext = CreateContext();
        if (nodeContext != null){
            try
            {
                nodeContext.Database.Migrate();
                nodeContext.Dispose();

                Console.WriteLine("Migration Succeeded");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Migration Error: " + ex.Message);
            }
        }
    }
}