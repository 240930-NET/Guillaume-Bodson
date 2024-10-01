using System.ComponentModel;
using System.Reflection.Metadata;

namespace RockPaperScissors;

class Program
{
    static int computerScore = 0;
    static int playerScore = 0;

    //Generates a random computer play (Rock, Paper, or Scissors)
    public static string SelectComputerPlay() {
        string[] computerPlays = ["rock", "paper", "scissors"];

        Random randomGen = new();
        int randNum = randomGen.Next(computerPlays.Length);

        return computerPlays[randNum];
    }

    //Selects the winner given the user play and the computer play
    public static void SelectWinner(string userPlay, string computerPlay) {
        if(userPlay.Equals("rock", StringComparison.CurrentCultureIgnoreCase)) {
            if(computerPlay == "scissors") {
                displayResult("win", computerPlay);
            }
            else if(computerPlay == "paper") {
                displayResult("lose", computerPlay);
            }
            else {
                displayResult("tie", computerPlay);
            }
        }
        else if(userPlay.Equals("paper", StringComparison.CurrentCultureIgnoreCase)) {
            if(computerPlay == "rock") {
                displayResult("win", computerPlay);
            }
            else if(computerPlay == "scissors") {
                displayResult("lose", computerPlay);
            }
            else {
                displayResult("tie", computerPlay);
            }
        }
        else if(userPlay.Equals("scissors", StringComparison.CurrentCultureIgnoreCase)) {
            if(computerPlay == "paper") {
                displayResult("win", computerPlay);
            }
            else if(computerPlay == "rock") {
                displayResult("lose", computerPlay);
            }
            else {
                displayResult("tie", computerPlay);
            }
        }
        else {
            Console.WriteLine("User input not recognized.");
        }

    }

    //Displays the results and updates the score
    public static void displayResult(string result, string computerPlay) {
        if (result.Equals("win")) {
            playerScore++;
            Console.WriteLine($"Computer plays {computerPlay}; YOU WIN!; Current Score: {playerScore}-{computerScore}");
        }
        else if (result.Equals("lose")) {
            computerScore++;
            Console.WriteLine($"Computer plays {computerPlay}; YOU LOSE!; Current Score: {playerScore}-{computerScore}");
        }
        else {
            Console.WriteLine($"Computer plays {computerPlay}; TIE!; Current Score: {playerScore}-{computerScore}");
        }
    }

    static void Main(string[] args)
    {   
        Console.WriteLine("Welcome to Rock Paper Scissors!");
        Console.WriteLine("Please choose one of the following options or type in Exit to stop playing");
        Console.WriteLine(" - Rock");
        Console.WriteLine(" - Paper");
        Console.WriteLine(" - Scissors");

        while (true) {
            string? userInput = Console.ReadLine();
            if (String.IsNullOrEmpty(userInput)) {
                Console.WriteLine("Please enter a valid keyword");
            }
            else if (userInput.Trim().Equals("exit", StringComparison.CurrentCultureIgnoreCase)) {
                Console.WriteLine($"Final Score: {playerScore}-{computerScore}");
                Console.WriteLine("Goodbye!");
                break;
            }
            else {
                SelectWinner(userInput.Trim(), SelectComputerPlay());
            }
        }
    }   
}
