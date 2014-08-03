using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;

namespace Seed.Infrastructure.Data
{
    public class SeedDatabaseConfiguration : DbConfiguration
    {
        public SeedDatabaseConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
            SetDefaultConnectionFactory(new LocalDbConnectionFactory("ProjectsV12")); 
        }
    }
}
