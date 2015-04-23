using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCaseAnalyzer
{
    class Program
    {
        static void Main()
        {
            var casesToAnalyze = new [] {21};
            foreach (var caseNum in casesToAnalyze)
            {
                var inputLines = File.ReadAllLines(
                    String.Format(@"Inputs\{0}.txt", caseNum));
                var inputLinesPastHeader = inputLines.Skip(3);
                var commands = inputLinesPastHeader.GroupBy(x => x[0]);
                var counts = new List<KeyValuePair<char, int>>();
                foreach (var command in commands)
                {
                    counts.Add(new KeyValuePair<char, int>(command.Key, command.Count()));   
                }
                Console.WriteLine();
            }
        }
    }
}
