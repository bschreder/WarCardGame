using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarCardGame.CardGame;
using Xunit;

namespace WarCardGame.Test
{
    public class Test_BattlePile
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(53)]
        public void Test_BattlePile_Properities(int numberOfCards)
        {
            List<Card> cards = Enumerable.Repeat(new Card(CardSuit.CLUB, CardRank.RANK_10), numberOfCards).ToList();

            BattlePile battlePile = new BattlePile(cards);

            if (numberOfCards == 0)
                Assert.Null(battlePile.TopCard);
            else
                Assert.Equal(cards[numberOfCards-1], battlePile.TopCard);
        }


        private List<Card> StackData()
        {
            return
                new List<Card>
                {
                    new Card(CardSuit.SPADE, CardRank.RANK_KING),
                    new Card(CardSuit.CLUB, CardRank.RANK_ACE),
                    new Card(CardSuit.DIAMOND, CardRank.RANK_2),
                    new Card(CardSuit.HEART, CardRank.RANK_6),
                    new Card(CardSuit.CLUB, CardRank.RANK_3),
                    new Card(CardSuit.HEART, CardRank.RANK_9)
                };
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(53)]
        public void Test_BattlePile_GetAll(int numberOfCards)
        {
            List<Card> stackData = StackData();
            List<Card> cards = new List<Card>();
            for (int i = 0; i < numberOfCards; i++)
                cards.Add(stackData[i % stackData.Count]);

            BattlePile battlePile = new BattlePile(cards);

            List<Card> allBattlePile = battlePile.RemoveAll();               //  GetAll removes the elements from the stack
            Assert.True(cards.Count == allBattlePile.Count);
            cards.Reverse();                                //  Stack is LIFO so reverse list to get read order
            Assert.Equal<Card>(cards, allBattlePile);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(53)]
        public void Test_BattlePile_AddCard(int numberOfCards)
        {
            BattlePile battlePile = new BattlePile();

            for (int i = 0; i < numberOfCards; i++)
                battlePile.AddCard(new Card(CardSuit.DIAMOND, CardRank.RANK_2));

            Assert.True(numberOfCards == battlePile.NumberOfCards);
        }
    }
}
