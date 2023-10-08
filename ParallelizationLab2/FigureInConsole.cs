using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelizationLab2
{
    internal class FigureInConsole //этот класс был написан для пробы. Он не используется в основной программе
    {
        public static void ConsolePrintArr()
        {
            bool[,] pixels = ArrMyFunk();

            for (int i = pixels.GetLength(0) - 1; i >= 0; i--)
            {
                for (int j = 0; j < pixels.GetLength(1); j++)
                {
                    //тут надоследить, чтобы раскраска в консоли не поменялась местами
                    if (pixels[j, i]) //если правда, то 8
                    {
                        Console.Write("88");
                    }
                    else //если ложь
                    {
                        Console.Write(". ");
                    }
                }
                Console.WriteLine();
            }
        }

        private static bool[,] ArrMyFunk()
        {
            bool[,] pixels = new bool[100, 100];

            int ii = 0;
            for (int i = (-1 * pixels.GetLength(0)) / 2; i < pixels.GetLength(0) / 2; i++)
            {
                int jj = 0;
                for (int j = (-1 * pixels.GetLength(1)) / 2; j < pixels.GetLength(1) / 2; j++)
                {
                    //выбор фигуры тут через код. Не очень удобно, но это я делал только для тренировки/проверки
                    if (FiguresInDekarts.DecarCoordBirdFunk(i / 5.0, j / 5.0) >= 0 || (i == 0 && j == 0))
                    {
                        pixels[ii, jj] = true;
                    }
                    jj++;
                }
                ii++;
            }

            return pixels;
        }
    }
}
