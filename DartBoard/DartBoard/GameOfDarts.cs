using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartBoard
{
    public class GameOfDarts
    {
        //List of the players playing the game.
        public static List<Player> Players { get; set; }
        //Lets you choose if you want all the information or just the summaries
        public static bool ShowDart { get; set; }
        //Will let you choose if you want to pause after each round
        public static bool RoundPause { get; set; }

        public static int StartingScore { get; set; }

        //Starts the game of darts. Lets you choose the starting score.
        public static void StartGame()
        {
            //Creating two players, 1 and 2

            Players = new List<Player>()
            {
                new Player { PlayerNum = 1, Score = StartingScore, MySkill = Skill.Medium},
                new Player { PlayerNum = 2, Score = StartingScore, MySkill = Skill.High}
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
                    Players[i].DoTurn();

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

                //Will pause after a round has been completed.
                if (RoundPause)
                {
                    //Lets you choose to skip through rounds after the game has already started.
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
