using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarCardGame.CardGame
{
    /// <summary>
    /// Class for instantiating and operating on a pile of cards
    /// </summary>
    public class CardPile
    {
        private List<Card> _cardPile;

        //  Fun Fact:  Should shuffle 7x  ( https://www.math.hmc.edu/funfacts/ffiles/20002.4-6.shtml )
        private const int _numberOfShuffles = 7;
        static Random rnd = new Random();       //  must be static so there is only 1 random # generator that both players use

        /// <summary>
        /// Number of cards current in this pile
        /// </summary>
        public int NumberOfCards => _cardPile.Count;

        /// <summary>
        /// Default constructor that instantiates the card pile
        /// </summary>
        public CardPile()
        {
            _cardPile = new List<Card>();
        }

        /// <summary>
        /// Hydrating constructor
        /// </summary>
        /// <param name="cards">List of Cards to hydrate CardPile object</param>
        public CardPile(List<Card> cards)
        {
            _cardPile = cards.ToList();        //  Create new list to break _cardPile and cards object reference
        }

        /// <summary>
        /// Swap the location of two cards
        /// </summary>
        /// <param name="a">location of first card</param>
        /// <param name="b">location of second card</param>
        private void SwapCard(int a, int b)
        {
            Card temp = _cardPile[a];
            _cardPile[a] = _cardPile[b];
            _cardPile[b] = temp;
        }

        /// <summary>
        /// Walks the card pile switching every card with another card selected at random
        /// </summary>
        private void RandomizeCardPile()
        {
            for (int i = 0; i < _cardPile.Count; i++)
                SwapCard(i, rnd.Next(_cardPile.Count));
        }


        /// <summary>
        /// Shuffle the card pile
        /// </summary>
        /// <param name="numTimes">number of times to shuffle the card pile</param>
        public void Shuffle(int numTimes = _numberOfShuffles)
        {
            for (int i = 0; i < numTimes; i++)
                RandomizeCardPile();
        }

        /// <summary>
        /// Get all cards in the card pile
        /// </summary>
        /// <returns>List of cards</returns>
        public List<Card> GetAll()
        {
            return _cardPile.ToList();
        }

        /// <summary>
        /// Add card to card pile
        /// </summary>
        /// <param name="card">card to add</param>
        public void AddCard(Card card)
        {
            _cardPile.Add(card);
        }

        /// <summary>
        /// Add a list of cards to card pile
        /// </summary>
        /// <param name="cards">list of cards</param>
        public void AddCards(List<Card> cards)
        {
            foreach (Card card in cards)
                AddCard(card);
        }

        /// <summary>
        /// Removes the next card from the card pile
        /// </summary>
        /// <returns>Next card</returns>
        public Card RemoveNextCard()
        {
            Card current = _cardPile.FirstOrDefault();
            if (current != null)
                _cardPile.Remove(current);

            return current;
        }

        /// <summary>
        /// Empties all cards in the card pile 
        /// </summary>
        /// <returns>List of cards</returns>
        public List<Card> RemoveAll()
        {
            List<Card> cards = GetAll();
            _cardPile.Clear();
            return cards;
        }
    }
}
