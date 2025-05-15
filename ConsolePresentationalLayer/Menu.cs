using System;

namespace ConsolePresentationalLayer
{
    class Menu
    {
        string tx;
        string[] options;
        int position;
        int currCurP;

        public Menu(string _tx, string[] _options)
        {
            tx = _tx;
            options = _options;
            position = 0;
        }

        void ShowMenu()
        {
            for (int i = 0; i < options.Length; i++)
            {
                string currOpt = options[i];
                string be;

                if (i == position)
                {
                    be = "->";
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    be = "  ";
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.WriteLine($" {be} {currOpt}");
                Console.ResetColor();
            }
        }

        public int ExSM()
        {
            Console.WriteLine(tx);
            currCurP = Console.CursorTop;
            ConsoleKey prKey;
            do
            {
                for (int i = 0; i < options.Length; i++)
                {
                    ClearLine(currCurP);
                }
                ShowMenu();
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                prKey = keyInfo.Key;
                if (prKey == ConsoleKey.UpArrow)
                {
                    position--;
                    if (position == -1)
                    {
                        position = options.Length - 1;
                    }
                }
                else if (prKey == ConsoleKey.DownArrow)
                {
                    position++;
                    if (position == options.Length)
                    {
                        position = 0;
                    }
                }

            } while (prKey != ConsoleKey.Enter);

            return position;
        }

        public static void ClearLine(int cr)
        {
            Console.SetCursorPosition(0, cr);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, cr);
        }
    }
}
