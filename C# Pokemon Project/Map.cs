using System;
class Map
{
    public char[,] map;
    public int size_x = 20;
    public int size_y = 50;

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