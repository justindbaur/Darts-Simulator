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
        public static bool RoundPause { get; set; }

        public static void StartGame(int startingScore)
        {
            //Creating two players, 1 and 2

            Players = new List<Player>()
            {
                new Player { PlayerNum = 1, Score = startingScore},
                new Player { PlayerNum = 2, Score = startingScore}
            };
            //Starting a int for round
            int r = 1;

            //Runs while neither player have a score of 0
            while (Players.FindAll(p => p.Score > 0).Count == Players.Count)
            {
                //Prints round number
                Console.WriteLine($"Round {r}:");

                //Iterates over players
                for (int i = 0; i < Players.Count; i++)
                {
                    //Runs player turn
                    Players[i].RunTurn();

                    Console.WriteLine();

                    //Does quick check if a players score is 0, would like to get rid of this
                    if (Players[i].Score > 0)
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }

                if (RoundPause)
                {
                    string response = Console.ReadLine();
                    if (response.ToUpper() == "SKIP")
                    {
                        RoundPause = false;
                    }
                }
                //Add one to round counter
                r++;
            }
        }
    }
}
