using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartBoard
{
    //Dart class for holding dart information
    public class Dart
    {
        public int Shot { get; set; }
        public Region ShotRegion { get; set; }
        public Seeking Seeking { get; set; }
        public int Score { get; set; }
        public static Random DartRand { get; set; }

        

        //Method to throw the dart and get its score and landing location.
        //Needs some work to better reflect the goal and possibilities in a game.
        public void ThrowRandomDart()
        {
            //Create a number between 0 and 22
            this.Shot = DartRand.Next(0, 21);

            //Checks if the shot was a miss.
            if (this.Shot == 0)
            {
                Score = 0;
            }
            else if (this.Shot == 21) //checks if the shot was a bullseye
            {
                //Gets a single or double region for a bullseye
                this.ShotRegion = (Region)DartRand.Next(1, 2);
                Score = 25 * (int)this.ShotRegion;
            }
            else
            {
                this.ShotRegion = (Region)DartRand.Next(1, 3);
                Score = this.Shot * (int)this.ShotRegion;
            }
        }

        //Method for throwing a skilled dart
        public void ThrowSkilledDart(Skill skill = Skill.None)
        {
            double scoreMult = 1;
            double skillMult = 1;
            Region regionGoal;

            switch (Seeking)
            {
                case Seeking.None:
                    ThrowRandomDart();
                    return;
                case Seeking.HighScore:
                    scoreMult = 1.13;
                    regionGoal = Region.Triple;
                    break;
                case Seeking.Double:
                    scoreMult = .99;
                    regionGoal = Region.Double;
                    break;
                default:
                    ThrowRandomDart();
                    return;
            }

            switch (skill)  
            {
                case Skill.Low:
                    skillMult = 1.01;
                    break;
                case Skill.Medium:
                    skillMult = 1.07;
                    break;
                case Skill.High:
                    skillMult = 1.1;
                    break;
                default:
                    ThrowRandomDart();
                    return;
            }

            Shot = (int)Math.Floor((DartRand.Next(0, 21) * scoreMult * skillMult));

            if (Shot == 0)
            {
                Score = 0;
            }
            else if (Shot >= 21)
            {
                Shot = 25;
                ShotRegion = (Region)Math.Floor((DartRand.Next(1,2) * scoreMult * skillMult));

                Score = Shot * (int)ShotRegion;
            }
            else
            {
                ShotRegion = (Region)Math.Floor((DartRand.Next(1, 3) * scoreMult * skillMult));

                if (ShotRegion != regionGoal)
                {
                    ShotRegion = (Region)Math.Floor((DartRand.Next(1, 3) * scoreMult * skillMult));
                }

                Score = Shot * (int)ShotRegion;
            }
        }

        //Will return true if the shot region is double.  Can be worked out of the program in time.
        public bool IsDouble()
        {
            return ShotRegion == Region.Double ? true : false;
        }

        public void Print(int num)
        {
            Console.WriteLine($"Dart: {num} | Shot: {Shot} | Region: {ShotRegion} | Score: {Score}");
        }
        public void Print()
        {
            Console.WriteLine($"Shot: {Shot} \tRegion: {ShotRegion} \tScore: {Score}");
        }
    }

    public enum Region
    {
        None,
        Single,
        Double,
        Triple
    }

    public enum Seeking
    {
        None,
        HighScore,
        Double
    }
}
