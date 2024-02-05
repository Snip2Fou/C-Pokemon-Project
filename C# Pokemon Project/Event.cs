using System;

class Event
{
    public int action_count = 0;
    public bool ChoiceEvent(int nb_choice)
    {
        ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();

        switch (consoleKeyInfo.Key)
        {
            case ConsoleKey.UpArrow:
                if (action_count - 1 < 0)
                {
                    action_count = nb_choice - 1;
                }
                else
                {
                    action_count--;
                }
                return false;

            case ConsoleKey.DownArrow:
                if (action_count + 1 == nb_choice)
                {
                    action_count = 0;
                }
                else
                {
                    action_count++;
                }
                return false;

            case ConsoleKey.Enter:
                return true;

            default:
                return false;
        }
    }
}