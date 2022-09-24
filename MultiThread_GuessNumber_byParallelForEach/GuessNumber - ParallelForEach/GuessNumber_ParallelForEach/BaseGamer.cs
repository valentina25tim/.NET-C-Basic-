
using System;
using System.Collections.Generic;
using System.Linq;

namespace GuessNumber___ParallelForEach.GuessNumber_ParallelForEach
{
    public class BaseGamer : IGamer
    {
        protected IRules AllSets;

        public BaseGamer(ConsoleColor color, IRules rule)
        {
            Color = color;
            AllSets = rule;
        }

        public string Name { get; init; }
        public ConsoleColor Color { get; set; }
        public int TryGuessGamer { get; set; }

        public virtual int GenerationAnswer()
        {
            return TryGuessGamer = Helper.GetRandomBetween(0, Rules.NumberSet.Count);
        }

       
    }
}
