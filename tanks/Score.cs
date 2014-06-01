﻿using System;
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
            set { currentScore = value; }
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
