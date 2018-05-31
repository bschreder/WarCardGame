using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarCardGame.CardGame
{

    //  Rules:  https://en.wikipedia.org/wiki/War_(card_game)
    public class Program
    {
        private const int _numberOfShuffles = 7;

        public static void Main(string[] args)
        {
            ConsoleKeyInfo consoleKey;
            do
            {
                IBattleOutput battleOutput = new BattleOutput();
                RunGame(battleOutput);

                Console.WriteLine("Hit 'a' to play again or any other key to finish");
                consoleKey = Console.ReadKey();
                Console.WriteLine();
            } while (consoleKey.Key == ConsoleKey.A);

        }

        /// <summary>
        /// Primary loop that runs the War card game
        /// </summary>
        /// <param name="battleOutput">User interface output class</param>
        /// <returns>GamesResults object with winning and losing player</returns>
        public static GameResults RunGame(IBattleOutput battleOutput)
        {
            Game game = new Game(battleOutput);
            CardPile deck = new CardPile();
            game.CreateDeck(deck);
            game.Shuffle(deck, _numberOfShuffles);

            Player player1 = new Player();
            Player player2 = new Player();
            game.DealCards(deck, player1, player2);

            if (player1.NumberOfCards == 0 || player2.NumberOfCards == 0)
            {
                Console.WriteLine("Error:  Something happend during the creation / shuffling / dealing of the game");
                return null;
            }

            return game.PlayGame(player1, player2);
        }
    }
}
