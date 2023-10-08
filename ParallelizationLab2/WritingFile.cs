using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelizationLab2
{
    internal class WritingFile
    {
        public static void CreateCSV(double[,] myData)
        {
            string pathWriteData = Path.Combine(Environment.CurrentDirectory, "ParallionLab2.csv");

            using (StreamWriter sw = new StreamWriter(pathWriteData, true, Encoding.UTF8))
            {
                for (int i = 0; i < myData.GetLength(0); i++)
                {
                    for (int j = 0; j < myData.GetLength(1); j++)
                    {
                        sw.Write($"{myData[i, j]};");
                    }
                    sw.WriteLine();
                }
            }
        }

    }
}
