using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelizationLab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int enumRanomNunbers = 90000000; //90000000 - на 30 секунд
            //FigureInConsole.ConsolePrintArr(); //функцию в консоль вывести

            MonteKarloAreaFigure mkMethod = new MonteKarloAreaFigure(10, 10); //(300000000) 1 п = 78,454 сек

            var tableResults = new double[14, 12]; //табличка результатов

            int ii = 0;
            for (int i = 0; i < 12; i++) //для потоков
            {
                for (int j = 0; j < 10; j++)
                {
                    var timeAndArea = mkMethod.StartMethod(enumRanomNunbers, i + 1);

                    tableResults[j, ii] = timeAndArea[0]; //каждое конкретное время
                    tableResults[13, ii] += timeAndArea[1] / 10.0;
                }
                tableResults[12, ii] = i + 1; //количество потоков
                ii++;
            }

            WritingFile.CreateCSV(tableResults);
        }
    }
}
