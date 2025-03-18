using System;

public class SquareMatrix
{
    private int[,] _matrix;
    private int _size;


    public SquareMatrix(int size)
    {
        _size = size;
        _matrix = new int[size, size];
        Random random = new Random();
        for (int row = 0; row < size; row++)
        {
            for (int column = 0; column < size; column++)
            {
                _matrix[row, column] = random.Next(1, 10);
            }
        }
    }

    // Конструктор для создания матрицы из существующего массива
    public SquareMatrix(int[,] matrix)
    {
        if (matrix.GetLength(0) != matrix.GetLength(1))
            throw new ArgumentException("Матрица должна быть квадратной.");
        _size = matrix.GetLength(0);
        _matrix = (int[,])matrix.Clone();
    }

    // Переопределение метода ToString для вывода матрицы
    public override string ToString()
    {
        string result = "";
        for (int row = 0; row < _size; row++)
        {
            for (int column = 0; column < _size; column++)
            {
                result += _matrix[row, column] + " ";
            }
            result += "\n";
        }
        return result;
    }

    public static SquareMatrix operator +(SquareMatrix firstMatrix, SquareMatrix secondMatrix)
    {
        if (firstMatrix._size != secondMatrix._size)
            throw new MatrixSizeMismatchException("Матрицы должны быть одного размера для сложения.");

        int[,] result = new int[firstMatrix._size, firstMatrix._size];
        for (int row = 0; row < firstMatrix._size; row++)
        {
            for (int column = 0; column < firstMatrix._size; column++)
            {
                result[row, column] = firstMatrix._matrix[row, column] + secondMatrix._matrix[row, column];
            }
        }
        return new SquareMatrix(result);
    }

    public static SquareMatrix operator *(SquareMatrix firstMatrix, SquareMatrix secondMatrix)
    {
        if (firstMatrix._size != secondMatrix._size)
            throw new MatrixSizeMismatchException("Матрицы должны быть одного размера для умножения.");

        int[,] result = new int[firstMatrix._size, firstMatrix._size];
        for (int row = 0; row < firstMatrix._size; row++)
        {
            for (int column = 0; column < firstMatrix._size; column++)
            {
                result[row, column] = 0;
                for (int k = 0; k < firstMatrix._size; k++)
                {
                    result[row, column] += firstMatrix._matrix[row, k] * secondMatrix._matrix[k, column];
                }
            }
        }
        return new SquareMatrix(result);
    }
}
