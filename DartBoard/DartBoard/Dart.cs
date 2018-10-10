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
        public int Score { get; set; }
        public static Random DartRand { get; set; }

        public enum Region
        {
            None,
            Single,
            Double,
            Triple
        }

        //Method to throw the dart and get its score and landing location.
        //Needs some work to better reflect the goal and possibilities in a game.
        public void ThrowDart()
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

        //Will return true if the shot region is double.  Can be worked out of the program in time.
        public bool IsDouble()
        {
            return ShotRegion == Region.Double ? true : false;
        }
    }
}
