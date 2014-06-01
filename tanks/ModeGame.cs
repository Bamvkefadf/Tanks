using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace tanks
{
    class ModeGame
    {
        protected int speedGame;
        protected int amountSimpleTanks;
        protected int amountWalls;
        protected int amountHunterTanks;
        protected int checkLoading;
        protected List<EnemyTank> simpleTanks;
        protected List<HunterTank> hunterTanks;
        protected List<Wall> walls;
        protected Player player;
        protected Score score;
        protected Random r;
        protected GameStatus gameStatus;
        protected List<Projectile> projectiles;
        private string loseMessage = "";
        private string winMessage = "";
        private string tooMuchObjectsMessage = "";
        private string okMessage = "";
        private SaveResultForm srf;
        int sizeProjectile = 8;
        int sizeWall = 40;
        int sizeTank = 30;
        public string OkMessage
        {
            get { return okMessage; }
            set { okMessage = value; }
        }
        public string LoseMessage
        {
            get { return loseMessage; }
            set { loseMessage = value; }
        }
        public string TooMuchObjectsMessage
        {
            get { return tooMuchObjectsMessage; }
            set { tooMuchObjectsMessage = value; }
        }
        public string WinMessage
        {
            get { return winMessage; }
            set { winMessage = value; }
        }
        public List<Projectile> Projectiles
        {
            get { return projectiles; }
            set { projectiles = value; }
        }
        public int SpeedGame
        {
            get { return speedGame; }
            set { speedGame = value; }
        }
        public GameStatus GameStatus
        {
            get { return gameStatus; }
            set { gameStatus = value; }
        }
        public Score Score
        {
            get { return score; }
            set { score = value; }
        }
        public Player Player
        {
            get { return player; }
        }
        public List<Wall> Walls
        {
            get { return walls; }
        }
        public List<EnemyTank> SimpleTanks
        {
            get { return simpleTanks; }
        }
        public List<HunterTank> HunterTanks
        {
            get { return hunterTanks; }
        }

        protected ModeGame() { }
        public ModeGame(int amountSimpleTanks, int speedGame, int amountWalls, int amountHunterTanks)
        {
            r = new Random();
            score = new Score();
            player = new Player();
            simpleTanks = new List<EnemyTank>();
            hunterTanks = new List<HunterTank>();
            walls = new List<Wall>();
            projectiles = new List<Projectile>();
            gameStatus = GameStatus.STOP;
            this.amountSimpleTanks = amountSimpleTanks;
            this.speedGame = speedGame;
            this.amountWalls = amountWalls;
            this.amountHunterTanks = amountHunterTanks;

            CreateWalls(amountWalls);
            CreateHunterTanks();
            CreateSimpleTanks();
        }

        public virtual void NewGame()
        {
            player = new Player();
            //score = new Score();
            simpleTanks = new List<EnemyTank>();
            hunterTanks = new List<HunterTank>();
            walls = new List<Wall>();
            projectiles = new List<Projectile>();

            amountHunterTanks = Properties.Settings.Default.amountHunterTanks;
            amountSimpleTanks = Properties.Settings.Default.amountSimpleTanks;
            speedGame = Properties.Settings.Default.speedGame;
            amountWalls = Properties.Settings.Default.amountWalls;

            CreateWalls(amountWalls);
            CreateHunterTanks();
            CreateSimpleTanks();

            gameStatus = GameStatus.STOP;
        }
        public virtual void Play()
        {
            while (gameStatus == GameStatus.PLAY)
            {
                int temp_x = player.X, temp_y = player.Y;
                Thread.Sleep(speedGame);
                player.Run();
                RunEnemies();
                FireAndViewProjectiles();
                RunProjectiles();

                Interaction_ProjectilesWithObjects();
                Interaction_EnemiesWithEnemies();

                Interaction_PlayerWithWalls(temp_x, temp_y);

                Interaction_PlayerWithEnemies();
                checkSimpleTanksInteractWalls();
                checkHunterTanksInteractWalls();
                checkWinGame();
            }
            if (gameStatus == GameStatus.LOSE)
            {
                MessageBoxManager.OK = okMessage;
                MessageBoxManager.Register();
                MessageBox.Show(loseMessage);
                MessageBoxManager.Unregister();
                srf = new SaveResultForm(score.CurrentScore, true);
                srf.ShowDialog();
            }

            if (gameStatus == GameStatus.WIN)
            {
                if (Properties.Settings.Default.mode == 0)
                {
                    MessageBoxManager.OK = okMessage;
                    MessageBoxManager.Register();
                    MessageBox.Show(winMessage);
                    MessageBoxManager.Unregister();
                }
                srf = new SaveResultForm(score.CurrentScore, true);
                srf.ShowDialog();

            }
        }

        protected virtual void CreateHunterTanks()
        {
            int x, y;
            bool flag = true;
            checkLoading = 0;
            while (hunterTanks.Count < amountHunterTanks)
            {
                checkLoading++;
                if (checkLoading > 5000)
                {
                    MessageBoxManager.OK = okMessage;
                    MessageBoxManager.Register();
                    MessageBox.Show(tooMuchObjectsMessage);
                    MessageBoxManager.Unregister();
                    break;
                }

                x = (r.Next(0, 39)) * 20;
                y = (r.Next(0, 23)) * 20;

                flag = true;

                foreach (HunterTank t in hunterTanks)
                    if (Math.Abs(t.X - x) <= sizeTank + 1 && Math.Abs(t.Y - y) <= sizeTank+1)
                    {
                        flag = false;
                        break;
                    }

                foreach (Wall w in walls)
                    if (Math.Abs(w.X - x) <= sizeWall && Math.Abs(w.Y - y) <= sizeWall)
                    {
                        flag = false;
                        break;
                    }

                if (flag)
                    hunterTanks.Add(new HunterTank(x, y));
            }
        }
        protected virtual void CreateSimpleTanks()
        {
            int x, y;
            bool flag = true;
            checkLoading = 0;
            while (simpleTanks.Count < amountSimpleTanks)
            {
                checkLoading++;
                if (checkLoading > 5000)
                {
                    Exception ex = new Exception(tooMuchObjectsMessage);
                    MessageBoxManager.OK = okMessage;
                    MessageBoxManager.Register();
                    MessageBox.Show(ex.Message);
                    MessageBoxManager.Unregister();
                    break;
                }

                x = (r.Next(0, 39)) * 20;
                y = (r.Next(0, 23)) * 20;

                flag = true;

                foreach (EnemyTank t in simpleTanks)
                    if (Math.Abs(t.X - x) <= sizeTank + 1 && Math.Abs(t.Y - y) <= sizeTank+1)
                    {
                        flag = false;
                        break;
                    }

                foreach (HunterTank h in hunterTanks)
                    if
                        (Math.Abs(h.X - x) <= sizeTank + 1 && Math.Abs(h.Y - y) <= sizeTank+1)
                    {
                        flag = false;
                        break;
                    }

                foreach (Wall w in walls)
                    if (Math.Abs(w.X - x) <= sizeWall && Math.Abs(w.Y - y) <= sizeWall)
                    {
                        flag = false;
                        break;
                    }

                if (flag)
                    simpleTanks.Add(new EnemyTank(x, y));
            }
        }
        protected virtual void CreateWalls(int amount)
        {
            int x, y;
            bool flag = true;
            checkLoading = 0;

            while (walls.Count < amount)
            {
                checkLoading++;
                if (checkLoading > 5000)
                {
                    MessageBoxManager.OK = okMessage;
                    MessageBoxManager.Register();
                    MessageBox.Show(tooMuchObjectsMessage);
                    MessageBoxManager.Unregister();
                    break;
                }

                x = (r.Next(0, 20)) * sizeWall;
                y = (r.Next(1, 13)) * sizeWall;
                flag = true;

                foreach (Wall w in walls)
                    if (Math.Abs(w.X - x) <= 20 && Math.Abs(w.Y - y) <= 20)
                    {
                        flag = false;
                        break;
                    }

                if (flag)
                    walls.Add(new Wall(x, y));

            }
        }
        protected virtual void checkWinGame()
        {
            if (simpleTanks.Count == 0 && hunterTanks.Count == 0)
                gameStatus = GameStatus.WIN;
        }
        protected void FireAndViewProjectiles()
        {
            for (int i = 0; i < simpleTanks.Count; i++)
            {
                if (simpleTanks[i].Shot == true)
                {
                    projectiles.Add(new Projectile(simpleTanks[i].DistanceOfProjectile, simpleTanks[i].GetDefaultProjectileX(), simpleTanks[i].GetDefaultProjectileY(), simpleTanks[i].Moving_direction, TypeOfProjectile.RED));
                    simpleTanks[i].Shot = false;
                }
            }

            for (int i = 0; i < hunterTanks.Count; i++)
            {
                if (hunterTanks[i].Shot == true)
                {
                    projectiles.Add(new Projectile(hunterTanks[i].DistanceOfProjectile, hunterTanks[i].GetDefaultProjectileX(), hunterTanks[i].GetDefaultProjectileY(), hunterTanks[i].Moving_direction, TypeOfProjectile.BLUE));
                    hunterTanks[i].Shot = false;
                }
            }
        }
        protected void RunProjectiles()
        {
            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Run();
            }
        }
        protected void DamagePlayer()
        {
            if (player.canDamaged == true)
            {
                player.Health -= 1;
                if (player.Health <= 0)
                {
                    gameStatus = GameStatus.LOSE;
                }
                else
                {
                    player.ResetPlayer();
                }
            }
        }
        protected void Interaction_PlayerWithWalls(int temp_x, int temp_y)
        {
            for (int i = 0; i < walls.Count; i++)
                if (
                    PlayerIsCloseToWall(i)
                    )
                {
                    player.X = temp_x;
                    player.Y = temp_y;
                    player.moving_direction = Direction.STOP;
                }
        }
        protected void Interaction_EnemiesWithEnemies()
        {
            for (int i = 0; i < simpleTanks.Count - 1; i++)
                for (int j = i + 1; j < simpleTanks.Count; j++)
                    if (
                        SimpleTankIsCloseToSimpleTank(i, j)
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
                        SimpleTankIsCloseToHunterTank(i, j)
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
                        HunterTankIsCloseToHunterTank(i, j)
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
        protected void Interaction_PlayerWithEnemies()
        {
            for (int i = 0; i < simpleTanks.Count; i++)
                if (Math.Abs(player.X - simpleTanks[i].X) <= sizeTank && Math.Abs(player.Y - simpleTanks[i].Y) <= sizeTank)
                    DamagePlayer();

            for (int i = 0; i < hunterTanks.Count; i++)
                if (Math.Abs(player.X - hunterTanks[i].X) <= sizeTank && Math.Abs(player.Y - hunterTanks[i].Y) <= sizeTank)
                    DamagePlayer();
        }
        protected void RunEnemies()
        {
            for (int i = 0; i < simpleTanks.Count; i++)
            {
                simpleTanks[i].Run();
            }

            for (int i = 0; i < hunterTanks.Count; i++)
            {
                hunterTanks[i].Run(player.X, player.Y);
            }
        }
        protected void Interaction_ProjectilesWithObjects()
        {
            for (int i = 0; i < projectiles.Count; i++)
                if (projectiles[i].ended == true)
                    projectiles.RemoveAt(i);

            for (int i = 0; i < projectiles.Count; i++)
                if
                    ((projectiles[i].safeProjectile > 5 &&
                    projectiles[i].X > player.X - sizeProjectile) &&
                    (projectiles[i].X < player.X + sizeTank) &&
                    (projectiles[i].Y > player.Y - sizeProjectile) &&
                    (projectiles[i].Y < player.Y + sizeTank))
                {
                    try
                    {
                        projectiles.RemoveAt(i);
                        DamagePlayer();
                    }
                    catch
                    {
                        DamagePlayer();
                    }
                }

            for (int i = 0; i < projectiles.Count; i++)
                for (int j = 0; j < walls.Count; j++)
                {
                    if
                        (projectiles[i].throughWalls == false &&
                        (projectiles[i].X > walls[j].X - sizeProjectile) &&
                        (projectiles[i].X < walls[j].X + sizeWall) &&
                        (projectiles[i].Y > walls[j].Y - sizeProjectile) &&
                        (projectiles[i].Y < walls[j].Y + sizeWall))
                    {
                        walls[j].CurrentHealth -= 1;
                        if (walls[j].CurrentHealth <= 0)
                            walls.RemoveAt(j);
                        projectiles.RemoveAt(i);
                        break;
                    }
                }
            for (int i = 0; i < projectiles.Count; i++)
            {
                for (int j = 0; j < projectiles.Count; j++)
                {
                    if
                        (i != j &&
                        (projectiles[i].X > projectiles[j].X - sizeProjectile) &&
                        (projectiles[i].X < projectiles[j].X + sizeProjectile) &&
                        (projectiles[i].Y > projectiles[j].Y - sizeProjectile) &&
                        (projectiles[i].Y < projectiles[j].Y + sizeProjectile))
                    {
                        try
                        {
                            projectiles.RemoveAt(i);
                            if (j < i)
                                projectiles.RemoveAt(j);
                            else
                                projectiles.RemoveAt(j - 1);
                        }
                        catch
                        {

                        }
                    }
                }
            }

            for (int i = 0; i < simpleTanks.Count; i++)
            {
                for (int j = 0; j < projectiles.Count; j++)
                {
                    if
                        (projectiles[j].safeProjectile > 5 &&
                        (projectiles[j].X > simpleTanks[i].X - sizeProjectile) &&
                        (projectiles[j].X < simpleTanks[i].X + sizeTank) &&
                        (projectiles[j].Y > simpleTanks[i].Y - sizeProjectile) &&
                        (projectiles[j].Y < simpleTanks[i].Y + sizeTank))
                    {
                        try
                        {
                            score.Increment();
                            simpleTanks.RemoveAt(i);
                            projectiles.RemoveAt(j);
                            if (i != 0)
                                i--;
                            if (j != 0)
                                j--;
                        }
                        catch
                        {

                        }
                    }
                }
            }

            for (int i = 0; i < hunterTanks.Count; i++)
            {
                for (int j = 0; j < projectiles.Count; j++)
                {
                    try
                    {
                        if
                            (projectiles[j].safeProjectile > 5 &&
                            (projectiles[j].X > hunterTanks[i].X - sizeProjectile) &&
                            (projectiles[j].X < hunterTanks[i].X + sizeTank) &&
                            (projectiles[j].Y > hunterTanks[i].Y - sizeProjectile) &&
                            (projectiles[j].Y < hunterTanks[i].Y + sizeTank))
                        {
                            score.Increment();
                            hunterTanks.RemoveAt(i);
                            projectiles.RemoveAt(j);
                            if (i != 0)
                                i--;
                            if (j != 0)
                                j--;
                        }
                    }
                    catch
                    {

                    }
                }
            }

        }
        protected void checkHunterTanksInteractWalls()
        {
            for (int i = 0; i < hunterTanks.Count; i++)
            {
                for (int j = 0; j < walls.Count; j++)
                {
                    if (
                        HunterTankIsCloseToWall(i, j)
                    )
                    {
                        hunterTanks[i].X = hunterTanks[i].Prev_x;
                        hunterTanks[i].Y = hunterTanks[i].Prev_y;
                        hunterTanks[i].TurnAround();
                    }
                }
            }
        }
        protected void checkSimpleTanksInteractWalls()
        {
            for (int i = 0; i < simpleTanks.Count; i++)
            {
                for (int j = 0; j < walls.Count; j++)
                {
                    if (
                        SimpleTankIsCloseToWall(i, j)
                    )
                    {
                        simpleTanks[i].X = simpleTanks[i].Prev_x;
                        simpleTanks[i].Y = simpleTanks[i].Prev_y;
                        simpleTanks[i].TurnAround();
                    }
                }
            }
        }

        private bool HunterTankIsCloseToWall(int i, int j)
        {
            if (
                     (Math.Abs(hunterTanks[i].X - walls[j].X) <= sizeTank && Math.Abs(hunterTanks[i].Y - walls[j].Y) <= sizeTank)
                     ||
                     ((hunterTanks[i].X - walls[j].X) <= sizeWall && (Math.Abs(hunterTanks[i].Y - walls[j].Y) <= sizeTank) && hunterTanks[i].X > walls[j].X)
                     ||
                     (((hunterTanks[i].X - walls[j].X <= sizeWall && hunterTanks[i].X - walls[j].X > 0) || (walls[j].X - hunterTanks[i].X <= sizeTank && walls[j].X - hunterTanks[i].X > 0)) && (Math.Abs(hunterTanks[i].Y - walls[j].Y) <= sizeWall) && hunterTanks[i].Y > walls[j].Y)
                     )
                return true;
            else
                return false;
        }
        private bool SimpleTankIsCloseToSimpleTank(int i, int j)
        {
            if (
                    (Math.Abs(simpleTanks[i].X - simpleTanks[j].X) <= sizeTank+1 && (simpleTanks[i].Y == simpleTanks[j].Y))
                        ||
                        (Math.Abs(simpleTanks[i].Y - simpleTanks[j].Y) <= sizeTank+1 && (simpleTanks[i].X == simpleTanks[j].X))
                        ||
                        (Math.Abs(simpleTanks[i].X - simpleTanks[j].X) <= sizeTank + 1 && Math.Abs(simpleTanks[i].Y - simpleTanks[j].Y) <= sizeTank+1)
                            )
                return true;
            else
                return false;
        }
        private bool SimpleTankIsCloseToHunterTank(int i, int j)
        {
            if (
                    (Math.Abs(simpleTanks[i].X - hunterTanks[j].X) <= sizeTank+1 && (simpleTanks[i].Y == hunterTanks[j].Y))
                        ||
                        (Math.Abs(simpleTanks[i].Y - hunterTanks[j].Y) <= sizeTank+1 && (simpleTanks[i].X == hunterTanks[j].X))
                        ||
                        (Math.Abs(simpleTanks[i].X - hunterTanks[j].X) <= sizeTank + 1 && Math.Abs(simpleTanks[i].Y - hunterTanks[j].Y) <= sizeTank+1)
                            )
                return true;
            else
                return false;
        }
        private bool HunterTankIsCloseToHunterTank(int i, int j)
        {
            if (
                    (Math.Abs(hunterTanks[i].X - hunterTanks[j].X) <= sizeTank+1 && (hunterTanks[i].Y == hunterTanks[j].Y))
                        ||
                        (Math.Abs(hunterTanks[i].Y - hunterTanks[j].Y) <= sizeTank+1 && (hunterTanks[i].X == hunterTanks[j].X))
                        ||
                        (Math.Abs(hunterTanks[i].X - hunterTanks[j].X) <= sizeTank + 1 && Math.Abs(hunterTanks[i].Y - hunterTanks[j].Y) <= sizeTank+1)
                            )
                return true;
            else
                return false;
        }
        private bool SimpleTankIsCloseToWall(int i, int j)
        {
            if (
                     (Math.Abs(simpleTanks[i].X - walls[j].X) <= sizeTank && Math.Abs(simpleTanks[i].Y - walls[j].Y) <= sizeTank)
                    ||
                    ((simpleTanks[i].X - walls[j].X) <= sizeWall && (Math.Abs(simpleTanks[i].Y - walls[j].Y) <= sizeTank) && simpleTanks[i].X > walls[j].X)
                    ||
                    (((simpleTanks[i].X - walls[j].X <= sizeWall && simpleTanks[i].X - walls[j].X > 0) || (walls[j].X - simpleTanks[i].X <= sizeTank && walls[j].X - simpleTanks[i].X > 0)) && (Math.Abs(simpleTanks[i].Y - walls[j].Y) <= sizeWall) && simpleTanks[i].Y > walls[j].Y)
                     )
                return true;
            else
                return false;
        }
        private bool PlayerIsCloseToWall(int i)
        {
            if (
            (Math.Abs(player.X - walls[i].X) <= sizeTank && Math.Abs(player.Y - walls[i].Y) <= sizeTank)
                    ||
                    ((player.X - walls[i].X) <= sizeWall && (Math.Abs(player.Y - walls[i].Y) <= sizeTank) && player.X > walls[i].X)
                    ||
                    (((player.X - walls[i].X <= sizeWall && player.X - walls[i].X > 0) || (walls[i].X - player.X <= sizeTank && walls[i].X - player.X > 0)) && (Math.Abs(player.Y - walls[i].Y) <= sizeWall) && player.Y > walls[i].Y)
            )
                return true;
            else
                return false;
        }

    }
}
