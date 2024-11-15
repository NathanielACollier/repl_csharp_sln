using System.Collections.ObjectModel;

namespace sqliteEFCoreTest.models;

public class MainWindowModel : nac.ViewModelBase.ViewModelBase
{

    public int rowCount
    {
        get { return GetValue(() => rowCount); }
        set { SetValue(() => rowCount, value);}
    }

    public ObservableCollection<db.Tables.TestTable> rows
    {
        get { return GetValue(() => rows); }
    }

    public db.Tables.TestTable lastRow
    {
        get { return GetValue(() => lastRow); }
        set { SetValue(() => lastRow, value); }
    }
    
}