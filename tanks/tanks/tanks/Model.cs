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
        public Tank tank;
        public Wall wall;

        public GameStatus gameStatus;

        public Model(int amountTanks, int speedGame, int amountWalls)
        {
            this.amountTanks = amountTanks;
            this.speedGame = speedGame;
            this.amountWalls = amountWalls;

            tank = new Tank();

            wall = new Wall();
            gameStatus = GameStatus.STOP;
        }

        public void Play()
        {
            while (gameStatus == GameStatus.PLAY)
            {
                Thread.Sleep(speedGame);
                tank.Run();
                tank.Turn();
            }
        }
    }
}
