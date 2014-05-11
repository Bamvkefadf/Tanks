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
            bool flag = true;

            while (tanks.Count < amountTanks)
            {
                x = r.Next(15) * 40;
                y = r.Next(4) * 40;
                flag = true;

                foreach (Tank t in tanks)
                    if ((t.X == x || t.X == x + 40 || t.X == x - 40) && (t.Y == y+10 || t.Y == y-10 || t.Y == y))
                    {
                        flag = false;
                        break;
                    }
                    
                if (flag)
                     tanks.Add(new Tank(x, y));
                
            }
        }

        public void Play()
        {
            while (gameStatus == GameStatus.PLAY)
            {
                Thread.Sleep(speedGame);
                foreach(Tank t in tanks)
                    t.Run();

                for (int i = 0; i < tanks.Count - 1; i++ )
                    for (int j = i + 1; j < tanks.Count; j++ )
                        if (
                            (Math.Abs(tanks[i].X - tanks[j].X) <= 40 && (tanks[i].Y == tanks[j].Y))
                            ||
                            (Math.Abs(tanks[i].Y - tanks[j].Y) <= 40 && (tanks[i].X == tanks[j].X))
                            ||
                            (Math.Abs(tanks[i].X - tanks[j].X) <= 40 && Math.Abs(tanks[i].Y - tanks[j].Y) <= 40)
                            )
                        {
                            tanks[i].TurnAround();
                            tanks[j].TurnAround();
                        }
            }
        }
    }
}
