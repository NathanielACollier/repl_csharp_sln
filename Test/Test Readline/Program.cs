using System;

namespace Test_Readline
{
    class Program
    {
        static void Main(string[] args)
        {
            string searchText = "";

            do{
                Console.WriteLine("Enter some text (q=quit()): ");
                searchText = Console.ReadLine();

                Console.WriteLine($"You entered: {searchText}");
            }while( !string.Equals(searchText, "q", StringComparison.OrdinalIgnoreCase));
        }
    }
}
