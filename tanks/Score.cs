using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tanks
{
    class Score
    {
        int currentScore;

        public int CurrentScore
        {
            get { return currentScore; }
        }

        public void Increment()
        {
            currentScore++;
        }

        public void DoubleIncrement()
        { 
            currentScore += 2;
        }
    }
}
