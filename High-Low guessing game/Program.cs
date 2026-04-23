using System;
using System.Runtime.InteropServices;

class Utils
{
    public (string, int) GetReadLine(string outputString, bool intNeeded)
    {
        Console.WriteLine(outputString);
        string input = "";
        int intResult = 0;
        bool inputCorrect = false;
        while (inputCorrect == false)
        {
            input = Console.ReadLine();

            bool isNull = (input == "" || input == null);

            if (intNeeded == true)
            {
                inputCorrect = (!isNull) && int.TryParse(input, out intResult);
            }
            else
            {
                inputCorrect = !isNull;
            }
        }

        return (input, intResult);
    }

}

class RandomNumber
{
    public int Number { get; set; }

    public string TryGuess(int guessNumber)
    {
        if (Number > guessNumber)
        {
            return "Higher";
        }
        else if (Number < guessNumber)
        {
            return "Lower";
        }
        else
        {
            return "Correct";
        }
    }
    
    public RandomNumber(double Max)
    {
        Number = new Random().Next(1, Convert.ToInt32(Max));
    }
}

class GuessingGame
{
    Utils utils = new Utils();

    public RandomNumber randomNum;
    private int turns;

    private string name;
    private int difficulty;
    private double maxNum;

    public int Turns
    {
        get { return turns; }
    }

    public void InitGame()
    {
        (name, int num) = utils.GetReadLine("What is your name?", false);

        (string txt, difficulty) = utils.GetReadLine($"Hello {name}! What level of difficulty would you like? (1 - 8)", true);
        difficulty = Math.Clamp(difficulty, 1, 8);
        turns = difficulty * 5;

        maxNum = Math.Pow(10, difficulty);

        randomNum = new RandomNumber(maxNum);
    }

    public bool StartTurn()
    {
        (string txt, int guess) = utils.GetReadLine($"What is your guess, {name}? (1 - {maxNum})", true);

        string guessOutput = randomNum.TryGuess(guess);

        switch (guessOutput)
        {
            case ("Higher"):
                Console.WriteLine("Your guess is too low...");
                return false;

            case ("Lower"):
                Console.WriteLine("Your guess is too high...");
                return false;

            case ("Correct"):
                Console.WriteLine($"Your guess is... Correct! Good job {name}!");
                return true;
        }

        return false;
    }
}

class Program
{
    static void Main()
    {
        Utils utils = new Utils();

        bool playing = true;

        while (playing)
        {
            GuessingGame mainGame = new GuessingGame();

            mainGame.InitGame();

            utils.GetReadLine("Enter any key when you're ready to start...", false);

            for (int turn = 0; turn <= mainGame.Turns; turn++)
            {
                Console.WriteLine($"TURN {turn}/{mainGame.Turns}");

                bool correct = mainGame.StartTurn();

                if (correct == true)
                {
                    Console.WriteLine($"You got it in {turn} guesses.");
                    break;
                }
                else if (turn == mainGame.Turns)
                {
                    Console.WriteLine($"Oh well, you couldn't get it. The answer was {mainGame.randomNum.Number}");
                }
            }

            (string check, int nil) = utils.GetReadLine("Do you want to play again?! (Y/N)", false);
            playing = (check.ToLower() == "y");
        }
    }
}