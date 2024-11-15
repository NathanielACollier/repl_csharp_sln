
using db = sqliteEFCoreTest.db;
using models = sqliteEFCoreTest.models;

var model = new models.MainWindowModel
{
    rowCount = 0,
    lastRow = new db.Tables.TestTable()
};

var f = nac.Forms.Form.NewForm();
f.DataContext = model;

f.HorizontalGroup(hg =>
    {
        hg.Panel<db.Tables.TestTable>(nameof(model.lastRow), _r =>
        {
            _r.HorizontalGroup(_rHG =>
            {
                _rHG.Text("Last row: ID: ")
                    .TextFor(nameof(db.Tables.TestTable.Id))
                    .Text("; Date: ")
                    .TextFor(nameof(db.Tables.TestTable.Date))
                    .Button("Add", async () =>
                    {
                        await addRow();
                    })
                    .Button("Clear", async () =>
                    {
                        await clearRows();
                    });
            });
        });

    })
.Table<db.Tables.TestTable>(nameof(model.rows))
.Display(onDisplay: async (_f) =>
{
    await addRow();
});



async Task addRow()
{
    using (var conn = new db.TestContext())
    {
        conn.Test.Add(new db.Tables.TestTable
        {
            Message = "Test Entry: " + Guid.NewGuid().ToString("N")
        });
        conn.SaveChanges();

        await invokeAsyncUpdateFormModel(conn);
        
    }
}

async Task clearRows()
{
    using (var conn = new db.TestContext())
    {
        conn.Test.RemoveRange(conn.Test);
        conn.SaveChanges();

        await invokeAsyncUpdateFormModel(conn);
    }
}


async Task invokeAsyncUpdateFormModel(db.TestContext context)
{
    await f.InvokeAsync(async () =>
    {
        model.lastRow = context.Test.OrderByDescending(i => i.Id)
                            .FirstOrDefault()
                        ?? new db.Tables.TestTable();

        // table rows
        model.rows.Clear();
        model.rowCount = 0;
        foreach (var r in context.Test)
        {
            model.rowCount++;
            model.rows.Add(r);
        }
    });
}
