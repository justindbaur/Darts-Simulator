using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartBoard
{
    public class GameOfDarts
    {
        public static List<Player> Players { get; set; }
        public static bool ShowTurn { get; set; }

        public static void StartGame(int startingScore)
        {
            Players = new List<Player>()
            {
                new Player { PlayerNum = 1, Score = startingScore},
                new Player { PlayerNum = 1, Score = startingScore}
            };

            while (Players.FindAll(p => p.Score >= 0).Count == Players.Count)
            {
                for (int i = 0; i < Players.Count; i++)
                {
                    Players[i].DoTurn();
                    if (Players[i].Score <= 0)
                    {
                        break;
                    }

                }
            }
        }
    }
    public class Player
    {
        public int PlayerNum { get; set; }
        public int Score { get; set; }
        public bool FinishStart { get; set; }

        public void DoTurn()
        {
            var listOfDarts = new List<Dart>()
            {
                new Dart{ },
                new Dart{ },
                new Dart{ }
            };
            var listOfScores = new List<int>();

            for (int i = 0; i < listOfDarts.Count; i++)
            {
                int dartScore = listOfDarts[i].GetScore();
                if (GameOfDarts.ShowTurn)
                {
                    Console.WriteLine($"Dart {i +1}: Score: {dartScore} | Region: {listOfDarts[i].ShotRegion}");
                }
            }

            if (!FinishStart)
            {
                for (int i = 0; i < listOfDarts.Count; i++)
                {
                    if (listOfDarts[i].IsDouble())
                    {
                        FinishStart = true;
                    }
                }
                if (FinishStart)
                {
                    int turnScore;

                    for (int i = 0; i < listOfDarts.Count; i++)
                    {

                    }
                }
                Console.WriteLine();
            }
        }

        public Player()
        {
            FinishStart = false;
        }

    }
    public class Dart
    {
        public int Shot { get; set; }
        public Region ShotRegion { get; set; }
        
        public enum Region
        {
            None,
            Single,
            Double,
            Triple
        }

        public int GetScore()
        {
            Random rnd = new Random();
            this.Shot = rnd.Next(0, 21);

            if (this.Shot == 0)
            {
                return 0;
            }
            else if (this.Shot > 0)
            {
                this.ShotRegion = (Region)rnd.Next(1, 3);
                if (this.Shot == 21 && this.ShotRegion == Region.Single)
                {
                    return 25;
                }
                else
                {
                    return 50;
                }
            }
            else
            {
                return this.Shot * ((int)this.ShotRegion + 1);
            }
        }

        public bool IsDouble()
        {
            if (this.Shot == 21)
            {
                return this.ShotRegion >= Region.Double ? true : false;
            }
            else if (this.ShotRegion == Region.Double)
            {
                return true;
            }
            return false;
        }
    }
}
