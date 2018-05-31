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
    public class Test_Program
    {

        [Theory]
        [InlineData(1)]
        [InlineData(1000)]
        public void Test_Program_RunGame(int numberOfRuns)
        {
            IBattleOutput battleOutput = new Fake_BattleOutput();

            for (int i = 0; i < numberOfRuns; i++)
            {
                GameResults gameResults = Program.RunGame(battleOutput);

                Assert.True(gameResults.WinningPlayer.NumberOfCards == 52);
                Assert.True(gameResults.LosingPlayer.NumberOfCards == 0);
                Assert.True(gameResults.NumberOfRounds >= 1);           //  Pathological case:  game ends in 1 round (many tie battles with eventual winner)
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        //[InlineData(1000)]
        public void Test_Program_RunGameParallel(int numberOfRuns)
        {
            IBattleOutput battleOutput = new Fake_BattleOutput();

            Parallel.For(0, numberOfRuns, i =>
            {
                GameResults gameResults = Program.RunGame(battleOutput);

                Assert.True(gameResults.WinningPlayer.NumberOfCards == 52);
                Assert.True(gameResults.LosingPlayer.NumberOfCards == 0);
                Assert.True(gameResults.NumberOfRounds >= 1);
            });
        }
    }
}
