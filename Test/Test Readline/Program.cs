using System;

string searchText = "";

do
{
    Console.WriteLine("Enter some text (q=quit()): ");
    searchText = Console.ReadLine();

    Console.WriteLine($"You entered: {searchText}");
} while (!string.Equals(searchText, "q", StringComparison.OrdinalIgnoreCase));

