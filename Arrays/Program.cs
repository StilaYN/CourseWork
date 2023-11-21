using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrays

{
    internal class Program
    {
        static int x = 0;
        static int y = 0;
        static int lastNumber = 1;
        static void Main(string[] args)
        {
            // Task1PrintArray(Task1CreateArray(10));
            // PrintMatrix(Task2CreateMatrix(10));
            PrintMatrix(Task3CreateMatrix(3));
        }

        public static int[] Task1CreateArray(int len)
        {
            var array = new int[len];
            for (int i = 0; i < len; i++)
            {
                array[i] = i + 1;
            }

            return array;
        }

        public static void Task1PrintArray(int[] array)
        {
            for (int i = array.Length - 1; i >= 0; i--)
            {
                Console.Write(array[i] + " ");
            }
            Console.WriteLine();
        }

        public static int[,] Task2CreateMatrix(int len)
        {
            int[,] matrix = new int[len, len];
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    matrix[i, j] = (j >= i - 1) ? 1 : 0;
                }
            }
            return matrix;
        }

        public static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public static int[,] Task3CreateMatrix(int len)
        {
            int[,] matrix = new int[len, len];
            while (!Task3IsMatrixFill(matrix))
            {
                Task3FillMatrix(matrix, 1, 0);
                Task3FillMatrix(matrix, 0, 1);
                Task3FillMatrix(matrix, -1, 0);
                Task3FillMatrix(matrix, 0, -1);
            }
            return matrix;

        }

        public static void Task3FillMatrix(int[,] matrix, int dx, int dy)
        {
            if (dy == 0)
            {
                while (x >= 0 && x < matrix.GetLength(1) && matrix[y,x]==0)
                {
                    matrix[y, x] = lastNumber;
                    x += dx;
                    y += dy;
                    lastNumber += 1;
                }

                x -= dx;
                lastNumber -= 1;
            }
            if (dx == 0)
            {
                while (y >= 0 && y < matrix.GetLength(0) && matrix[y,x]==0)
                {
                    matrix[y, x] = lastNumber;
                    x += dx;
                    y += dy;
                    lastNumber += 1;
                }

                y -= dy;
                lastNumber -= 1;
            }

        }

        public static bool Task3IsMatrixFill(int[,] matrix)
        {
            foreach (int i in matrix)
            {
                if (i == 0) return false;
            }
            return true;
        }
    }

}