using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarCardGame.CardGame;
using WarCardGame.Test.Fake;
using Xunit;

namespace WarCardGame.Test
{
    public class Test_Game
    {
        private List<Card> CreateCardDeck()
        {
            List<Card> cards = new List<Card>();

            foreach (CardRank rank in Enum.GetValues(typeof(CardRank)))
                foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
                    cards.Add(new Card(suit, rank));

            return cards;
        }

        [Fact]
        public void Test_Game_CreateDeck()
        {
            IBattleOutput battleOutput = new Fake_BattleOutput();
            Game game = new Game(battleOutput);
            CardPile deck = new CardPile();
            game.CreateDeck(deck);

            List<Card> cards = CreateCardDeck();

            List<Card> deckCards = deck.GetAll();

            foreach (Card card in cards)
                Assert.Contains(deckCards, c => (string.Equals(c.Name, card.Name) && (c.Value == card.Value)));

            Assert.True(cards.Count == deckCards.Count);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Test_Game_Shuffle(bool createDeck)
        {
            const int numberOfShuffles = 1;

            IBattleOutput battleOutput = new Fake_BattleOutput();
            Game game = new Game(battleOutput);
            CardPile deck = new CardPile();
            if (createDeck)
                game.CreateDeck(deck);

            List<Card> preShuffleCards = deck.GetAll();
            deck.Shuffle(numberOfShuffles);
            List<Card> postShuffleCards = deck.GetAll();

            Assert.True(preShuffleCards.Count == postShuffleCards.Count);

            if (!createDeck)
                Assert.Equal<Card>(preShuffleCards, postShuffleCards);
            else
            {
                Assert.NotEqual<Card>(preShuffleCards, postShuffleCards);
                foreach (Card card in preShuffleCards)
                    Assert.Contains(postShuffleCards, c => (string.Equals(c.Name, card.Name) && (c.Value == card.Value)));
            }
        }

        [Theory]
        [InlineData(0)]
        [InlineData(3)]
        [InlineData(27)]
        [InlineData(54)]
        public void Test_Game_DealCards(int numberOfCards)
        {
            List<Card> cards = Enumerable.Repeat(new Card(CardSuit.SPADE, CardRank.RANK_KING), numberOfCards).ToList();
            CardPile deck = new CardPile(cards);
            IBattleOutput battleOutput = new Fake_BattleOutput();
            Game game = new Game(battleOutput);
            Player player1 = new Player();
            Player player2 = new Player();

            game.DealCards(deck, player1, player2);

            int dealtCardsToPlayer = (int)Math.Floor(numberOfCards / 2m);

            Assert.True(dealtCardsToPlayer <= player1.NumberOfCards);       //  For odd numberOfCards, player 1 gets an extra card
            Assert.True(dealtCardsToPlayer == player2.NumberOfCards);
        }


        [Theory]
        [InlineData(1)]
        public void Test_Game_PlayGame_Player1Wins(int numberOfCards)
        {
            List<Card> p1Cards = Enumerable.Repeat(new Card(CardSuit.SPADE, CardRank.RANK_KING), numberOfCards).ToList();
            List<Card> p2Cards = Enumerable.Repeat(new Card(CardSuit.SPADE, CardRank.RANK_QUEEN), numberOfCards).ToList();

            Fake_PlayGame fp = new Fake_PlayGame();
            Fake_PlayGame_Model fpm = fp.Setup(p1Cards, p2Cards);

            GameResults gameResults = fpm.Game.PlayGame(fpm.Player1, fpm.Player2);

            Assert.True(numberOfCards * 2 == fpm.Player1.NumberOfCards);
            Assert.True(0 == fpm.Player2.NumberOfCards);
            Assert.Equal<Player>(gameResults.WinningPlayer, fpm.Player1);
            Assert.Equal<Player>(gameResults.LosingPlayer, fpm.Player2);

        }

        [Fact]
        public void Test_Game_PlayGame_TiePlayer1Wins()
        {
            List<Card> p1Cards = new List<Card>
            {
                new Card(CardSuit.SPADE, CardRank.RANK_KING),
                new Card(CardSuit.HEART, CardRank.RANK_KING)
            };

            List<Card> p2Cards = new List<Card>
            {
                new Card(CardSuit.DIAMOND, CardRank.RANK_KING),
                new Card(CardSuit.CLUB, CardRank.RANK_QUEEN)
            };

            Fake_PlayGame fp = new Fake_PlayGame();
            Fake_PlayGame_Model fpm = fp.Setup(p1Cards, p2Cards);

            GameResults gameResults = fpm.Game.PlayGame(fpm.Player1, fpm.Player2);

            Assert.True(0 < fpm.Player1.NumberOfCards);
            Assert.True(0 == fpm.Player2.NumberOfCards);
            Assert.Equal<Player>(gameResults.WinningPlayer, fpm.Player1);
            Assert.Equal<Player>(gameResults.LosingPlayer, fpm.Player2);
        }

        [Theory]
        [InlineData(1)]
        public void Test_Game_PlayGame_Player2Wins(int numberOfCards)
        {
            List<Card> p1Cards = Enumerable.Repeat(new Card(CardSuit.SPADE, CardRank.RANK_6), numberOfCards).ToList();
            List<Card> p2Cards = Enumerable.Repeat(new Card(CardSuit.SPADE, CardRank.RANK_10), numberOfCards).ToList();

            Fake_PlayGame fp = new Fake_PlayGame();
            Fake_PlayGame_Model fpm = fp.Setup(p1Cards, p2Cards);

            GameResults gameResults = fpm.Game.PlayGame(fpm.Player1, fpm.Player2);

            Assert.True(0 == fpm.Player1.NumberOfCards);
            Assert.True(numberOfCards * 2 == fpm.Player2.NumberOfCards);
            Assert.Equal<Player>(gameResults.LosingPlayer, fpm.Player1);
            Assert.Equal<Player>(gameResults.WinningPlayer, fpm.Player2);
        }

        [Fact]
        public void Test_Game_PlayGame_TiePlayer2Wins()
        {
            List<Card> p1Cards = new List<Card>
            {
                new Card(CardSuit.DIAMOND, CardRank.RANK_JACK),
                new Card(CardSuit.CLUB, CardRank.RANK_KING),                //  Down card
                new Card(CardSuit.CLUB, CardRank.RANK_3)

            };

            List<Card> p2Cards = new List<Card>
            {
                new Card(CardSuit.SPADE, CardRank.RANK_JACK),
                new Card(CardSuit.HEART, CardRank.RANK_6)
            };

            Fake_PlayGame fp = new Fake_PlayGame();
            Fake_PlayGame_Model fpm = fp.Setup(p1Cards, p2Cards);

            GameResults gameResults = fpm.Game.PlayGame(fpm.Player1, fpm.Player2);

            Assert.True(0 == fpm.Player1.NumberOfCards);
            Assert.True(0 < fpm.Player2.NumberOfCards);
            Assert.Equal<Player>(gameResults.LosingPlayer, fpm.Player1);
            Assert.Equal<Player>(gameResults.WinningPlayer, fpm.Player2);
        }

        [Fact]
        public void Test_Game_PlayGame_Tie()
        {
            List<Card> cards = new List<Card>
            {
                new Card(CardSuit.CLUB, CardRank.RANK_KING),
                new Card(CardSuit.HEART, CardRank.RANK_10),
                new Card(CardSuit.SPADE, CardRank.RANK_4),
                new Card(CardSuit.DIAMOND, CardRank.RANK_2),
            };

            Fake_PlayGame fp = new Fake_PlayGame();
            Fake_PlayGame_Model fpm = fp.Setup(cards, cards);

            GameResults gameResults = fpm.Game.PlayGame(fpm.Player1, fpm.Player2);

            Assert.True(0 < gameResults.WinningPlayer.NumberOfCards);
            Assert.True(0 == gameResults.LosingPlayer.NumberOfCards);
        }
    }
}
