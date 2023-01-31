										TIC TAC TOE - Coding Challange

OverView:

	This is a straightforward game written in C# and is a console application. Two players, with different symbols ('X' and 'O'), 
Players compete against one another in a 3 x 3 grid. The motive for winning this game is to get their three symbols in a row. 
Players will take turns placing their symbols on the grid in a horizontal, vertical, or diagonal pattern to achieve victory. 
The one who completes the task first is declared the winner of the game. If both players fail to get their three symbols in a row 
and all the places are filled with the symbols, then the game will be declared a tie.

File names:

1> Program.cs  -->  It is the main class and contains the main method which contains the code that sets up and start the application.
					When application is executed, first this method will executed first.

2> Design.cs   -->  It is a static class which contains all the methods with logics to run the application.

GamePlay:

1> The game will prompt a welcome message and asks the user to choose the mode of the game. i.e single player or two players
2> If user selects single player, then it asks to choose the symbol amoung 'X' and 'O'. If the user selects 'X', then he will be 
playing first. If he / she choose 'O', then computer will have its first move.
3> User will be asked to select the position in the grid to place the symbol. If wrong position is entered, it will ask again to
enter. Once the user enter the position, computer will select one of the available position and display the board in command prompt.
4> If user choose two players mode, then a message is prompted indicating player 1 symbol will be 'X' and player 2 symbol will be 'O'.
5> Both players will be asked to choose their position in the grid sequentially. The one who arrange his / her three symbols in a row,
will be declared as winner. If no one wins, then the game will be declared as tie.
6> After declaring the status of the game, application will ask the user to play again or not. If the user wants to play again, he/she
has to press 'Y' key to continue.



