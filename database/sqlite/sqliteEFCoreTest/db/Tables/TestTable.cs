namespace sqliteEFCoreTest.db.Tables;

public class TestTable : nac.ViewModelBase.ViewModelBase
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Message { get; set; }
}