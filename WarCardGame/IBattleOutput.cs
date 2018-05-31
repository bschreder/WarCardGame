using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarCardGame.CardGame
{
    public interface IBattleOutput
    {
        void OutputHeader();
        void OutputResult(Card c1, Card c2, string result);
    }
}
