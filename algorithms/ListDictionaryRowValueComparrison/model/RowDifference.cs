using System.Collections.Generic;

namespace ListDictionaryRowValueComparrison.model;

public class RowDifference
{
    public string Type { get; set; }
    public Dictionary<string, object> MissingRow { get; set; }
    public Dictionary<string, object> RowA { get; set; }
    public Dictionary<string, object> RowB { get; set; }
    public string Key { get; set; }
    public object ValA { get; set; }
    public object ValB { get; set; }
}