using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("9_ClassesChallenge.Tests")]
namespace _9_ClassesChallenge
{
    internal class Human2
    {
        private string lastName = "Smyth";
        private string firstName = "Pat";
        private string eyeColor;
        private int? age;

        
        private int weight;
        public int Weight
        {
            get => weight;
            set
            {
                if(value < 0 || value > 400) weight = 0;
                else weight = value;
            }
        }

        public Human2() {}
        public Human2(string firstName, string lastName) {
            this.firstName = firstName;
            this.lastName = lastName;
        }

        public Human2(string firstName, string lastName, string eyeColor) {
            this.firstName = firstName;
            this.lastName = lastName;
            this.eyeColor = eyeColor;
        }

        public Human2(string firstName, string lastName, int age) {
            this.firstName = firstName;
            this.lastName = lastName;
            this.age = age;
        }

        public Human2(string firstName, string lastName, string eyeColor, int age) {
            this.firstName = firstName;
            this.lastName = lastName;
            this.eyeColor = eyeColor;
            this.age = age;
        }

        public string AboutMe() {
            return ($"My name is {firstName} {lastName}.");
        }

        public string AboutMe2() {
            string output = $"My name is {firstName} {lastName}.";
            if (age != null) output += $" My age is {age}.";
            if (eyeColor != null) output += $" My eye color is {eyeColor}.";
            return output;
        } 
    }
}