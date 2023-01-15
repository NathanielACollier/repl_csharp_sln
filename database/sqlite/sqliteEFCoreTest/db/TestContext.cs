using Microsoft.EntityFrameworkCore;

namespace sqliteEFCoreTest.db;

/*
 see: https://entityframeworkcore.com/providers-sqlite

+ When you first get the context and a table model created then run this command to create the database
```powershell
Add-Migration Initial
Update-Database
```
+ If you are not on windows and need to run the ef core migrations there is a dotnet tool
    + see: https://stackoverflow.com/questions/45382536/how-to-enable-migrations-in-visual-studio-for-mac
+ Install the dotnet ef global tool
```powershell
dotnet tool install --global dotnet-ef
```
+ Now you can run `dotnet ef` in the project folder and it will show you the standard help info
+ Then once that works right you can run these commands to setup the database
```powershell
dotnet ef migrations add initial
dotnet ef database update
```
+ Now when you change something you can add another migration like `dotnet ef migrations add v2`.  Then run the `database update`
 */

public class TestContext : DbContext
{
    public DbSet<Tables.TestTable> Test { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string dbFilePath = System.IO.Path.Combine(commonUtilitiesLib.Directory.getRootDirectory(), "TestDB.db");
        optionsBuilder.UseSqlite($"Data Source={dbFilePath};");
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