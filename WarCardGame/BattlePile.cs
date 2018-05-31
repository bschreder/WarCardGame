using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarCardGame.CardGame
{
    /// <summary>
    /// Class for instantiating and operating on a pile (stack) of cards used in the battle
    /// </summary>
    public class BattlePile
    {
        private Stack<Card> _battlePile;

        /// <summary>
        /// Top card of the battlePile stack
        /// </summary>
        public Card TopCard => (_battlePile.Count != 0) ? _battlePile.Peek() : null;

        /// <summary>
        /// Number of cards in the battlePile stack
        /// </summary>
        public int NumberOfCards => _battlePile.Count;

        /// <summary>
        /// Default Constructor that instantiates the battle pile stack
        /// </summary>
        public BattlePile()
        {
            _battlePile = new Stack<Card>();
        }

        /// <summary>
        /// Hydrating constructor
        /// </summary>
        /// <param name="cards">List of Cards to hydrate BattlePile object</param>
        public BattlePile(List<Card> cards)
        {
            _battlePile = new Stack<Card>(cards);
        }

        /// <summary>
        /// Remove all cards in the battle pile stack and returns a list of cards
        /// </summary>
        /// <returns>List of cards</returns>
        public List<Card> RemoveAll()
        {
            List<Card> rtnList = _battlePile.ToList();
            _battlePile.Clear();
            return rtnList;
        }

        /// <summary>
        /// Add card to battle pile stack
        /// </summary>
        /// <param name="card">card to add</param>
        public void AddCard(Card card)
        {
            _battlePile.Push(card);
        }
    }
}
