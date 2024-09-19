namespace RuleEvaluator.Exceptions
{
    /// <summary>
    /// Thrown when an operand is invalid (Entity is null or empty and Value is null)
    /// </summary>
    public class InvalidOperandException : Exception
    {
        public InvalidOperandException() : base("Operand is invalid, either Entity or Value must contain a value") { }
    }

    /// <summary>
    /// Thrown when a transaction field lookup fails
    /// </summary>
    public class MissingFieldException(string message) : Exception(message) { }

    /// <summary>
    /// Thrown when using an unsupported operator
    /// </summary>
    public class InvalidOperatorException(string message) : Exception(message) { }
}
