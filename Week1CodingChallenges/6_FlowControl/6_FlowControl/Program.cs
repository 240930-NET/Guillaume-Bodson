﻿



using System;

namespace _6_FlowControl
{
    public class Program
    {
        private static string _username;
        private static string _password;
        static void Main(string[] args)
        {
        }

        /// <summary>
        /// This method gets a valid temperature between -40 asnd 135 inclusive from the user
        /// and returns the valid int. 
        /// </summary>
        /// <returns></returns>
        public static int GetValidTemperature()
        {
            string? userInput = Console.ReadLine();
            int number;
            if(Int32.TryParse(userInput, out number))
            {
                if(number > -40 && number < 135) return number;
            }  
            return 0; 
        }

        /// <summary>
        /// This method has one int parameter
        /// It prints outdoor activity advice and temperature opinion to the console 
        /// based on 20 degree increments starting at -20 and ending at 135 
        /// n < -20, Console.Write("hella cold");
        /// -20 <= n < 0, Console.Write("pretty cold");
        ///  0 <= n < 20, Console.Write("cold");
        /// 20 <= n < 40, Console.Write("thawed out");
        /// 40 <= n < 60, Console.Write("feels like Autumn");
        /// 60 <= n < 80, Console.Write("perfect outdoor workout temperature");
        /// 80 <= n < 90, Console.Write("niiice");
        /// 90 <= n < 100, Console.Write("hella hot");
        /// 100 <= n < 135, Console.Write("hottest");
        /// </summary>
        /// <param name="temp"></param>
        public static void GiveActivityAdvice(int temp)
        {
            if (temp < -20) Console.Write("hella cold");
            else if (temp < 0) Console.Write("pretty cold");
            else if (temp < 20) Console.Write("cold");
            else if (temp < 40) Console.Write("thawed out");
            else if (temp < 60) Console.Write("feels like Autumn");
            else if (temp < 80) Console.Write("perfect outdoor workout temperature");
            else if (temp < 90) Console.Write("niiice");
            else if (temp < 100) Console.Write("hella hot");
            else if (temp < 135) Console.Write("hottest");
        }

        /// <summary>
        /// This method gets a username and password from the user
        /// and stores that data in the global variables of the 
        /// names in the method.
        /// </summary>
        public static void Register()
        {
            string? _username = Console.ReadLine();
            //string? _password = Console.ReadLine();
            Console.WriteLine("saved");
        }

        /// <summary>
        /// This method gets username and password from the user and
        /// compares them with the username and password names provided in Register().
        /// If the password and username match, the method returns true. 
        /// If they do not match, the user is reprompted for the username and password
        /// until the exact matches are inputted.
        /// </summary>
        /// <returns></returns>
        public static bool Login()
        {
            string? username = Console.ReadLine();
            //string? password = Console.ReadLine();

            return _username == username;
        }

        /// <summary>
        /// This method has one int parameter.
        /// It checks if the int is <=42, Console.WriteLine($"{temp} is too cold!");
        /// between 43 and 78 inclusive, Console.WriteLine($"{temp} is an ok temperature");
        /// or > 78, Console.WriteLine($"{temp} is too hot!");
        /// For each temperature range, a different advice is given. 
        /// </summary>
        /// <param name="temp"></param>
        public static void GetTemperatureTernary(int temp)
        {
            if (temp <= 42) Console.WriteLine($"{temp} is too cold!");
            else if (temp <= 78) Console.WriteLine($"{temp} is an ok temperature");
            else Console.WriteLine($"{temp} is too hot!");
        }
    }//EoP
}//EoN
