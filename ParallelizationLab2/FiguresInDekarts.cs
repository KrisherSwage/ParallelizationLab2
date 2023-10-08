using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelizationLab2
{
    internal class FiguresInDekarts
    {
        //уравнение функции записывается в декартовой системе координат
        public static double DecarCoordBirdFunk(double x, double y)
        {
            double opOne = -1 * Math.Pow(x * x + y * y, 0.5);
            double opTwo = 4;
            double opThree = (x * x - y * y) / (x * x + y * y);

            double opFour = (4.5 * x) / Math.Pow(x * x + y * y, 0.5);
            double opFive = (4 * x * y) / (x * x + y * y);
            double opSix = (x * x - y * y) / (x * x + y * y);

            double opGeneric3 = opFour * opFive * opSix;

            double sumOfOperands = opOne + opTwo + opThree + opGeneric3;

            return sumOfOperands;
        }

        public static double InfinityGrapgFunk(double x, double y) // S = 4,71
        {
            double opOne = -1 * Math.Pow(x * x + y * y, 0.5);
            double opTwo = 2 * Math.Pow((x / (Math.Pow(x * x + y * y, 0.5))), 2);
            return opOne + opTwo;
        }

        public static double CircleFunk(double x, double y) // S = 153.86
        {
            //круг с центром (10;20) и радиусом 7
            double opOne = -1 * Math.Pow(x, 2);
            double opTwo = -1 * Math.Pow(y, 2);
            double opThree = 7 * 7;

            double sumOfOperands = opOne + opTwo + opThree;

            return sumOfOperands;
        }
    }
}
