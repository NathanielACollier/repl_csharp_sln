using sqliteEFCoreTest.db.Tables;
using db = sqliteEFCoreTest.db;

using (var conn = new db.TestContext())
{
    conn.Test.Add(new TestTable
    {
        Message = "Test Entry: " + Guid.NewGuid().ToString("N")
    });
    conn.SaveChanges();

    var f = nac.Forms.Form.NewForm();
    f.Model["rowCount"] = conn.Test.Count();
    f.Model["rows"] = conn.Test.ToList();

    var lastRow = conn.Test.OrderByDescending(i => i.Id)
        .First();
    
    f.HorizontalGroup(hg=> {
        hg.Text($"Last row: ID: {lastRow.Id}; Date: {lastRow.Date}");
    })
    .Table<db.Tables.TestTable>(itemsModelFieldName: "rows");

    f.Display();
}
