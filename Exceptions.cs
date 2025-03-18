using System;

public class MatrixSizeMismatchException : Exception
{
    public MatrixSizeMismatchException(string message) : base(message) { }
}

public class InvalidMatrixOperationException : Exception
{
    public InvalidMatrixOperationException(string message) : base(message) { }
}