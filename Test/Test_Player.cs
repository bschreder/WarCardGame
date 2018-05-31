using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarCardGame.CardGame;
using Xunit;

namespace WarCardGame.Test
{
    public class Test_Player
    {
        private static List<Card> CardData()
        {
            return
                new List<Card>
                {
                    new Card(CardSuit.CLUB, CardRank.RANK_QUEEN),
                    new Card(CardSuit.DIAMOND, CardRank.RANK_JACK),
                    new Card(CardSuit.HEART, CardRank.RANK_4),
                    new Card(CardSuit.SPADE, CardRank.RANK_7),
                    new Card(CardSuit.CLUB, CardRank.RANK_5),
                    new Card(CardSuit.DIAMOND, CardRank.RANK_10)
                };
        }

        private List<Card> GetFromCardData(int numberOfCards)
        {
            List<Card> cardData = CardData();
            List<Card> cards = new List<Card>();
            for (int i = 0; i < numberOfCards; i++)
                cards.Add(cardData[i % cardData.Count]);
            return cards;
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(53)]
        public void Test_Player_AddToHand(int numberOfCards)
        {
            Card card = new Card(CardSuit.SPADE, CardRank.RANK_3);
            Player player = new Player();

            for(int i = 0; i < numberOfCards; i++)
                player.AddToHand(card);

            Assert.True(numberOfCards == player.NumberOfCards);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(53)]
        public void Test_Player_AddToReserves(int numberOfCards)
        {
            List<Card> cards = Enumerable.Repeat(new Card(CardSuit.HEART, CardRank.RANK_8), numberOfCards).ToList();
            Player player = new Player();

            player.AddToWinPile(cards);

            Assert.True(numberOfCards == player.NumberOfCards);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(53)]
        public void Test_Player_DrawHand_FromHand(int numberOfCards)
        {
            List<Card> cards = GetFromCardData(numberOfCards);
            Player player = new Player();

            foreach (Card card in cards)
                player.AddToHand(card);

            Assert.True(numberOfCards == player.NumberOfCards);
            for (int i = 0; i < numberOfCards; i++)
                Assert.Equal(cards[i], player.DrawHand());
            Assert.True(player.NumberOfCards == 0);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(53)]
        public void Test_Player_DrawHand_FromReserve(int numberOfCards)
        {
            List<Card> cards = GetFromCardData(numberOfCards);
            Player player = new Player();

            player.AddToWinPile(cards);

            while (player.NumberOfCards != 0)
                Assert.Contains<Card>(player.DrawHand(), cards);
            Assert.True(0 == player.NumberOfCards);
            Assert.Null(player.TopBattleCard);
        }


        [Fact]
        public void Test_Player_DrawHand_EmptyHandReserve()
        {
            Player player = new Player();

            Assert.True(0 == player.NumberOfCards);
            Assert.Null(player.DrawHand());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(53)]
        public void Test_Player_AddToBattlePile(int numberOfCards)
        {
            List<Card> cards = GetFromCardData(numberOfCards);
            Player player = new Player();

            foreach (Card card in cards)
                player.AddToBattlePile(card);

            if (numberOfCards == 0)
                Assert.Null(player.TopBattleCard);
            else
                Assert.Equal(cards[cards.Count - 1], player.TopBattleCard);
        }

        [Fact]
        public void Test_Player_AddToBattlePile_Null()
        {
            Card card = null;
            Player player = new Player();

            player.AddToBattlePile(card);

            Assert.Null(player.TopBattleCard);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(53)]
        public void Test_Player_GetBattlePile(int numberOfCards)
        {
            List<Card> cards = GetFromCardData(numberOfCards);
            Player player = new Player();

            foreach (Card card in cards)
                player.AddToBattlePile(card);

            List<Card> allBattlePile = player.GetBattlePile();

            Assert.Null(player.TopBattleCard);
            Assert.True(cards.Count == allBattlePile.Count);
            cards.Reverse();                                //  Stack is LIFO so reverse list to get read order
            Assert.Equal<Card>(cards, allBattlePile);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(6)]
        public void Test_Player_Properities(int numberOfCards)
        {
            List<Card> cards = GetFromCardData(numberOfCards);
            Player player = new Player();

            foreach (Card card in cards)
            {
                player.AddToHand(card);
                player.AddToBattlePile(card);
            }
            player.AddToWinPile(cards);


            Assert.True(numberOfCards * 2 == player.NumberOfCards);
            if (numberOfCards == 0)
                Assert.Null(player.TopBattleCard);
            else
                Assert.Equal(cards[cards.Count - 1], player.TopBattleCard);
        }
    }
}
