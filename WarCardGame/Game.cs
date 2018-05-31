using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarCardGame.CardGame
{
    /// <summary>
    /// Class that contains the game play
    /// </summary>
    public class Game
    {
        private IBattleOutput _battleOutput;

        /// <summary>
        /// Constructor used to instantiated the game play object
        /// </summary>
        /// <param name="battleOutput">Interface to the game's UI class</param>
        public Game(IBattleOutput battleOutput)
        {
            _battleOutput = battleOutput;
        }

        /// <summary>
        /// Creates (hydrates) a full card deck based on Card suits and rank enum
        /// </summary>
        /// <param name="cs">Object to be hydrated</param>
        public void CreateDeck(CardPile cs)
        {
            foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
                foreach (CardRank rank in Enum.GetValues(typeof(CardRank)))
                    cs.AddCard(new Card(suit, rank));
        }

        /// <summary>
        /// Shuffles cards
        /// </summary>
        /// <param name="cs">card pile to be shuffled</param>
        /// <param name="numTimes">number of times the card pile is to be shuffled</param>
        public void Shuffle(CardPile cs, int numTimes)
        {
            if (cs.NumberOfCards == 0)
            {
                Console.WriteLine("A Deck must be created before it can be shuffled");
                return;
            }

            cs.Shuffle(numTimes);
        }

        /// <summary>
        /// Deal a card pile to the players
        /// </summary>
        /// <param name="cs">card pile to be dealt</param>
        /// <param name="player1">player 1</param>
        /// <param name="player2">player 2</param>
        public void DealCards(CardPile cs, Player player1, Player player2)
        {
            while (cs.NumberOfCards != 0)
            {
                player1.AddToHand(cs.RemoveNextCard());
                player2.AddToHand(cs.RemoveNextCard());
            }
        }

        /// <summary>
        /// Adds a card to a player's battle pile
        /// </summary>
        /// <param name="player">player</param>
        private void AddToBattlePile(Player player)
        {
            Card c = player.DrawHand();
            if (c != null)
                player.AddToBattlePile(c);
        }

        /// <summary>
        /// Reset Round play
        /// When both players run out of cards and in a tie situation, reset the round and play again
        /// </summary>
        /// <param name="player">player</param>
        private void ResetRound(Player player)
        {
            player.AddToWinPile(player.GetBattlePile());
        }

        /// <summary>
        /// Worker method:
        ///   - Draws card from player's hand and puts it on the top of the player's battle pile
        ///   - Compares player's battle card and declares a winner or tie
        ///   - If tie, draws a card to place face down on the battle pile and recursively calls battle method until a winner is found
        ///   - When winner is determined, adds both player's battle pile to the winning player's win pile
        ///   - Calls the output object to display results
        /// </summary>
        /// <param name="p1">player 1</param>
        /// <param name="p2">player 2</param>
        private void Battle(Player p1, Player p2)
        {
            AddToBattlePile(p1);
            AddToBattlePile(p2);
            Card c1 = p1.TopBattleCard;
            Card c2 = p2.TopBattleCard;

            if (c1.Value > c2.Value)        //  Player 1 wins
            {
                p1.AddToWinPile(p1.GetBattlePile());
                p1.AddToWinPile(p2.GetBattlePile());
                _battleOutput.OutputResult(c1, c2, "Player 1 wins");
            }
            else if (c1.Value < c2.Value)   //  Player 2 wins
            {
                p2.AddToWinPile(p1.GetBattlePile());
                p2.AddToWinPile(p2.GetBattlePile());
                _battleOutput.OutputResult(c1, c2, "Player 2 wins");
            }
            else
            {                                       //  Tie => additional battle
                _battleOutput.OutputResult(c1, c2, "Battle is a tie");
                if (p1.NumberOfCards == 0 && p2.NumberOfCards == 0)     //  Both player run out of cards and currently a tie
                {
                    _battleOutput.OutputResult(c1, c2, "Tie - both players ran out of cards => resetting round");
                    ResetRound(p1);
                    ResetRound(p2);
                }

                AddToBattlePile(p1);                //  Draw face down card and place in battle pile
                AddToBattlePile(p2);
                _battleOutput.OutputResult(p1.TopBattleCard, p2.TopBattleCard, "Down Card");
                Battle(p1, p2);
            }
        }

        /// <summary>
        /// Game play loop - continues until a winner is found
        /// </summary>
        /// <param name="player1">player 1</param>
        /// <param name="player2">player 2</param>
        /// <returns>GameResults object containing winning and losing player</returns>
        public GameResults PlayGame(Player player1, Player player2)
        {
            _battleOutput.OutputHeader();

            int numberOfRounds = 0;
            while (player1.NumberOfCards != 0 && player2.NumberOfCards != 0)
            {
                Battle(player1, player2);
                numberOfRounds++;
            }

            GameResults results = new GameResults() { NumberOfRounds = numberOfRounds };
            if (player2.NumberOfCards == 0)
            {
                results.WinningPlayer = player1;
                results.LosingPlayer = player2;
                Console.WriteLine($"Player 1 wins in {numberOfRounds} rounds!");
            }
            else
            {
                results.WinningPlayer = player2;
                results.LosingPlayer = player1;
                Console.WriteLine($"Player 2 wins in {numberOfRounds} rounds!");
            }

            return results;
        }

    }
}
