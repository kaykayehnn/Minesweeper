using System;

public static class Utilities
{
    public static int[][] InitMatrix(int fieldLength, Mine[] mines)
    {
        int[][] matrix = new int[fieldLength][];
        
        //bool[][] minefield = new bool[fieldLength][];
        //for (int i = 0; i < fieldLength; i++)
        //{
        //    minefield[i] = new bool[fieldLength];
        //}
        //foreach (var mine in mines)
        //{
        //    minefield[mine.Row][mine.Column] = true;
        //}

        for (int i = 0; i < fieldLength; i++)
        {
            matrix[i] = new int[fieldLength];
        }
        
        return matrix;
    }

    public static char[][] CharJaggedArr(int fieldLength)
    {
        char[][] matrix = new char[fieldLength + 1][];//top line only for indexing
        fieldLength += 2;// for indexing
        matrix[0] = new char[fieldLength];
        for (int i = 2; i < fieldLength; i++)
        {
            matrix[0][i] = (char)(i + 46);
        }
        for (int i = 1; i < matrix.Length; i++)
        {
            matrix[i] = new char[fieldLength];
            matrix[i][0] = (char)(i + 47);
            matrix[i][1] = ' ';
            for (int j = 2; j < fieldLength; j++)
            {
                matrix[i][j] = '-';
            }
        }
        return matrix;
    }
}