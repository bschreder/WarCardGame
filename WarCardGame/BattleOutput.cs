using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarCardGame.CardGame
{
    /// <summary>
    /// Game output class
    /// </summary>
    public class BattleOutput : IBattleOutput
    {
        const int _stringFieldLength = 13;
        const int _outputHeaderFrequency = 20;

        private int _rowNumber = 0;

        /// <summary>
        /// Format game output string in a 3 column format
        /// </summary>
        /// <param name="s1">left column input value</param>
        /// <param name="s2">middle column input value</param>
        /// <param name="s3">right column input value</param>
        /// <returns>Formatted string of the form '  left | middle  | right'</returns>
        private string FormatOutput(string s1, string s2, string s3)
        {
            if (s1.Length > _stringFieldLength || s2.Length > _stringFieldLength)
            {
                Console.WriteLine($"Output fields are required to be less than {_stringFieldLength}");
                Console.WriteLine($"Left side is {s1.Length} long:   {s1}");
                Console.WriteLine($"Middle is {s2.Length} long:   {s2}");
            }

            int leadingSpaces = _stringFieldLength - s1.Length;
            string left = new string(' ', leadingSpaces) + s1;

            int trailingSpaces = _stringFieldLength - s2.Length;
            string middle = s2 + new string(' ', trailingSpaces);

            string right = s3;

            return $"{left} | {middle} | {right}";
        }

        /// <summary>
        /// Output header line
        /// </summary>
        public void OutputHeader()
        {
            Console.WriteLine();
            Console.WriteLine(FormatOutput("Player 1", "Player 2", "Battle Results"));
            _rowNumber = 0;
        }

        /// <summary>
        /// Output game results
        /// </summary>
        /// <param name="c1">Card</param>
        /// <param name="c2">Card</param>
        /// <param name="result">Battle result</param>
        public void OutputResult(Card c1, Card c2, string result)
        {
            string s1 = (c1 != null) ? c1.Name : "";
            string s2 = (c2 != null) ? c2.Name : "";

            Console.WriteLine(FormatOutput(c1.Name, c2.Name, result));

            _rowNumber++;
            if (_rowNumber % _outputHeaderFrequency == 0)
            {
                Console.WriteLine("Hit any key to continue");
                Console.ReadKey();
                OutputHeader();
            }
        }
    }
}
