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
            GameOfDarts.ShowTurn = true;
            GameOfDarts.StartGame(301);

            Console.ReadLine();
        }
    }
}
