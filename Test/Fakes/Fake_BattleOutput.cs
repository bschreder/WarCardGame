using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarCardGame.CardGame;

namespace WarCardGame.Test.Fake
{
    public class Fake_BattleOutput :  IBattleOutput 
    {
        public void OutputHeader() { }

        public void OutputResult(Card c1, Card c2, string result) { }
    }
}
