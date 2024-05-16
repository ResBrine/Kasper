using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace test
{
    internal class Program
    {
        class Person
        {
            public string name {get; set;}
            public int age { get; set; }
            public Person(int age, string name) {
            this.age = age;
            this.name = name;
            }
        }
        static void Main(string[] args)
        {
            Person tom = new Person(18, "Jon");
            string json = JsonSerializer.Serialize(tom);
            Console.WriteLine(json);
            //Person jon = JsonSerializer.Deserialize<Person>(json);
            //Console.WriteLine(jon.name);
            Console.ReadKey();
        }
    }
}
