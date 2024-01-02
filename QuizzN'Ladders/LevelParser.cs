using System;
using static System.Console;
using System.IO;

namespace QuizzN_Ladders

{
    internal class LevelParser//Reads the txt file and make it into 2d array 
    {
        public static string[,] ParseFileToArray(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            string firstLine = lines[0];
            int rows = lines.Length;
            int cols = firstLine.Length;
            string[,] grid = new string[rows, cols];
            for (int y = 0; y < rows; y++)
            {
                string line = lines[y];
                for (int x = 0; x < cols; x++)
                {
                    char currentChar = line[x];
                    grid[y, x] = currentChar.ToString();
                }
            }

            return grid;

        }
    }
}
