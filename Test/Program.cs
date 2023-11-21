using System.Text;
using System.Text.Json;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Person bob = new Person("bob", 16363);
            using (FileStream writer = new FileStream("test.json", FileMode.OpenOrCreate))
            {
                var tom = new Person("Tom", 1524435356);
                JsonSerializer.Serialize<Person>(writer, tom);
                
            }
            using (FileStream writer = new FileStream("test.json", FileMode.OpenOrCreate))
            {
                var tom2 = JsonSerializer.Deserialize<Person>(writer);

                Console.WriteLine(tom2.ToString());
                
            }

        }



    }
}