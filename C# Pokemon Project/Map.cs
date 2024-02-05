using System;
class Map
{
    public char[,] map;
    public int size_x = 20;
    public int size_y = 50;
    public Npc npc = new Npc();

    public Map()
    {
        map = new char[size_x, size_y];
        for (int x = 0; x < size_x; x++)
        {
            for (int y = 0; y < size_y; y++)
            {
                if (x == 0 && y == 0 || x == 0 && y == size_y - 1)
                {
                    map[x, y] = ' ';
                }
                else if (y == 0 || y == size_y - 1)
                {
                    map[x, y] = '|';
                }
                else if (x == 0 || x == size_x - 1)
                {
                    map[x, y] = '_';
                }
                else if((x >= 10 && x <= 13 && y >= 10 && y <= 13) || (x >= 1 && x <= 6 && y >= 40 && y <= 48) || (x >= 12 && x <= 14 && y >= 28 && y <= 34) || (x >= 1 && x <= 4 && y >= 15 && y <= 17) || (x >= 3 && x <= 4 && y >= 18 && y <= 19))
                {
                    map[x, y] = '"';
                }
                else if((x == 1 && y == 35))
                {
                    map[x, y] = 'O';
                }
                else
                {
                    map[x, y] = ' ';
                }
            }
        }
    }

    public void Draw()
    {
        for (int x = 0; x < size_x; x++)
        {
            for (int y = 0; y < size_y; y++)
            {
                Console.Write(map[x, y]);
            }
            Console.Write('\n');
        }
    }
}