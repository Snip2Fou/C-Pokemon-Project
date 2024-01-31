using System;


class Game
{
    public bool isRunning = true;
    protected Map map = new Map();
    protected int[] playerPos = new int[2];

    public Game()
    {
        playerPos[0] = map.size_x / 2;
        playerPos[1] = map.size_y / 2;
        map.map.SetValue('p', playerPos[0], playerPos[1]);
        map.Draw();
    }

    public void GameLoop()
    {


        while (isRunning)
        {
            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
            Console.Clear();

            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.Escape:
                    isRunning = false;
                    break;

                case ConsoleKey.UpArrow:
                    if (playerPos[0] - 1 > 0)
                    {
                        map.map[playerPos[0], playerPos[1]] = ' ';
                        map.map[playerPos[0] - 1, playerPos[1]] = 'p';
                        playerPos[0] -= 1;
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (playerPos[0] + 1 < map.size_x - 1)
                    {
                        map.map[playerPos[0], playerPos[1]] = ' ';
                        map.map[playerPos[0] + 1, playerPos[1]] = 'p';
                        playerPos[0] += 1;
                    }
                    break;

                case ConsoleKey.LeftArrow:
                    if (playerPos[1] - 1 > 0)
                    {
                        map.map[playerPos[0], playerPos[1]] = ' ';
                        map.map[playerPos[0], playerPos[1] - 1] = 'p';
                        playerPos[1] -= 1;
                    }
                    break;

                case ConsoleKey.RightArrow:
                    if (playerPos[1] + 1 < map.size_y - 1)
                    {
                        map.map[playerPos[0], playerPos[1]] = ' ';
                        map.map[playerPos[0], playerPos[1] + 1] = 'p';
                        playerPos[1] += 1;
                    }
                    break;


                default:
                    break;

            }
            map.Draw();
        }
    }
}