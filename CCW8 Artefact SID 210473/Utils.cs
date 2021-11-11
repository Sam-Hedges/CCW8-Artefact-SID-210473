using System;
using System.IO;
using System.Threading;

namespace Artefact
{
    public static class Utils
    {
        public static void WriteLineAdvanced(string text, bool centered = true, bool printAnim = true)
        {

            using (StringReader reader = new StringReader(text))
            {

                string line = string.Empty;

                do
                {

                    line = reader.ReadLine();

                    if (line != null)
                    {

                        if (centered)
                        {
                            Console.SetCursorPosition((Console.WindowWidth - line.Length) / 2, Console.CursorTop);
                        }

                        if (printAnim)
                        {

                            for (int i = 0; i < line.Length; i++)
                            {
                                Console.Write(line[i]);
                                Thread.Sleep(1);
                            }

                            Console.WriteLine();

                        } else
                        {
                            Console.WriteLine(line);
                        }

                    }

                } while (line != null);

            }

        }

    }
}
