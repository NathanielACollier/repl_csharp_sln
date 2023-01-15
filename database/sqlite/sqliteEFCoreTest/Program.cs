using sqliteEFCoreTest.db.Tables;
using db = sqliteEFCoreTest.db;

using (var conn = new db.TestContext())
{
    conn.Test.Add(new TestTable
    {
        Message = "Test Entry: " + Guid.NewGuid().ToString("N")
    });
    conn.SaveChanges();
    
    Console.WriteLine($"Row count: {conn.Test.Count()}");

    var lastRow = conn.Test.OrderByDescending(i => i.Id)
        .First();
    
    Console.WriteLine($@"Last row:
ID:      {lastRow.Id}
Date:    {lastRow.Date}
Message: {lastRow.Message}");
}
