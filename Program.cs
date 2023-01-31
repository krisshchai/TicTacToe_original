
using TicTacToe;

internal class Program
{
    private static void Main(string[] args)
    {
        //Displaying welcome message
        Console.WriteLine("!!! Welcome to Tic Tac Toe !!!!");

        bool repeat = true;

        //Continuing the loop till the user wants to exit
        while (repeat)
        {
            repeat = Design.GameMode();
        }
        
    }
}