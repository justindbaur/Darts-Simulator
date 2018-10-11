using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartBoard
{
    //Player class
    public class Player
    {
        //The player number, could be name or anything, not extensively used for logic
        public int PlayerNum { get; set; }
        //Their current score, will be set at the start of a game with the game rule score.
        public int Score { get; set; }
        //Boolean to see if they have doubled into the game, I want to work it out of the program and rely on if their score is equal to the starting score.
        public bool FinishStart { get; set; }
        //Skill enum
        public Skill MySkill { get; set; }

        //Run a turn based on a three dart throw
        public void RunTurn()
        {
            //Creates a temp score. Can probably be gotten rid of.
            int tempPlayerScore = Score;

            //Creates a list of darts
            var listOfDarts = new List<Dart>();

            if (GameOfDarts.ShowDart)
            {
                Console.WriteLine($"Player {PlayerNum} throwing darts. Starting Score: {Score}");
            }

            //Setting the current turns running score.
            int curTurnScore = 0;

            for (int i = 0; i < 3; i++)
            {
                listOfDarts.Add(new Dart());

                listOfDarts.Last().ThrowRandomDart();

                if (GameOfDarts.ShowDart)
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
                //Holds a temp score
                int tempScore = 0;

                //Gets the overall turn score.
                for (int i = 0; i < listOfDarts.Count; i++)
                {
                    tempScore += listOfDarts[i].Score;
                }

                //Does math on turn outcome
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

        public void DoTurn()
        {
            int curTurnScore = 0;

            var turnDarts = new List<Dart>();

            //Prints out player info
            Print();

            //
            for (int i = 0; i < 3; i++)
            {
                //Will create a dart;
                turnDarts.Add(new Dart());

                if (Score == GameOfDarts.StartingScore || Score < 60)
                {
                    turnDarts.Last().Seeking = Seeking.Double;
                }
                else
                {
                    turnDarts.Last().Seeking = Seeking.HighScore;
                }

                turnDarts[i].ThrowSkilledDart(MySkill);

                //Sees if the score is still the starting score and this is the first dart.
                if (Score == GameOfDarts.StartingScore && i == 0)
                {
                    //Checks if the first dart is a double
                    if (turnDarts[0].ShotRegion != Region.Double)
                    {
                        //Will show dart info if true
                        if (GameOfDarts.ShowDart)
                        {
                            //Will print dart information if needed.
                            turnDarts[0].Print(1);
                        }
                        //Prints turn outcome no matter what
                        Console.WriteLine($"Player {PlayerNum} could not double in. Turn ended.");
                        return;
                    }
                }

                //Adds the current dart score 
                curTurnScore += turnDarts[i].Score;

                if ((Score - curTurnScore) < 0)
                {
                    if (GameOfDarts.ShowDart)
                    {
                        turnDarts[i].Print(i + 1);
                    }
                    Console.WriteLine($"Player {PlayerNum} score less than 0, turn ended.");
                    Print();
                    return;
                }
                else if ((Score - curTurnScore) == 1)
                {
                    if (GameOfDarts.ShowDart)
                    {
                        turnDarts[i].Print(i + 1);
                    }
                    Console.WriteLine("Impossible to win from score of 1. Turn ended.");
                    Print();
                    return;
                }
                else if ((Score - curTurnScore) == 0)
                {
                    if (GameOfDarts.ShowDart)
                    {
                        turnDarts[i].Print(i + 1);
                    }

                    if (turnDarts[i].ShotRegion == Region.Double)
                    {
                        Score = 0;
                        Console.WriteLine($"Player {PlayerNum} won!");
                        return;
                    }
                    else
                    {
                        Console.WriteLine($"Player {PlayerNum} must end on double. Turn ended.");
                        Print();
                        return;
                    }
                }
                else
                {
                    if (GameOfDarts.ShowDart)
                    {
                        turnDarts[i].Print(i + 1);
                    }
                }
            }

            if (GameOfDarts.ShowDart)
            {
                Console.WriteLine($"Player {PlayerNum} turn score: {curTurnScore}");
            }

            Score -= curTurnScore;

            Print();
        }

        public void Print()
        {
            Console.WriteLine($"Player {PlayerNum} Score: {Score}");
        }
    }
    public enum Skill
    {
        None,
        Low,
        Medium,
        High
    }
}
