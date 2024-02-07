using System;
using System.IO;

public class Map

{
    public char[,] map;
    public char[,] copy_map;
    public int size_x;
    public int size_y;

    public Map()
    {
        string[] lines = File.ReadAllLines("data/carte.txt");
        size_x = lines.Length;
        size_y = lines[0].Length;

        for (int i = 1; i < size_x; i++)
        {
            if (lines[i].Length != size_y)
            {
                throw new ArgumentException("Les lignes du fichier ne sont pas de la m�me longueur.");
            }
        }

        map = new char[size_x, size_y];
        copy_map = new char[size_x, size_y];

        for (int x = 0; x < size_x; x++)
        {
            for (int y = 0; y < size_y; y++)
            {
                map[x, y] = lines[x][y];
                copy_map[x,y] = lines[x][y];
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