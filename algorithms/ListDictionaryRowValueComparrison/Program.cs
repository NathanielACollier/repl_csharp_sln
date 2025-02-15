using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using model=ListDictionaryRowValueComparrison.model;

var now = DateTime.Now;

var listA = new List<Dictionary<string, object>>
{
    new Dictionary<string, object>
    {
        { "Prop1", 33 },
        { "Prop2", "Hello" },
        { "Prop3", now },
        { "Prop4", 33.6666 }
    },
    new Dictionary<string, object>
    {
        { "Prop1", 34 },
        { "Prop2", "Hello" },
        { "Prop3", now },
        { "Prop4", 33.6666 }
    },
    new Dictionary<string, object>
    {
        { "Prop1", 35 },
        { "Prop2", "Hello" },
        { "Prop3", now.AddDays(3) },
        { "Prop4", 33.6666 }
    }
};

var listB = new List<Dictionary<string, object>>
{
    new Dictionary<string, object>
    {
        { "Prop1", 33 },
        { "Prop2", "Hello22" },
        { "Prop3", now },
        { "Prop4", 33.6666 }
    },
    new Dictionary<string, object>
    {
        { "Prop1", 34 },
        { "Prop2", "Hello" },
        { "Prop3", now },
        { "Prop4", 33.6676 }
    },
    new Dictionary<string, object>
    {
        { "Prop1", 35 },
        { "Prop2", "Hello" },
        { "Prop3", now },
        { "Prop4", 33.6666 }
    }
};


var differences = GetDifferences(listA, listB, ["Prop1"]);

Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(differences, options: new JsonSerializerOptions
{
    WriteIndented = true
}));

List<model.RowDifference> GetDifferences(List<Dictionary<string, object>> listA,
    List<Dictionary<string, object>> listB,
    List<string> primaryKeyList)
{
    var commonKeysOtherThanPrimaryList = (listA?.FirstOrDefault()?.Keys.ToList() ?? new List<string>())
        .Union(
            (listB?.FirstOrDefault()?.Keys.ToList() ?? new List<string>())
        )
        .Where(k => !primaryKeyList.Contains(k))
        .ToList();

    var missingRowsListA = from rB in listB
        let rA = GetMatchingRowFromKey(targetRow: rB, haystackList: listA, rowKeys: primaryKeyList)
        where rA == null
        select new model.RowDifference
        {
            Type="Missing Row B",
            MissingRow = rB
        };

    var missingRowsListB = from rA in listA
        let rB = GetMatchingRowFromKey(targetRow: rA, haystackList: listB, rowKeys: primaryKeyList)
        where rB == null
        select new model.RowDifference
        {
            Type = "Missing Row A",
            MissingRow = rA
        };

    var rowDifferences = from rA in listA
        let rB = GetMatchingRowFromKey(targetRow: rA, haystackList: listB, rowKeys: primaryKeyList)
        where rB != null
        let rowDiff = GetRowDifferences(rA: rA, rB: rB, keysToCompare: commonKeysOtherThanPrimaryList)
        where rowDiff.Any()
        from diff in rowDiff
        select new model.RowDifference
        {
            Type = "Column Difference",
            RowA = rA,
            RowB = rB,
            Key = diff.Key,
            ValA = diff.ValA,
            ValB = diff.ValB
        };

    return missingRowsListA.Union(
            missingRowsListB
        ).Union(rowDifferences)
        .ToList();
}


Dictionary<string,object> GetMatchingRowFromKey(Dictionary<string, object> targetRow, 
                                List<Dictionary<string, object>> haystackList, 
                                List<string> rowKeys)
{
    foreach (var sourceRow in haystackList)
    {
        if (AreRowsEqualOnKeys(rowA: targetRow, rowB: sourceRow, keyList: rowKeys))
        {
            return sourceRow;
        }
    }

    return null;
}


bool AreRowsEqualOnKeys(Dictionary<string, object> rowA,
    Dictionary<string, object> rowB,
    List<string> keyList)
{
    bool rowsEqual = true;

    foreach (string key in keyList)
    {
        if (rowA.TryGetValue(key, out object valA) &&
            rowB.TryGetValue(key, out object valB))
        {
            if (!object.Equals(valA, valB))
            {
                rowsEqual = false;
                break; // found a key doesn't equal break out
            }
        }
    }
    
    return rowsEqual;
}



List<model.RowColumnDifference> GetRowDifferences(Dictionary<string, object> rA, Dictionary<string, object> rB, List<string> keysToCompare)
{
    var diffList = new List<model.RowColumnDifference>();
    
    foreach (string key in keysToCompare)
    {
        if (!AreRowsEqualOnKeys(rowA: rA, rowB: rB, keyList: [key]))
        {
            rA.TryGetValue(key, out object valA);
            rB.TryGetValue(key, out object valB);
            
            // column wasn't equal between the two rows
            diffList.Add(new model.RowColumnDifference
            {
                Key = key,
                ValA = valA,
                ValB = valB
            });
        }
    }

    return diffList;
}





