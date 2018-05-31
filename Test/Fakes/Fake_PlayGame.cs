using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarCardGame.CardGame;


namespace WarCardGame.Test.Fake
{
    public class Fake_PlayGame_Model
    {
        public Game Game { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
    }

    public class Fake_PlayGame
    {
        public Fake_PlayGame_Model Setup(List<Card> player1Cards, List<Card> player2Cards)
        {
            Fake_PlayGame_Model fpm = new Fake_PlayGame_Model();

            IBattleOutput battleOutput = new Fake_BattleOutput();
            fpm.Game = new Game(battleOutput);

            fpm.Player1 = new Player();
            foreach(Card card in player1Cards)
                fpm.Player1.AddToHand(card);

            fpm.Player2 = new Player();
            foreach (Card card in player2Cards)
                fpm.Player2.AddToHand(card);

            return fpm;
        }
    }
}
