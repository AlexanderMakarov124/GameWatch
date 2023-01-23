namespace GameWatch.Infrastructure.Common;

/// <summary>
/// Thrown when something already exist.
/// </summary>
public class AlreadyExistException : Exception
{
    /// <summary>
    /// Parameterless constructor.
    /// </summary>
    public AlreadyExistException()
    {
    }

    /// <summary>
    /// Constructor with message.
    /// </summary>
    /// <param name="message">Message.</param>
    public AlreadyExistException(string message) : base(message)
    {
    }
}
