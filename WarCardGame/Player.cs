using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarCardGame.CardGame
{
    public class Player
    {
        /// <summary>
        /// Class that represents an individual player
        /// </summary>
        private CardPile _hand;                 //  Holds the cards that are to be used in the next battle
        private CardPile _winPile;              //  Holds the cards from the won battles
        private BattlePile _battlePile;         //  Holds the cards that are currently in battle

        /// <summary>
        /// Number of cards a player currently has that can be used in a battle
        /// </summary>
        public int NumberOfCards => _hand.NumberOfCards + _winPile.NumberOfCards;

        /// <summary>
        /// Top card of a players battle pile
        /// </summary>
        public Card TopBattleCard => _battlePile.TopCard;

        /// <summary>
        /// Default constructor that instantiates a player
        /// </summary>
        public Player ()
        {
            _hand = new CardPile();
            _winPile = new CardPile();
            _battlePile = new BattlePile();
        }

        /// <summary>
        /// Add a card to the players hand
        /// </summary>
        /// <param name="card">card to add</param>
        public void AddToHand(Card card)
        {
            if (card != null)
                _hand.AddCard(card);
        }

        /// <summary>
        /// Draws a card from the players hand
        /// If necessary, will shuffle the win pile and add to the end of the hand object
        /// </summary>
        /// <returns>Next card to be played</returns>
        public Card DrawHand()
        {
            if (_hand.NumberOfCards == 0 && _winPile.NumberOfCards == 0)    //  Player out of cards
                return null;

            if (_hand.NumberOfCards == 0)           //  Player needs to move won reserves to hand
            {
                //Console.WriteLine("Player reshuffling hand");
                _winPile.Shuffle();
                _hand.AddCards(_winPile.RemoveAll());
            }

            return _hand.RemoveNextCard();
        }

        /// <summary>
        /// Adds a list of cards to the player's winning's pile
        /// </summary>
        /// <param name="cards">List of cards</param>
        public void AddToWinPile(List<Card> cards)
        {
            _winPile.AddCards(cards);
        }

        /// <summary>
        /// Adds a card to the player's current battle pile stack
        /// </summary>
        /// <param name="card">card to add</param>
        public void AddToBattlePile(Card card)
        {
            _battlePile.AddCard(card);
        }

        /// <summary>
        /// Gets (and clears) the player's current battle pile
        /// </summary>
        /// <returns>List of cards</returns>
        public List<Card> GetBattlePile()
        {
            return _battlePile.RemoveAll();
        }
    }
}
