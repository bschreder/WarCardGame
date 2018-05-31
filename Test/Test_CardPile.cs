using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarCardGame.CardGame;
using Xunit;

namespace WarCardGame.Test
{
    public class Test_CardPile
    {

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(52)]
        public void Test_CardPile_Properities(int numberOfCards)
        {
            List<Card> cards = Enumerable.Repeat(new Card(CardSuit.CLUB, CardRank.RANK_10), numberOfCards).ToList();

            CardPile cardPile = new CardPile(cards);

            Assert.True(numberOfCards == cardPile.NumberOfCards);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(20)]
        [InlineData(53)]
        public void Test_CardPile_GetAll(int numberOfCards)
        {
            List<Card> cards = Enumerable.Repeat(new Card(CardSuit.SPADE, CardRank.RANK_KING), numberOfCards).ToList();
            CardPile cardPile = new CardPile(cards);

            Assert.True(cards.Count == cardPile.GetAll().Count);
            Assert.Equal<Card>(cards, cardPile.GetAll());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(20)]
        public void Test_CardPile_AddCard(int numberOfCards)
        {
            CardPile cardPile = new CardPile();

            for (int i = 0; i < numberOfCards; i++)
                cardPile.AddCard(new Card(CardSuit.DIAMOND, CardRank.RANK_2));

            Assert.True(numberOfCards == cardPile.NumberOfCards);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        [InlineData(53)]
        public void Test_CardPile_AddCards(int numberOfCards)
        {
            List<Card> cards = Enumerable.Repeat(new Card(CardSuit.HEART, CardRank.RANK_JACK), numberOfCards).ToList();

            CardPile cardPile = new CardPile();
            cardPile.AddCards(cards);

            Assert.True(cards.Count == cardPile.NumberOfCards);
            Assert.Equal<Card>(cards, cardPile.GetAll());
        }

        [Theory]
        [InlineData(0, 2)]
        [InlineData(2, 1)]
        [InlineData(2, 2)]
        [InlineData(5, 3)]
        [InlineData(53, 2)]
        public void Test_CardPile_RemoveCard(int numberOfCards, int numberToRemove)
        {
            List<Card> cards = Enumerable.Repeat(new Card(CardSuit.CLUB, CardRank.RANK_9), numberOfCards).ToList();
            CardPile cardPile = new CardPile(cards);

            for (int i = 0; i < numberToRemove; i++)
                cardPile.RemoveNextCard();

            int expectedCardCount = (numberOfCards - numberToRemove) > 0 ? numberOfCards - numberToRemove : 0;
            Assert.True(expectedCardCount == cardPile.NumberOfCards);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(2)]
        [InlineData(53)]
        public void Test_CardPile_RemoveAll(int numberOfCards)
        {
            List<Card> cards = Enumerable.Repeat(new Card(CardSuit.DIAMOND, CardRank.RANK_QUEEN), numberOfCards).ToList();
            CardPile cardPile = new CardPile(cards);

            cardPile.RemoveAll();

            Assert.Empty(cardPile.GetAll());
        }

        [Theory]
        [InlineData(null)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1001)]
        public void Test_CardPile_Shuffle(int? numberOfShuffles)
        {
            List<Card> cards = new List<Card>();
            foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
                foreach (CardRank rank in Enum.GetValues(typeof(CardRank)))
                    cards.Add(new Card(suit, rank));

            CardPile cardPile = new CardPile(cards);


            if (numberOfShuffles == null)
                cardPile.Shuffle();
            else
                cardPile.Shuffle(numberOfShuffles ?? 0);


            List<Card> actualCards = cardPile.GetAll();
            if (numberOfShuffles <= 0)
                Assert.Equal<Card>(cards, actualCards);
            else
                Assert.NotEqual<Card>(cards, actualCards);

            Assert.True(cards.Count == actualCards.Count);
        }
    }
}
