using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarCardGame.CardGame;
using Xunit;

namespace WarCardGame.Test
{
    public class Test_Card
    {
        [Theory]
        [InlineData(CardRank.RANK_2, CardSuit.HEART, 2, "2 HEART")]
        [InlineData(CardRank.RANK_3, CardSuit.CLUB, 3, "3 CLUB")]
        [InlineData(CardRank.RANK_4, CardSuit.SPADE, 4, "4 SPADE")]
        [InlineData(CardRank.RANK_5, CardSuit.DIAMOND, 5, "5 DIAMOND")]
        [InlineData(CardRank.RANK_6, CardSuit.HEART, 6, "6 HEART")]
        [InlineData(CardRank.RANK_7, CardSuit.CLUB, 7, "7 CLUB")]
        [InlineData(CardRank.RANK_8, CardSuit.SPADE, 8, "8 SPADE")]
        [InlineData(CardRank.RANK_9, CardSuit.DIAMOND, 9, "9 DIAMOND")]
        [InlineData(CardRank.RANK_10, CardSuit.HEART, 10, "10 HEART")]
        [InlineData(CardRank.RANK_JACK, CardSuit.CLUB, 11, "J CLUB")]
        [InlineData(CardRank.RANK_QUEEN, CardSuit.SPADE, 12, "Q SPADE")]
        [InlineData(CardRank.RANK_KING, CardSuit.DIAMOND, 13, "K DIAMOND")]
        [InlineData(CardRank.RANK_ACE, CardSuit.HEART, 14, "A HEART")]
        public void Test_Card_Properities(CardRank rank, CardSuit suit, int expectedValue, string expectedName)
        {
            //  Arrange
            Card card = new Card(suit, rank);

            //  Act
            int value = card.Value;
            string name = card.Name;

            //  Assert
            Assert.Equal(expectedValue, value);
            Assert.Equal(expectedName, name);
        }
    }
}
