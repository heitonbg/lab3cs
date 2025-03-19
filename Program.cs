using System;

class Program
{
  static void Main(string[] args)
  {
    try
    {
      SquareMatrix matrix1 = new SquareMatrix(3);
      SquareMatrix matrix2 = new SquareMatrix(3);

      Console.WriteLine("Матрица 1:");
      Console.WriteLine(matrix1);

      Console.WriteLine("Матрица 2:");
      Console.WriteLine(matrix2);

      SquareMatrix sum = matrix1 + matrix2;
      Console.WriteLine("Сумма матриц:");
      Console.WriteLine(sum);

      SquareMatrix product = matrix1 * matrix2;
      Console.WriteLine("Произведение матриц:");
      Console.WriteLine(product);

      Console.WriteLine("Детерминант матрицы 1: " + matrix1.Determinant());
      Console.WriteLine("Детерминант матрицы 2: " + matrix2.Determinant());

      if (matrix1 > matrix2)
      {
        Console.WriteLine("Детерминант матрицы 1 больше.");
      }
      else if (matrix1 < matrix2)
      {
        Console.WriteLine("Детерминант матрицы 1 меньше.");
      }
      else
      {
        Console.WriteLine("Детерминанты равны.");
      }

      SquareMatrix inverseMatrix1 = matrix1.Inverse();
      Console.WriteLine("Обратная матрица 1:");
      Console.WriteLine(inverseMatrix1);
    }
    catch (MatrixSizeMismatchException ex)
    {
      Console.WriteLine("Ошибка: " + ex.Message);
    }
    catch (InvalidMatrixOperationException ex)
    {
      Console.WriteLine("Ошибка: " + ex.Message);
    }
    catch (Exception ex)
    {
      Console.WriteLine("Ошибка: " + ex.Message);
    }
  }
}