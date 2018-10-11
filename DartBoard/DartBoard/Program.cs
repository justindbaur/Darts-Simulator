using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartBoard
{
    class Program
    {
        static void Main(string[] args)
        {
            Dart.DartRand = new Random();
            GameOfDarts.ShowDart = true;
            GameOfDarts.RoundPause = false;
            GameOfDarts.StartingScore = 301;
            GameOfDarts.StartGame();
            Console.ReadLine();
        }
    }
}
