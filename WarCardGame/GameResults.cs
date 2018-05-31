using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarCardGame.CardGame;

namespace WarCardGame
{
    public class GameResults
    {
        public Player WinningPlayer { get; set; }
        public Player LosingPlayer { get; set; }
        public int NumberOfRounds { get; set; }
    }
}
