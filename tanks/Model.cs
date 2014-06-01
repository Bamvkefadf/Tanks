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
        public int speedGame;
        int amountSimpleTanks;
        int amountWalls;
        int amountHunterTanks;
        int checkLoading;
        List<EnemyTank> simpleTanks;
        List<HunterTank> hunterTanks;
        List<Wall> walls;
        Player player;
        Score score;

        public GameStatus gameStatus;
        Random r;

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

        internal List<EnemyTank> SimpleTanks
        {
            get { return simpleTanks; }
        }

        internal List<HunterTank> HunterTanks
        {
            get { return hunterTanks; }
        }

        public Model(int amountSimpleTanks, int speedGame, int amountWalls, int amountHunterTanks)
        {
            r = new Random();

            this.amountSimpleTanks = amountSimpleTanks;
            this.speedGame = speedGame;
            this.amountWalls = amountWalls;
            this.amountHunterTanks = amountHunterTanks;

            NewGame();
        }

        private void CreateHunterTanks()
        {
            int x, y;
            bool flag = true;
            checkLoading = 0;
            while (hunterTanks.Count < amountHunterTanks)
            {
                checkLoading++;
                if (checkLoading > 5000)
                {
                    MessageBox.Show("Слишком большое число объектов!");
                    break;
                }
                
                x = (r.Next(0, 750));
                y = (r.Next(1, 300));

                flag = true;

                foreach (HunterTank t in hunterTanks)
                    if //(Math.Sqrt(Math.Pow(Math.Abs(t.X - x), 2) + Math.Pow(Math.Abs(t.Y - y), 2)) < 56)
                        (Math.Abs(t.X - x) <= 41 && Math.Abs(t.Y - y) <= 41)
                    {
                        flag = false;
                        break;
                    }

                if (flag)
                    hunterTanks.Add(new HunterTank(x, y));
            }
        }

        private void CreateSimpleTanks()
        {
            int x, y;
            bool flag = true;
            checkLoading = 0;
            while (simpleTanks.Count < amountSimpleTanks)
            {
                checkLoading++;
                if (checkLoading > 5000)
                {
                    MessageBox.Show("Слишком большое число объектов!");
                    break;
                }

                x = (r.Next(0, 750));
                y = (r.Next(1, 300));

                flag = true;

                foreach (EnemyTank t in simpleTanks)
                    if (Math.Abs(t.X - x) <= 41 && Math.Abs(t.Y - y) <= 41)
                    {
                        flag = false;
                        break;
                    }

                foreach (HunterTank h in hunterTanks)
                    if //(Math.Sqrt(Math.Pow(Math.Abs(h.X - x), 2) + Math.Pow(Math.Abs(h.Y - y), 2)) < 56)
                        (Math.Abs(h.X - x) <= 41 && Math.Abs(h.Y - y) <= 41)
                    {
                        flag = false;
                        break;
                    }

                if (flag)
                    simpleTanks.Add(new EnemyTank(x, y));
            }
        }

        private void CreateWalls()
        {
            int x, y;
            bool flag = true;
            checkLoading = 0;

            while (walls.Count < amountWalls)
            {
                checkLoading++;
                if (checkLoading > 5000)
                {
                    MessageBox.Show("Слишком большое число объектов!");
                    break;
                }

                x = (r.Next(0, 765));
                y = (r.Next(1, 510));
                flag = true;

                foreach (Wall w in walls)
                    if (Math.Abs(w.X - x) <= 41 && Math.Abs(w.Y - y) <= 41)
                    {
                        flag = false;
                        break;
                    }

                foreach (EnemyTank t in simpleTanks)
                    if (Math.Abs(t.X - x) <= 41 && Math.Abs(t.Y - y) <= 41)
                    {
                        flag = false;
                        break;
                    }

                foreach (HunterTank h in hunterTanks)
                    if (Math.Abs(h.X - x) <= 41 && Math.Abs(h.Y - y) <= 41)
                    {
                        flag = false;
                        break;
                    }

                if (flag)
                    walls.Add(new Wall(x, y));        

            }
        }

        public void Play()
        {
            while (gameStatus == GameStatus.PLAY)
            {
                int temp_x = player.X, temp_y = player.Y;
                Thread.Sleep(speedGame);
                player.Run();
                RunEnemies();
                Interaction_EnemiesProjectilesWithObjects();
                Interaction_PlayerProjectileWithObjects();
                Interaction_EnemiesWithEnemies();

                Interaction_PlayerWithWalls(temp_x, temp_y);

                Interaction_PlayerWithEnemies();
                checkSimpleTanksInteractWalls();
                checkHunterTanksInteractWalls();
            }
            if (gameStatus == GameStatus.LOSE)
                MessageBox.Show("Вы потерпели сокрушительное поражение!");
        }

        private void Interaction_EnemiesProjectilesWithObjects()
        {
            for (int i = 0; i < simpleTanks.Count; i++)
                if
                    ((simpleTanks[i].projectile.X > player.X - 8) &&
                    (simpleTanks[i].projectile.X < player.X + 40) &&
                    (simpleTanks[i].projectile.Y > player.Y - 8) &&
                    (simpleTanks[i].projectile.Y < player.Y + 40))
                {
                    simpleTanks[i].projectile.ProjectileDefaultSettings();
                    DamagePlayer();
                }

            for (int i = 0; i < hunterTanks.Count; i++)
                if
                    ((hunterTanks[i].projectile.X > player.X - 8) &&
                    (hunterTanks[i].projectile.X < player.X + 40) &&
                    (hunterTanks[i].projectile.Y > player.Y - 8) &&
                    (hunterTanks[i].projectile.Y < player.Y + 40))
                {
                    hunterTanks[i].projectile.ProjectileDefaultSettings();
                    DamagePlayer();
                }

            for (int i = 0; i < simpleTanks.Count; i++)
                for (int j = 0; j < walls.Count; j++)
                    if
                        ((simpleTanks[i].projectile.X > walls[j].X - 8) &&
                        (simpleTanks[i].projectile.X < walls[j].X + 40) &&
                        (simpleTanks[i].projectile.Y > walls[j].Y - 8) &&
                        (simpleTanks[i].projectile.Y < walls[j].Y + 40))
                    {
                    walls[i].CurrentHealth -= 1;
                    if (walls[i].CurrentHealth == 0)
                        walls.RemoveAt(j);
                    simpleTanks[i].projectile.ProjectileDefaultSettings();
                }
        }

        private void DamagePlayer()
        {
            player.Health -= 1;
            if (player.Health == 0)
                gameStatus = GameStatus.LOSE;
            else
            {
                player.X = 400;
                player.Y = 550;
            }
        }

        private void Interaction_PlayerWithWalls(int temp_x, int temp_y)
        {
            for (int i = 0; i < walls.Count; i++)
                if (
                    (Math.Abs(player.X - walls[i].X) <= 40 && Math.Abs(player.Y - walls[i].Y) <= 40)
                    )
                {
                    player.X = temp_x;
                    player.Y = temp_y;
                    player.moving_direction = Direction.STOP;
                }
        }

        private void Interaction_EnemiesWithEnemies()
        {
            for (int i = 0; i < simpleTanks.Count - 1; i++)
                for (int j = i + 1; j < simpleTanks.Count; j++)
                    if (
                        (Math.Abs(simpleTanks[i].X - simpleTanks[j].X) <= 41 && (simpleTanks[i].Y == simpleTanks[j].Y))
                        ||
                        (Math.Abs(simpleTanks[i].Y - simpleTanks[j].Y) <= 41 && (simpleTanks[i].X == simpleTanks[j].X))
                        ||
                        (Math.Abs(simpleTanks[i].X - simpleTanks[j].X) <= 41 && Math.Abs(simpleTanks[i].Y - simpleTanks[j].Y) <= 41)
                        )
                    {
                        simpleTanks[i].X = simpleTanks[i].Prev_x;
                        simpleTanks[i].Y = simpleTanks[i].Prev_y;
                        simpleTanks[j].X = simpleTanks[j].Prev_x;
                        simpleTanks[j].Y = simpleTanks[j].Prev_y;
                        simpleTanks[i].TurnAround();
                        simpleTanks[j].TurnAround();
                    }

            for (int i = 0; i < simpleTanks.Count; i++)
                for (int j = 0; j < hunterTanks.Count; j++)
                    if (
                        (Math.Abs(simpleTanks[i].X - hunterTanks[j].X) <= 41 && (simpleTanks[i].Y == hunterTanks[j].Y))
                        ||
                        (Math.Abs(simpleTanks[i].Y - hunterTanks[j].Y) <= 41 && (simpleTanks[i].X == hunterTanks[j].X))
                        ||
                        (Math.Abs(simpleTanks[i].X - hunterTanks[j].X) <= 41 && Math.Abs(simpleTanks[i].Y - hunterTanks[j].Y) <= 41)
                        )
                    {
                        simpleTanks[i].X = simpleTanks[i].Prev_x;
                        simpleTanks[i].Y = simpleTanks[i].Prev_y;
                        hunterTanks[j].X = hunterTanks[j].Prev_x;
                        hunterTanks[j].Y = hunterTanks[j].Prev_y;
                        simpleTanks[i].TurnAround();
                        hunterTanks[j].TurnAround();
                    }

            for (int i = 0; i < hunterTanks.Count - 1; i++)
                for (int j = i + 1; j < hunterTanks.Count; j++)
                    if (
                        (Math.Abs(hunterTanks[i].X - hunterTanks[j].X) <= 41 && (hunterTanks[i].Y == hunterTanks[j].Y))
                        ||
                        (Math.Abs(hunterTanks[i].Y - hunterTanks[j].Y) <= 41 && (hunterTanks[i].X == hunterTanks[j].X))
                        ||
                        (Math.Abs(hunterTanks[i].X - hunterTanks[j].X) <= 41 && Math.Abs(hunterTanks[i].Y - hunterTanks[j].Y) <= 41)
                        )
                    {
                        hunterTanks[i].X = hunterTanks[i].Prev_x;
                        hunterTanks[i].Y = hunterTanks[i].Prev_y;
                        hunterTanks[j].X = hunterTanks[j].Prev_x;
                        hunterTanks[j].Y = hunterTanks[j].Prev_y;
                        hunterTanks[i].TurnAround();
                        hunterTanks[j].TurnAround();
                    }
        }

        private void Interaction_PlayerWithEnemies()
        {
            for (int i = 0; i < simpleTanks.Count; i++)
                if (Math.Abs(player.X - simpleTanks[i].X) <= 40 && Math.Abs(player.Y - simpleTanks[i].Y) <= 40)
                    DamagePlayer();

            for (int i = 0; i < hunterTanks.Count; i++)
                if (Math.Abs(player.X - hunterTanks[i].X) <= 40 && Math.Abs(player.Y - hunterTanks[i].Y) <= 40)
                    DamagePlayer();
        }

        private void RunEnemies()
        {
            for (int i = 0; i < simpleTanks.Count; i++)
            {
                simpleTanks[i].Run(player.X, player.Y);
            }

            for (int i = 0; i < hunterTanks.Count; i++)
            {
                hunterTanks[i].Run(player.X, player.Y);
            }
        }

        private void Interaction_PlayerProjectileWithObjects()
        {
            for (int i = 0; i < simpleTanks.Count; i++)
            {
                if
                    ((player.projectile.X > simpleTanks[i].projectile.X - 8) &&
                    (player.projectile.X < simpleTanks[i].projectile.X + 8) &&
                    (player.projectile.Y > simpleTanks[i].projectile.Y - 8) &&
                    (player.projectile.Y < simpleTanks[i].projectile.Y + 8))
                {
                    simpleTanks[i].projectile.ProjectileDefaultSettings();
                    player.projectile.ProjectileDefaultSettings();
                }
                if
                    ((player.projectile.X > simpleTanks[i].X - 8) &&
                    (player.projectile.X < simpleTanks[i].X + 40) &&
                    (player.projectile.Y > simpleTanks[i].Y - 8) &&
                    (player.projectile.Y < simpleTanks[i].Y + 40))
                {
                    score.Increment();
                    simpleTanks.RemoveAt(i);
                    player.projectile.ProjectileDefaultSettings();
                }
            }

            for (int i = 0; i < hunterTanks.Count; i++)
            {
                if
                    ((player.projectile.X > hunterTanks[i].projectile.X - 8) &&
                    (player.projectile.X < hunterTanks[i].projectile.X + 8) &&
                    (player.projectile.Y > hunterTanks[i].projectile.Y - 8) &&
                    (player.projectile.Y < hunterTanks[i].projectile.Y + 8))
                {
                    hunterTanks[i].projectile.ProjectileDefaultSettings();
                    player.projectile.ProjectileDefaultSettings();
                }
                if
                    ((player.projectile.X > hunterTanks[i].X - 8) &&
                    (player.projectile.X < hunterTanks[i].X + 40) &&
                    (player.projectile.Y > hunterTanks[i].Y - 8) &&
                    (player.projectile.Y < hunterTanks[i].Y + 40))
                {
                    score.DoubleIncrement();
                    hunterTanks.RemoveAt(i);
                    player.projectile.ProjectileDefaultSettings();
                }

            }

            for (int i = 0; i < walls.Count; i++)
                if
                    ((player.projectile.X > walls[i].X - 8) &&
                    (player.projectile.X < walls[i].X + 40) &&
                    (player.projectile.Y > walls[i].Y - 8) &&
                    (player.projectile.Y < walls[i].Y + 40))
                {
                    walls[i].CurrentHealth -= 1;
                    if (walls[i].CurrentHealth == 0)
                        walls.RemoveAt(i);
                    player.projectile.ProjectileDefaultSettings();
                }
        }

        private void checkHunterTanksInteractWalls()
        {
            for (int i = 0; i < hunterTanks.Count; i++)
            {
                for (int j = 0; j < walls.Count; j++)
                {
                    if (
                            (Math.Abs(hunterTanks[i].X - walls[j].X) <= 41 && (hunterTanks[i].Y == walls[j].Y))
                            ||
                            (Math.Abs(hunterTanks[i].Y - walls[j].Y) <= 41 && (hunterTanks[i].X == walls[j].X))
                            ||
                            (Math.Abs(hunterTanks[i].X - walls[j].X) <= 41 && Math.Abs(hunterTanks[i].Y - walls[j].Y) <= 41)
                            )
                    {
                        hunterTanks[i].X = hunterTanks[i].Prev_x;
                        hunterTanks[i].Y = hunterTanks[i].Prev_y;
                        hunterTanks[i].TurnAround();
                    }
                }
            }
        }

        public void checkSimpleTanksInteractWalls()
        {
            for (int i = 0; i < simpleTanks.Count; i++ )
            {
                for (int j = 0; j < walls.Count; j++)
                {
                    if (
                            (Math.Abs(simpleTanks[i].X - walls[j].X) <= 41 && (simpleTanks[i].Y == walls[j].Y))
                            ||
                            (Math.Abs(simpleTanks[i].Y - walls[j].Y) <= 41 && (simpleTanks[i].X == walls[j].X))
                            ||
                            (Math.Abs(simpleTanks[i].X - walls[j].X) <= 41 && Math.Abs(simpleTanks[i].Y - walls[j].Y) <= 41)
                            )
                    {
                        simpleTanks[i].X = simpleTanks[i].Prev_x;
                        simpleTanks[i].Y = simpleTanks[i].Prev_y;
                        simpleTanks[i].TurnAround();
                    }
                }
            }
        }

        internal void NewGame()
        {
            player = new Player();
            score = new Score();
            simpleTanks = new List<EnemyTank>();
            hunterTanks = new List<HunterTank>();
            walls = new List<Wall>();
            amountHunterTanks = Properties.Settings.Default.amountHunterTanks;
            amountSimpleTanks = Properties.Settings.Default.amountSimpleTanks;
            speedGame = Properties.Settings.Default.speedGame;
            amountWalls = Properties.Settings.Default.amountWalls;

            CreateHunterTanks();
            CreateSimpleTanks();
            CreateWalls();

            gameStatus = GameStatus.STOP;
        }
    }
}
