using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessNumber__Task.GuessNumber_Task
{
    /// <summary>
    /// i; i++
    /// </summary>
    sealed class GamerQueue1 : BaseGamer
    {
        private int _tryGessed;
        private int temp = 0;

        public GamerQueue1(ConsoleColor color) : base(color) { }

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

    ///  <summary>
    /// random numb without memory
    /// </summary>
    sealed class GamerRand2 : BaseGamer
    {
        public GamerRand2(ConsoleColor color) : base(color) { }

        public override int GenerationAnswer()
        {
            TryGuessGamer = Rules.NumberSet.ElementAt(Helper.GetRandomBetween(0, Rules.NumberSet.Count));

            return TryGuessGamer;
        }
    }


    /// <summary>
    /// random numb with memory
    /// </summary>
    sealed class GamerRandMemory3 : BaseGamer
    {
        private HashSet<int> Gamer2Set = new HashSet<int>();
        private bool _contGener;
        private int _tryGessed;

        public GamerRandMemory3(ConsoleColor color) : base(color) { }


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

    ///  <summary>
    /// random numb without memory and knows everyone answers 
    /// </summary>
    sealed class GamerRandSly4 : BaseGamer
    {
        private HashSet<int> NotUsedSet;

        public GamerRandSly4(ConsoleColor color) : base(color) { }

        public override int GenerationAnswer()
        {
            NotUsedSet = Helper.DifBetween(Rules.NumberSet, Rules.AnswersSet);

            TryGuessGamer = NotUsedSet.ElementAt(Helper.GetRandomBetween(0, NotUsedSet.Count));

            return TryGuessGamer;
        }
    }

    ///  <summary>
    /// random number with memory and knows everyone answers 
    /// </summary>
    sealed class GamerRandMemorySly5 : BaseGamer
    {
        private HashSet<int> NotUsedSet;
        private bool _contGener;
        private int _tryGessed;

        public GamerRandMemorySly5(ConsoleColor color) : base(color) { }

        public override int GenerationAnswer()
        {
            do
            {
                NotUsedSet = Helper.DifBetween(Rules.NumberSet, Rules.AnswersSet);

                _tryGessed = NotUsedSet.ElementAt(Helper.GetRandomBetween(0, NotUsedSet.Count));

                if (Rules.AnswersSet.Contains(_tryGessed))
                {
                    _contGener = false;
                }
                else if (!Rules.AnswersSet.Contains(_tryGessed))
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
