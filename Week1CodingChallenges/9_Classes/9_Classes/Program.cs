using System;

namespace _9_ClassesChallenge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Human h_1 = new Human();
            Human h_2 = new Human("John", "Doe");

            Console.WriteLine(h_1.AboutMe());
            Console.WriteLine(h_2.AboutMe());

            Human2 h2_1 = new Human2("James", "Smith", "Brown");
            Human2 h2_2 = new Human2("Bob", "Ross", 60);
            Human2 h2_3 = new Human2("Jane", "Doe", "Blue", 35);

            Console.WriteLine(h2_1.AboutMe2());
            Console.WriteLine(h2_2.AboutMe2());
            Console.WriteLine(h2_3.AboutMe2());

            Human2 h2_4 = new Human2();
            h2_4.Weight = 125;
            Console.WriteLine(h2_4.Weight);
            h2_4.Weight = -10;
            Console.WriteLine(h2_4.Weight);

        }
    }
}
