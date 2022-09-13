using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace commonUtilitiesLib;

public class SystemTextJson
{

    /*
     Originally used code from here: https://stackoverflow.com/questions/1207731/how-can-i-deserialize-json-to-a-simple-dictionarystring-string-in-asp-net
     
     System.Text.Json has alot more abilities, so this code is going to look different
     */
    public static object DeserializeToDictionaryList(string jsonText)
    {
        var jsonElementValueKindsToDeserialize = new[]
        {
            JsonValueKind.Array,
            JsonValueKind.Object
        };

        var root = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(jsonText);

        if (!jsonElementValueKindsToDeserialize.Contains(root.ValueKind))
        {
            return getValue(root); // covers strings, numbers, etc...
        }

        if (root.ValueKind == JsonValueKind.Object)
        {
            var values = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(jsonText);
            var values2 = new Dictionary<string, object>();

            foreach (KeyValuePair<string, JsonElement> d in values)
            {
                if (!jsonElementValueKindsToDeserialize.Contains(d.Value.ValueKind))
                {
                    values2.Add(d.Key, getValue(d.Value));
                }
                else
                {
                    values2.Add(d.Key, DeserializeToDictionaryList(d.Value.GetRawText()));
                }
            }

            return values2;
        }

        if( root.ValueKind == JsonValueKind.Array)
        {
            var values = System.Text.Json.JsonSerializer.Deserialize<List<JsonElement>>(jsonText);
            var values2 = new List<object>();
            foreach (JsonElement je in values)
            {
                if (jsonElementValueKindsToDeserialize.Contains(je.ValueKind))
                {
                    values2.Add(DeserializeToDictionaryList(je.GetRawText()));
                }
                else
                {
                    values2.Add(getValue(je));
                }
            }

            return values2;
        }

        throw new Exception("Could not parse: " + jsonText);
    }

    private static object getValue(JsonElement je)
    {
        if (je.ValueKind == JsonValueKind.Number)
        {
            string numberStr = je.GetRawText();
            if (int.TryParse(numberStr, out int valInt))
            {
                return valInt;
            }else if (long.TryParse(numberStr, out long valLong))
            {
                return valLong;
            }else if (float.TryParse(numberStr, out float valFloat))
            {
                return valFloat;
            }else if (double.TryParse(numberStr, out double valDouble))
            {
                return valDouble;
            }
            else
            {
                throw new Exception($"Could not parse [{numberStr}] as a number");
            }
        }else if (je.ValueKind == JsonValueKind.String)
        {
            return je.GetString();
        }else if (je.ValueKind == JsonValueKind.True)
        {
            return true;
        }else if (je.ValueKind == JsonValueKind.False)
        {
            return false;
        }

        return null;
    }
    
    
    
}