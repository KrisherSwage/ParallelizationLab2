using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelizationLab2
{
    internal class MonteKarloAreaFigure
    {
        private static double xCoord;
        private static double yCoord;
        private static double allArea;

        private static long amountRndNumbers;
        private static long amountPixels;

        private static int fluxes;

        private static List<Random> rndList = new List<Random>(); // !!! крайне важный список !!!

        public MonteKarloAreaFigure(double x, double y)
        {
            xCoord = x;
            yCoord = y;

            allArea = Math.Abs(xCoord * 2) * Math.Abs(yCoord * 2); //площадь четырехугольника
        }

        private void InitialConditions(int N, int fl)
        {
            amountRndNumbers = N;
            fluxes = fl;

            for (int i = 0; i < fluxes; i++)
            {
                rndList.Add(new Random());
            }
        }

        public double[] StartMethod(int N, int fl) //запускаем из конструктора
        {
            InitialConditions(N, fl);

            Task<long>[] tasks = new Task<long>[fluxes]; //массив для созданных потоков

            long enumForOneTask = amountRndNumbers / fluxes;

            Stopwatch clock = new Stopwatch();
            clock.Start();

            for (int i = 0; i < fluxes; i++)
            {
                tasks[i] = Task.Run(() => Experiments(enumForOneTask)); //создаем и запускаем новый поток с функцией расчета
            }

            Task.WaitAll(tasks); //ждем выполнения всех потоков

            foreach (var task in tasks) //считаем итоговую площадь
                amountPixels += task.Result;

            clock.Stop();

            double timeRes = clock.ElapsedMilliseconds / 1000.0;
            double resultArea = allArea * (Convert.ToDouble(amountPixels) / Convert.ToDouble(amountRndNumbers));
            //Console.WriteLine($"время = {timeRes} сек;\tпотоков - {fluxes};\tплощадь = {resultArea}");

            FinalConditions();

            return new double[] { timeRes, resultArea };
        }

        private void FinalConditions()
        {
            rndList.Clear();
            amountPixels = 0;
        }

        private static long Experiments(long amountOfExpts)
        {
            long localCounter = 0;

            for (long i = 0; i < amountOfExpts; i++)
            {
                var localCoords = GenerationRndCoortes();

                if (PixelInFigure(localCoords))
                    localCounter++;
            }

            return localCounter;
        }

        private static double[] GenerationRndCoortes()
        {
            //проблема многопоточности в ссылке на элемент rnd (lock (rnd) - сильно замедляет)
            int taskNumber = Convert.ToInt32(Task.CurrentId) % rndList.Count; // !!! крайне важная строка !!!

            double rndCoorX = rndList[taskNumber].NextDouble() * (xCoord - (-1 * xCoord)) + (-1 * xCoord);
            double rndCoorY = rndList[taskNumber].NextDouble() * (yCoord - (-1 * yCoord)) + (-1 * yCoord);

            var coorXandY = new double[] { rndCoorX, rndCoorY };
            return coorXandY;
        }

        private static bool PixelInFigure(double[] crdPixel)
        {
            double flagPixelInFunc = FiguresInDekarts.DecarCoordBirdFunk(crdPixel[0], crdPixel[1]); //тут выбираем функцию

            if (flagPixelInFunc >= 0)
                return true; //если точка внутри функции
            else
                return false;
        }
    }
}
