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
        List<Tank> tanks;
        List<Wall> walls;
        Player player;

        public GameStatus gameStatus;
        Random r;

        internal Player Player
        {
            get { return player; }
            set { player = value; }
        }

        internal List<Wall> Walls
        {
            get { return walls; }
        }

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
            player = new Player();

            tanks = new List<Tank>();
            CreateTanks();

            walls = new List<Wall>();
            CreateWalls();
         
            gameStatus = GameStatus.STOP;
        }

        private void CreateWalls()
        {
            int x, y;
            bool flag = true;

            while (walls.Count < amountWalls)
            {
                x = (r.Next(0, 750));
                y = (r.Next(1, 560));
                flag = true;

                foreach (Wall w in walls)
                    if (Math.Sqrt(Math.Pow(Math.Abs(w.X - x), 2) + Math.Pow(Math.Abs(w.X - x), 2)) < 56)
                    {
                        flag = false;
                        break;
                    }

                if (flag)
                    walls.Add(new Wall(x, y));        

            }
        }

        private void CreateTanks()
        {
            int x, y;
            bool flag = true;

            while (tanks.Count < amountTanks)
            {
                x = (r.Next(0, 750));
                y = (r.Next(1, 200));
                flag = true;

                foreach (Tank t in tanks)
                    if (Math.Sqrt(Math.Pow(Math.Abs(t.X - x), 2) + Math.Pow(Math.Abs(t.X - x), 2)) < 56)
                        //&& (t.Y == y || t.Y <= y - 40 || t.Y >= y + 40))
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

                player.Run();

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
                checkInternalWalls();
            }
        }

        public void checkInternalWalls()
        {
            for (int i = 0; i < tanks.Count; i++ )
            {
                for (int j = 0; j < walls.Count; j++)
                {
                    if (
                            (Math.Abs(tanks[i].X - walls[j].X) <= 40 && (tanks[i].Y == walls[j].Y))
                            ||
                            (Math.Abs(tanks[i].Y - walls[j].Y) <= 40 && (tanks[i].X == walls[j].X))
                            ||
                            (Math.Abs(tanks[i].X - walls[j].X) <= 40 && Math.Abs(tanks[i].Y - walls[j].Y) <= 40)
                            )
                    {
                        tanks[i].TurnAround();
                    }
                }
            }
        }
    }
}
