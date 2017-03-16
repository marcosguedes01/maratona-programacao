using System;
using System.Diagnostics;
using System.IO;

namespace _001Escada
{
    class Program
    {
        #region Constantes
        private const char LINE_BREAK = '\n';
        private const char SHARP = '#';        
        private const string PATH_FILE_OUTPUT = "./Output.txt";
        private const string MSG_DONE = "Done";
        #endregion

        static void Main(string[] args)
        {
            FileStream ostrm;
            StreamWriter writer;
            TextWriter oldOut = Console.Out;

            ConfigOutputFile(out ostrm, out writer); // determina que o Console.Write será no arquivo Output.txt

            //Stopwatch ob = Stopwatch.StartNew();

            string[] linhas = File.ReadAllLines(args[0]);

            foreach (string line in linhas)
            {
                short count;
                if (short.TryParse(line, out count))
                {
                    Print(count); // Imprime a escada conforme valor de entrada em cada linha
                }
            }

            //Console.WriteLine(ob.ElapsedMilliseconds);
            End(ostrm, writer, oldOut);
        }

        private static void Print(short count)
        {
            for (short i = 1; i <= count; i++)
            {
                Console.WriteLine(new string(SHARP, i).PadLeft(count));
            }
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
