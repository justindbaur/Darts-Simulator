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
                    if (Players[i].Score > 0)
                    {
                        continue;
                    }
                    else
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


            if (GameOfDarts.ShowTurn)
            {
                Console.WriteLine($"Player {PlayerNum} throwing darts.");
            }

            for (int i = 0; i < listOfDarts.Count; i++)
            {
                listOfDarts[i].ThrowDart();
            }
            if (GameOfDarts.ShowTurn)
            {
                for (int i = 0; i < listOfDarts.Count; i++)
                {
                    Console.WriteLine($"Dart {i + 1}: Score: {listOfDarts[i].Score} | Region: {listOfDarts[i].ShotRegion}");
                }
            }

            if (!FinishStart)
            {
                for (int i = 0; i < listOfDarts.Count; i++)
                {
                    if (listOfDarts[i].IsDouble())
                    {
                        FinishStart = true;
                        break;
                    }
                }

                if (FinishStart)
                {
                    int turnScore = 0;

                    for (int i = 0; i < listOfDarts.Count; i++)
                    {
                        turnScore += listOfDarts[i].Score;
                    }
                    Score =- turnScore;
                    Console.WriteLine($"Double was gotten. Player {PlayerNum} new score is {Score}");
                }
                else
                {
                    Console.WriteLine($"Double was not gotten. Score is still {Score}");
                }
            }
            else
            {
                int tempScore = 0;

                for (int i = 0; i < listOfDarts.Count; i++)
                {
                    tempScore += listOfDarts[i].Score;
                }

                if (Score - tempScore <= 0)
                {
                    bool doubleGot = false;

                    for (int i = 0; i < listOfDarts.Count; i++)
                    {
                        if(listOfDarts[i].IsDouble())
                        {
                            doubleGot = true;
                            break;
                        }
                    }

                    if (doubleGot)
                    {
                        Score = -tempScore;
                        return;
                    }
                }
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
        public int Score { get; set; }

        public enum Region
        {
            None,
            Single,
            Double,
            Triple
        }

        public void ThrowDart()
        {
            Random rnd = new Random();
            this.Shot = rnd.Next(0, 21);

            if (this.Shot == 0)
            {
                Score = 0;
                return;
            }
            else if (this.Shot == 21)
            {
                this.ShotRegion = (Region)rnd.Next(1, 2);
                Score = 25 * (int)this.ShotRegion;
            }
            else
            {
                this.ShotRegion = (Region)rnd.Next(1, 3);
                Score =  this.Shot * (int)this.ShotRegion;
                return;
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
