using System;

public class SquareMatrix : ICloneable, IComparable<SquareMatrix>
{
  private int[,] _matrix;
  private int _size;

  public SquareMatrix(int size)
  {
    _size = size;
    _matrix = new int[size, size];
    Random random = new Random();
    for (int row = 0; row < size; ++row)
    {
      for (int column = 0; column < size; ++column)
      {
        _matrix[row, column] = random.Next(1, 10);
      }
    }
  }

  public SquareMatrix(int[,] matrix)
  {
    if (matrix.GetLength(0) != matrix.GetLength(1))
    {
      throw new ArgumentException("Матрица должна быть квадратной.");
    }
    _size = matrix.GetLength(0);
    _matrix = (int[,])matrix.Clone();
  }

  public static SquareMatrix operator +(SquareMatrix firstMatrix, SquareMatrix secondMatrix)
  {
    if (firstMatrix._size != secondMatrix._size)
    {
      throw new MatrixSizeMismatchException("Матрицы должны быть одного размера для сложения.");
    }

    int[,] result = new int[firstMatrix._size, firstMatrix._size];
    for (int row = 0; row < firstMatrix._size; ++row)
    {
      for (int column = 0; column < firstMatrix._size; ++column)
      {
        result[row, column] = firstMatrix._matrix[row, column] + secondMatrix._matrix[row, column];
      }
    }
    return new SquareMatrix(result);
  }

  public static SquareMatrix operator *(SquareMatrix firstMatrix, SquareMatrix secondMatrix)
  {
    if (firstMatrix._size != secondMatrix._size)
    {
      throw new MatrixSizeMismatchException("Матрицы должны быть одного размера для умножения.");
    }

    int[,] result = new int[firstMatrix._size, firstMatrix._size];
    for (int row = 0; row < firstMatrix._size; ++row)
    {
      for (int column = 0; column < firstMatrix._size; ++column)
      {
        result[row, column] = 0;
        for (int innerIndex = 0; innerIndex < firstMatrix._size; ++innerIndex)
        {
          result[row, column] += firstMatrix._matrix[row, innerIndex] * secondMatrix._matrix[innerIndex, column];
        }
      }
    }
    return new SquareMatrix(result);
  }

  public static bool operator >(SquareMatrix firstMatrix, SquareMatrix secondMatrix)
  {
    return firstMatrix.Determinant() > secondMatrix.Determinant();
  }

  public static bool operator <(SquareMatrix firstMatrix, SquareMatrix secondMatrix)
  {
    return firstMatrix.Determinant() < secondMatrix.Determinant();
  }

  public static bool operator ==(SquareMatrix firstMatrix, SquareMatrix secondMatrix)
  {
    return firstMatrix.Equals(secondMatrix);
  }

  public static bool operator !=(SquareMatrix firstMatrix, SquareMatrix secondMatrix)
  {
    return !firstMatrix.Equals(secondMatrix);
  }

  public static explicit operator int(SquareMatrix matrix)
  {
    return matrix.Determinant();
  }

  public static bool operator true(SquareMatrix matrix)
  {
    return matrix.Determinant() != 0;
  }

  public static bool operator false(SquareMatrix matrix)
  {
    return matrix.Determinant() == 0;
  }

  public int Determinant()
  {
    if (_size == 1)
    {
      return _matrix[0, 0];
    }
    if (_size == 2)
    {
      return _matrix[0, 0] * _matrix[1, 1] - _matrix[0, 1] * _matrix[1, 0];
    }

    int determinant = 0;
    for (int rowDeterminant = 0; rowDeterminant < _size; ++rowDeterminant)
    {
      int[,] subMatrix = new int[_size - 1, _size - 1];
      for (int row = 1; row < _size; ++row)
      {
        for (int column = 0, subColumn = 0; column < _size; ++column)
        {
          if (column == rowDeterminant) continue;
          subMatrix[row - 1, ++subColumn] = _matrix[row, column];
        }
      }
      determinant += (int)Math.Pow(-1, rowDeterminant) * _matrix[0, rowDeterminant] * new SquareMatrix(subMatrix).Determinant();
    }
    return determinant;
  }

  public SquareMatrix Inverse()
  {
    int determinant = Determinant();
    if (determinant == 0)
    {
      throw new InvalidMatrixOperationException("Матрица вырожденная, обратной матрицы не существует.");
    }

    int[,] inverse = new int[_size, _size];
    for (int row = 0; row < _size; ++row)
    {
      for (int column = 0; column < _size; ++column)
      {
        int[,] subMatrix = new int[_size - 1, _size - 1];
        for (int rowDeterminant = 0, subRow = 0; rowDeterminant < _size; ++rowDeterminant)
        {
          if (rowDeterminant == row)
          {
           continue;
          }
          for (int columnDeterminant = 0, subColumn = 0; columnDeterminant < _size; ++columnDeterminant)
          {
            if (columnDeterminant == column)
            {
             continue;
            }
            subMatrix[subRow, subColumn++] = _matrix[rowDeterminant, columnDeterminant];
          }
          ++subRow;
        }
        inverse[column, row] = (int)Math.Pow(-1, row + column) * new SquareMatrix(subMatrix).Determinant();
      }
    }
    return new SquareMatrix(inverse);
  }

  public SquareMatrix DeepClone()
  {
    return new SquareMatrix((int[,])_matrix.Clone());
  }

  public object Clone()
  {
    return DeepClone();  
  }

  public int CompareTo(SquareMatrix other)
  {
    return Determinant().CompareTo(other.Determinant());
  }

  public override bool Equals(object obj)
  {
    if (obj is SquareMatrix other)
    {
      if (_size != other._size)
      {
        return false;
      }
      for (int row = 0; row < _size; ++row)
      {
        for (int column = 0; column < _size; ++column)
        {
          if (_matrix[row, column] != other._matrix[row, column])
            return false;
        }
      }
      return true;
    }
    return false;
  }

  public override int GetHashCode()
  {
    return _matrix.GetHashCode();
  }

  public override string ToString()
  {
    string result = "";
    for (int row = 0; row < _size; ++row)
    {
      for (int column = 0; column < _size; ++column)
      {
        result += _matrix[row, column] + " ";
      }
      result += "\n";
    }
    return result;
  }
}