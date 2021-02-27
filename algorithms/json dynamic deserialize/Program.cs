using System;
using Newtonsoft.Json;

namespace json_dynamic_deserialize
{
    class Program
    {
        static void Main(string[] args)
        {
            // practicing stuff found at this place: https://stackoverflow.com/questions/3142495/deserialize-json-into-c-sharp-dynamic-object
            dynamic stuff = JsonConvert.DeserializeObject("{ 'Name': 'Jon Smith', 'Address': { 'City': 'New York', 'State': 'NY' }, 'Age': 42 }");

            string name = stuff.Name;
            string address = stuff.Address.City;
        }
    }
}
