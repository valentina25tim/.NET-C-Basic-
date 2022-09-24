using System;
using System.Collections.Generic;
using System.Linq;

namespace GuessNumber___ParallelForEach.GuessNumber_ParallelForEach
{

    /// i; i++
    sealed class GamerQueue1 : BaseGamer
    {
        private int _tryGessed;
        private int temp = 0;

        public GamerQueue1(ConsoleColor color, IRules answers) :
            base(color, answers)
        { }

        public override int GenerationAnswer()
        {
            int _temp = temp;
            _tryGessed = Rules.NumberSet.ElementAt(_temp);
            TryGuessGamer = _tryGessed;

            if (temp < Rules.NumberSet.Count)
                temp++;

            return TryGuessGamer;
        }
    }

    /// random numb without memory
    sealed class GamerRand2 : BaseGamer
    {
        public GamerRand2(ConsoleColor color, IRules answers) :
            base(color, answers)
        { }

        public override int GenerationAnswer()
        {
            TryGuessGamer = Rules.NumberSet.ElementAt(Helper.GetRandomBetween(0, Rules.NumberSet.Count));

            return TryGuessGamer;
        }
    }

    /// random numb with memory
    sealed class GamerRandMemory3 : BaseGamer
    {
        private HashSet<int> Gamer2Set = new HashSet<int>();
        private bool _contGener;
        private int _tryGessed;

        public GamerRandMemory3(ConsoleColor color, IRules answers) :
            base(color, answers)
        { }

        public override int GenerationAnswer()
        {
            HashSet<int> _gamer2Set = new HashSet<int>();
            do
            {
                _tryGessed = Rules.NumberSet.ElementAt(Helper.GetRandomBetween(0, Rules.NumberSet.Count));

                if (Gamer2Set.Contains(_tryGessed))
                {
                    Gamer2Set.Add(_tryGessed);

                    _contGener = false;
                }
                if (!Gamer2Set.Contains(_tryGessed))
                {
                    TryGuessGamer = _tryGessed;
                    Gamer2Set.Add(TryGuessGamer);

                    _contGener = true;
                }
            }
            while (!_contGener);

            return TryGuessGamer;
        }
    }

    /// random numb without memory and knows everyone answers 
    sealed class GamerRandSly4 : BaseGamer
    {
        private HashSet<int> NotUsedSet;

        public GamerRandSly4(ConsoleColor color, IRules answers) :
            base(color, answers)
        { }

        public override int GenerationAnswer()
        {
            NotUsedSet = Helper.DifBetween(Rules.NumberSet, Rules.AnswerSet);

            TryGuessGamer = NotUsedSet.ElementAt(Helper.GetRandomBetween(0, NotUsedSet.Count));

            return TryGuessGamer;
        }
    }

    /// random number with memory and knows everyone answers 
    sealed class GamerRandMemorySly5 : BaseGamer
    {
        private HashSet<int> NotUsedSet;
        private bool _contGener;
        private int _tryGessed;

        public GamerRandMemorySly5(ConsoleColor color, IRules answers) :
            base(color, answers)
        { }

        public override int GenerationAnswer()
        {
            do
            {
                NotUsedSet = Helper.DifBetween(Rules.NumberSet, Rules.AnswerSet);

                _tryGessed = NotUsedSet.ElementAt(Helper.GetRandomBetween(0, NotUsedSet.Count));

                if (Rules.AnswerSet.Contains(_tryGessed))
                {
                    _contGener = false;
                }
                else if (!Rules.AnswerSet.Contains(_tryGessed))
                {
                    TryGuessGamer = _tryGessed;

                    _contGener = true;
                }
            }
            while (!_contGener);

            return TryGuessGamer;
        }
    }
}
