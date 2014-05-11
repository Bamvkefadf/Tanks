using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace tanks
{
    class Model
    {
        int amountTanks;
        public int speedGame;
        int amountWalls;
        public Wall wall;
        List<Tank> tanks;
        public GameStatus gameStatus;
        Random r;

        internal List<Tank> Tanks
        {
            get { return tanks; }
        }

        public Model(int amountTanks, int speedGame, int amountWalls)
        {
            r = new Random();
            this.amountTanks = amountTanks;
            this.speedGame = speedGame;
            this.amountWalls = amountWalls;

            tanks = new List<Tank>();

            CreateTanks();
            
            wall = new Wall();
            gameStatus = GameStatus.STOP;
        }

        private void CreateTanks()
        {
            int x, y;
            while (tanks.Count < amountTanks)
            {
                x = r.Next(0, 550);
                y = r.Next(0, 300);
                tanks.Add(new Tank(x, y));
                foreach (Tank t in tanks)
                {
                }
            }
        }

        public void Play()
        {
            while (gameStatus == GameStatus.PLAY)
            {
                Thread.Sleep(speedGame);
                foreach(Tank t in tanks)
                {
                    t.Run();
                }
            }
        }
    }
}
