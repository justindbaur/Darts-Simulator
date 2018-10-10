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
                new Player { PlayerNum = 2, Score = startingScore}
            };
            int r = 1;
            while (Players.FindAll(p => p.Score > 0).Count == Players.Count)
            {
                Console.WriteLine($"Round {r}:");
                for (int i = 0; i < Players.Count; i++)
                {
                    Players[i].RunTurn();
                    Console.WriteLine();
                    
                    if (Players[i].Score > 0)
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                //Console.ReadLine();
                r++;
            }
        }
    }
    public class Player
    {
        public int PlayerNum { get; set; }
        public int Score { get; set; }
        public bool FinishStart { get; set; }


        public void RunTurn()
        {
            int tempPlayerScore = Score;

            var listOfDarts = new List<Dart>();

            if (GameOfDarts.ShowTurn)
            {
                Console.WriteLine($"Player {PlayerNum} throwing darts. Starting Score: {Score}");
            }
            //Setting the current turns running score.
            int curTurnScore = 0;

            for (int i = 0; i < 3; i++)
            {
                listOfDarts.Add(new Dart());

                listOfDarts.Last().ThrowDart();

                if (GameOfDarts.ShowTurn)
                {
                    Console.WriteLine($"Dart {i + 1}: Shot: {listOfDarts.Last().Shot} | Region: {listOfDarts.Last().ShotRegion} | Score: {listOfDarts.Last().Score}");
                }

                curTurnScore += listOfDarts.Last().Score;

                if ((tempPlayerScore - curTurnScore) == 0)
                {
                    if (listOfDarts.Last().IsDouble())
                    {
                        Console.WriteLine($"Player {PlayerNum} won.");
                        Score = tempPlayerScore - curTurnScore;
                        return;
                    }
                    else
                    {
                        Console.WriteLine($"Player {PlayerNum} can't win");
                        return;
                    }
                }
                else if ((tempPlayerScore - curTurnScore) < 0)
                {
                    Console.WriteLine($"Player {PlayerNum} got less than 0, turn ended.");
                    return;
                }
            }

            if (!FinishStart)
            {
                //If the first dart is a double they have doubled in.
                if (listOfDarts[0].IsDouble())
                {
                    FinishStart = true;
                }

                //If they have finished the start.  Calculate their score.
                if (FinishStart)
                {
                    int turnScore = 0;

                    //Combine all darts scores
                    for (int i = 0; i < listOfDarts.Count; i++)
                    {
                        turnScore += listOfDarts[i].Score;
                    }

                    //If their score is less than 0, end their turn.
                    if ((tempPlayerScore - turnScore) < 0)
                    {
                        Console.WriteLine($"Turn score is less than 0. Player {PlayerNum} turn ended.");
                    }
                    else
                    {
                        Score -= turnScore;
                        Console.WriteLine($"Double was gotten. Player {PlayerNum} new score is {Score}");
                    }
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

                if ((Score - tempScore) == 0)
                {
                    bool doubleGot = false;

                    if (listOfDarts[listOfDarts.Count - 1].IsDouble())
                    {
                        doubleGot = true;
                    }

                    if (doubleGot)
                    {
                        Score -= tempScore;
                        Console.WriteLine($"Double was got. Player {PlayerNum} Won!");
                        return;
                    }
                    else
                    {
                        Console.WriteLine($"Double was needed and not gotten. Player {PlayerNum} score is still {Score}");
                    }
                }
                else if ((Score - tempScore) <= 1)
                {
                    Console.WriteLine($"Player {PlayerNum} went less than 1, score is still {Score}");
                }
                else
                {
                    Score -= tempScore;
                }

                Console.WriteLine($"Player {PlayerNum} new score is {Score}");
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
        public static Random DartRand { get; set; }

        public Dart()
        {
            //DartRand = new Random();

        }

        public enum Region
        {
            None,
            Single,
            Double,
            Triple
        }

        public void ThrowDart()
        {
            this.Shot = DartRand.Next(0, 21);

            if (this.Shot == 0)
            {
                Score = 0;
                return;
            }
            else if (this.Shot == 21)
            {
                this.ShotRegion = (Region)DartRand.Next(1, 2);
                Score = 25 * (int)this.ShotRegion;
            }
            else
            {
                this.ShotRegion = (Region)DartRand.Next(1, 3);
                Score = this.Shot * (int)this.ShotRegion;
                return;
            }
        }

        public bool IsDouble()
        {
            if (this.Shot == 21)
            {
                return this.ShotRegion == Region.Double ? true : false;
            }
            else if (this.ShotRegion == Region.Double)
            {
                return true;
            }
            return false;
        }
    }
}
