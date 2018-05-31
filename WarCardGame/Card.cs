using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarCardGame.CardGame
{
    public enum CardSuit { CLUB, DIAMOND, HEART, SPADE };
    public enum CardRank
    {
        RANK_2 = 2, RANK_3, RANK_4, RANK_5, RANK_6, RANK_7, RANK_8, RANK_9, RANK_10,
        RANK_JACK, RANK_QUEEN, RANK_KING, RANK_ACE
    }

    /// <summary>
    /// Class that defines the Card object
    /// </summary>
    public class Card
    {
        private readonly CardSuit _suit;
        private readonly CardRank _rank;

        /// <summary>
        /// Integer value of the Card's rank
        /// </summary>
        public int Value => (int)_rank;

        /// <summary>
        /// String representation of the Card's rank and suit
        /// </summary>
        public string Name => $"{GetRank()} {Enum.GetName(typeof(CardSuit), _suit)}";

        /// <summary>
        /// Card constructor
        /// </summary>
        /// <param name="suit">Card's suit enum</param>
        /// <param name="rank">Card's rank enum</param>
        public Card(CardSuit suit, CardRank rank)
        {
            _suit = suit;
            _rank = rank;
        }

        /// <summary>
        /// Get the string representation of this Card's rank 
        /// </summary>
        /// <returns>String representation of the Card's rank</returns>
        private string GetRank()
        {
            string returnRank = null;

            switch(_rank)
            {
                case CardRank.RANK_2:     returnRank = "2"; break;
                case CardRank.RANK_3:     returnRank = "3"; break;
                case CardRank.RANK_4:     returnRank = "4"; break;
                case CardRank.RANK_5:     returnRank = "5"; break;
                case CardRank.RANK_6:     returnRank = "6"; break;
                case CardRank.RANK_7:     returnRank = "7"; break;
                case CardRank.RANK_8:     returnRank = "8"; break;
                case CardRank.RANK_9:     returnRank = "9"; break;
                case CardRank.RANK_10:    returnRank = "10"; break;
                case CardRank.RANK_JACK:  returnRank = "J"; break;
                case CardRank.RANK_QUEEN: returnRank = "Q"; break;
                case CardRank.RANK_KING:  returnRank = "K"; break;
                case CardRank.RANK_ACE:   returnRank = "A"; break;

                default: returnRank = "XXXXX"; break;
            }

            return returnRank;
        }

    }
}
