using Microsoft.EntityFrameworkCore;

namespace sqliteEFCoreTest.db;

/*
 see: https://entityframeworkcore.com/providers-sqlite

+ When you first get the context and a table model created then run this command to create the database
```powershell
Add-Migration Initial
Update-Database
```
 */

public class TestContext : DbContext
{
    public DbSet<Tables.TestTable> Test { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=TestDB.db;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tables.TestTable>().ToTable("Test");

        modelBuilder.Entity<Tables.TestTable>()
            .Property(i => i.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Tables.TestTable>()
            .Property(i => i.Date)
            .HasDefaultValueSql("getdate()");
    }
}