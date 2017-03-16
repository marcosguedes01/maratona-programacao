//#define TEST

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace _002Cartas
{
    class Program
    {
        #region Constantes
        private const string PATH_FILE_OUTPUT = "./Output.txt";
        private const string MSG_DONE = "Done";
        private const char SPACE = ' ';
        #endregion

        static void Main(string[] args)
        {
            FileStream ostrm;
            StreamWriter writer;
            TextWriter oldOut = Console.Out;

            ConfigOutputFile(out ostrm, out writer); // determina que o Console.Write será no arquivo Output.txt

            //Stopwatch ob = Stopwatch.StartNew();

#if TEST
            var linhas = File.ReadAllLines(
                @"C:\Users\marcos.guedes\Documents\Visual Studio 2017\MaratonaAvanade\002Cartas\002Cartas\bin\Debug\input.txt"
                );
#else
            var linhas = File.ReadAllLines(args[0]);
#endif
            int countLine = 1;
            
            string[] cardsOne = null;
            string[] cardsTwo = null;
            
            foreach (string line in linhas)
            {
                if (countLine == 2)
                {
                    cardsOne = line.Trim().Split(SPACE);
                }
                else if (countLine == 3)
                {
                    cardsTwo = line.Trim().Split(SPACE);
                    countLine = 0;

                    int cardsA = cardsOne.Except(cardsTwo).Count();
                    int cardsB = cardsTwo.Except(cardsOne).Count();

                    Print(Math.Min(cardsA, cardsB));
                }

                countLine++;
            }

            End(ostrm, writer, oldOut);
            //Console.WriteLine(ob.ElapsedMilliseconds);
        }

        private static void Print(int count)
        {
            Console.WriteLine(count);
        }

        private static void ConfigOutputFile(out FileStream ostrm, out StreamWriter writer)
        {
            ostrm = new FileStream(PATH_FILE_OUTPUT, FileMode.OpenOrCreate, FileAccess.Write);
            writer = new StreamWriter(ostrm);
            Console.SetOut(writer);
        }

        private static void End(FileStream ostrm, StreamWriter writer, TextWriter oldOut)
        {
            Console.SetOut(oldOut);
            writer.Close();
            ostrm.Close();

            Console.WriteLine(MSG_DONE); // será exibido no console
        }
    }
}
