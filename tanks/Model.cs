using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace tanks
{
    class Model
    {
        int amountTanks;
        public int speedGame;
        int amountWalls;
        List<EnemyTank> tanks;
        List<Wall> walls;
        Player player;
        Score score;
        Projectile projectile;

        public GameStatus gameStatus;
        Random r;

        internal Projectile Projectile
        {
            get { return projectile; }
        }

        internal Score Score
        {
            get { return score; }
            set { score = value; }
        }

        internal Player Player
        {
            get { return player; }
        }

        internal List<Wall> Walls
        {
            get { return walls; }
        }

        internal List<EnemyTank> Tanks
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
            score = new Score();

            projectile = new Projectile();

            tanks = new List<EnemyTank>();
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
                y = (r.Next(1, 450));
                flag = true;

                foreach (Wall w in walls)
                    if (Math.Abs(w.X - x) <= 40 && Math.Abs(w.Y - y) <= 40)
                    {
                        flag = false;
                        break;
                    }

                foreach (EnemyTank t in tanks)
                    if (Math.Sqrt(Math.Pow(Math.Abs(t.X - x), 2) + Math.Pow(Math.Abs(t.Y - y), 2)) < 56)
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

                if (tanks.Count == 0)
                    tanks.Add(new HunterTank(x, y));
                
                flag = true;

                foreach (EnemyTank t in tanks)
                    if (Math.Sqrt(Math.Pow(Math.Abs(t.X - x), 2) + Math.Pow(Math.Abs(t.Y - y), 2)) < 56)
                    {
                        flag = false;
                        break;
                    }
                    
                if (flag)
                     tanks.Add(new EnemyTank(x, y));  
            }
        }

        public void Play()
        {
            while (gameStatus == GameStatus.PLAY)
            {
                int temp_x = player.X, temp_y = player.Y;
                Thread.Sleep(speedGame);

                player.Run();
                projectile.Run();

                ((HunterTank)tanks[0]).Run(player.X, player.Y);

                for (int i = 1; i < tanks.Count; i++)
                {
                    tanks[i].Run();
                }

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
                            if (i == 0)
                            {
                                ((HunterTank)tanks[i]).TurnAround();
                                tanks[j].TurnAround();
                            }
                            else
                            {
                                tanks[i].TurnAround();
                                tanks[j].TurnAround();
                            }
                        }

                for (int i = 0; i < walls.Count; i++)
                    if (
                        (Math.Abs(player.X - walls[i].X) <= 40 && Math.Abs(player.Y - walls[i].Y) <= 40)
                        )
                    {
                        player.X = temp_x;
                        player.Y = temp_y;
                        player.Direct_x = 0;
                        player.Direct_y = 0;
                    }

                for (int i = 0; i < tanks.Count; i++)
                    if (Math.Abs(player.X - tanks[i].X) <= 40 && Math.Abs(player.Y - tanks[i].Y) <= 40)
                        gameStatus = GameStatus.LOSE;

                for (int i = 0; i < tanks.Count; i++ )
                    if (Math.Abs(player.X - tanks[i].X) <= 40 && Math.Abs(player.Y - tanks[i].Y) <= 40)
                    {
                        score.Increment();
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
