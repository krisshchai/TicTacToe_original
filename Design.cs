using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    //<summary>
    //This class includes all the methods related for the game design
    //</summary>
    public static class Design
    {
        //Assigning the values of the game board
        private static string[] values = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        
        //Assigning the values 'X' and 'O'
        private static readonly string player_X = "X";
        private static readonly string player_O = "O";

        //<summary>
        //This method represents which type of Game mode the user wants to play
        //<typeparam> </typeparam>
        //<return type = 'boolen'> Do user wants to play game again </return>  
        //</summary>
        public static bool GameMode()
        {
            bool repeat = false;
            // Asking the mode of the game
            Console.WriteLine("\nChoose any one of the mode\n1> single player \n2> Two players \n3> Exit");
            int choice =  Convert.ToInt32(Console.ReadLine());

            //choicing the mode of game according to user selection
            switch (choice)
            {
                //User vs computer
                case 1:
                    Design.SinglePlayer();
                    //Asking user to play the game again
                    Console.WriteLine("If you want to play again press 'Y'");
                    repeat = (Console.ReadKey().Key == ConsoleKey.Y);
                    break;

                //User 1 vs User 2
                case 2:
                    Design.TwoPlayers();
                    //Asking user to play the game again
                    Console.WriteLine("If you want to play again press 'Y'");
                    repeat = (Console.ReadKey().Key == ConsoleKey.Y);
                    break;

                //Exit from the game
                case 3:
                    Console.WriteLine("Thanks for playing the game !!!");
                    break;
            }

            return repeat;
        }

        //<summary>
        //This method generates gaming board with values and display it
        //<typeparam> </typeparam>
        //<return> </return>  
        //</summary>
        private static void Board() {

            //Generating a gaming board with three rows and three columns 
            for (int i = 0; i < 9; i = i + 3)
            {
                Console.WriteLine(" {0}  | {1}  | {2}  ", values[i], values[i + 1], values[i + 2]);
                
                //Not allowing the loop to create the division line at the bottom of the board
                if (i != 6)
                    Console.WriteLine("------------");
            }

        }

        //<summary>
        //This method is responsible for single player mode.
        //<typeparam> </typeparam>
        //<return> </return>  
        //</summary>
        public static void SinglePlayer()
        {
            int i = 0;

            //Asking the user to choose 'X' or 'O'
            Console.WriteLine("Choose any one: X or O");

            //Validating the input whether it is 'X' or 'O'
            string player = ValidateInput(Console.ReadLine());

            //Initiating the symbol for computer to play
            string computer = (player == player_X) ? player_O : player_X;

            //If player choose 'O', computer will play first; if not player will play first
            if (computer == player_X)
            {
                Console.WriteLine("Computer choice:");

                //Computer will do its move
                ComputerMove(computer);
            }

            bool isGameProcess = true;

            //Making continous game till it's result
            while (isGameProcess)
            {
                i++;

                //Displaying the game board
                Board();

                //Condition for player's game
                if(i == 1)
                {
                    Console.WriteLine("Player: Choose any position from 1 to 9");
                    string position = Console.ReadLine();

                    //Validating the position choosen by the user.
                    while (ValidatePosition(position))
                    {
                        //If the validation went wrong, asking the user to choose the position again
                        Console.WriteLine("Player: Please select another position from 1 to 9");
                        position = Console.ReadLine();
                    }

                    //Replacing the value in the choosen position of the board with player value
                    values[int.Parse(position) - 1] = player;
                }
                else
                {
                    Console.WriteLine("Computer Choice:");
                    
                    //Computer will do its move
                    ComputerMove(computer);
                }

                //Displays the winner of the game
                if (Win())
                {
                    string winner = (i == 1) ? "Player" : "Computer";
                    Console.WriteLine("{0} is the winner !!!", winner);
                    isGameProcess = false;
                }

                //Displays that the game is draw
                else if (Tie())
                {
                    Console.WriteLine("The game is draw !!!!!");
                    isGameProcess = false;
                }

                //for every two cycles value of i will become '0'
                i = (i == 2) ? 0 : i;
            }
            
        }

        //<summary>
        //This method is responsible for dual player mode.
        //<typeparam> </typeparam>
        //<return> </return>  
        //</summary>
        public static void TwoPlayers()
        {
            //Displays the board
            Board();

            //Indicating who will be 1st player and 2nd player
            Console.WriteLine("Player 1: X ; Player 2: O");
            int i = 0;

            bool isGameProcess = true;

            //Making continous game till it's result
            while (isGameProcess)
            {
                i++;

                //Asking each player to choose a position
                Console.WriteLine("Player {0}: Choose any position from 1 to 9", i);
                string position = Console.ReadLine();

                //Validating the position
                while (ValidatePosition(position))
                {
                    Console.WriteLine("Player {0}: Please select another position from 1 to 9", i);
                    position = Console.ReadLine();
                }

                //Replacing the value of the choosen position with the respective player value
                values[int.Parse(position) - 1] = (i == 1) ? player_X : player_O;

                //Displaying the board
                Board();

                //Checking the game is over and displaying which player won it 
                if (Win())
                {
                    Console.WriteLine("Player {0} is the winner !!!", i);
                    isGameProcess = false;
                }

                //Checking the game is draw and displaying it
                else if(Tie())
                {
                    Console.WriteLine("The game is draw !!!!!");
                    isGameProcess = false;
                }

                //for every two cycles value of i will become '0'
                i = (i == 2) ? 0 : i;
            }

        }

        //<summary>
        //This method checks all the available positions and pick randoms among one 
        //<typeparam type = 'string'> com </typeparam>
        //<return type = 'boolen'> true / false </return>  
        //</summary>
        private static void ComputerMove(string com)
        {
            //Initiating list for available positions
            List<int> availableCorners = new List<int>();
            List<int> availableEdges = new List<int>();
            
            bool isAvailableCenter = false;

            for(int i = 0; i< values.Length; i++)
            {
                //Checking the available positions
                if (values[i] != player_X && values[i] != player_O) {
                    
                    //Adding all the available corner positions in the list
                    if ( i%2 == 0 && i != 4)
                    {
                        availableCorners.Add(i);
                    }
                    //Adding all the available edge positions in the list
                    else if (i%2 != 0)
                    {
                        availableEdges.Add(i);
                    }
                    //checking whether the center position is available
                    else
                    {
                        isAvailableCenter = true;
                    }
                
                }
            }

            Random random= new Random();

            //checking the availablity and selecting a position randomly
            if(availableCorners.Count != 0 )
            {
                int r = random.Next( availableCorners.Count );
                values[availableCorners[r]] = com;
            }

            //checking the availablity and selecting the center position
            else if (isAvailableCenter)
            {
                values[4] = com;
            }

            //checking the availablity and selecting a position randomly
            else
            {
                int r = random.Next(availableEdges.Count);
                values[availableEdges[r]] = com;
            }
        }

        //<summary>
        //This method checks whether the game is over and win 
        //<typeparam> </typeparam>
        //<return type = 'boolen'> true / false </return>  
        //</summary>
        private static bool Win()
        {
            //checks the values row wise
            for(int i = 0; i < 9; i = i+3)
            {
                if (values[i] == values[i + 1] && values[i] == values[i + 2])
                    return true;
            }

            //checks the values column wise
            for(int i = 0; i < 3; i++)
            {
                if(values[i] == values[i + 3] && values[i] == values[i + 6])
                    return true;
            }

            //checks the values in diagonals
            if ((values[0] == values[4] && values[0] == values[8]) || (values[2] == values[4] && values[2] == values[6]))
                return true;

            return false;
        }

        //<summary>
        //This method checks whether the game is tie or not 
        //<typeparam> </typeparam>
        //<return type = 'boolen'> true / false </return>  
        //</summary>
        private static bool Tie()
        {
            //checks all the positions and returns true if all of them are filled
            return (values[0] != "1" && values[1] != "2" && values[2] != "3"
                && values[3] != "4" && values[4] != "5" && values[5] != "6"
                && values[6] != "7" && values[7] != "8" && values[8] != "9");
        }

        //<summary>
        //This method validates the position choosen by the user and return ture or false
        //<typeparam type = 'string'> position </typeparam>
        //<return type = 'boolen'> true / false </return>  
        //</summary>
        private static bool ValidatePosition(string position)
        {
            //Checking the position has correct value
            if (!String.IsNullOrEmpty(position) && Array.Exists(values, i => i == position))
            {
                int pos = int.Parse(position);

                //checking the position is occupied by 'X' or 'O'
                return (values[pos - 1] == player_X || values[pos - 1] == player_O);
            }

            return true;
            
        }

        //<summary>
        //This method will validate the input given by user and return the input value in upper case
        //<typeparam type = 'string'> input </typeparam>
        //<return type = 'string'> input value </return>  
        //</summary>
        private static string ValidateInput(string input)
        {
            bool isValidated = true;

            //Asks the user in loop to enter any one value - 'X' or 'O'
            while(isValidated)
            {
                //checks the value is 'X' or 'O'
                if(String.IsNullOrEmpty(input) || input.ToUpper() != player_X && input.ToUpper() != player_O)
                {
                    Console.WriteLine("Please enter any one of value: 'X' or 'O'");
                    input = Console.ReadLine();
                }

                //If the value matches, the loop ends
                else
                    isValidated = false;
            }

            //return the user input value in capital case
            return input.ToUpper();
        }
    }
}
