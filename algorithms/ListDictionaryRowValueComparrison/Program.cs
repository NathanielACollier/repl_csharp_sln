using System;
using System.Collections.Generic;
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
        { "Prop3", now },
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
        { "Prop4", 33.6666 }
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





