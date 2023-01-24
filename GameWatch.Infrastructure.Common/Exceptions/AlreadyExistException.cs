using System.Runtime.Serialization;


namespace GameWatch.Infrastructure.Common.Exceptions;

/// <summary>
/// Raises when something already exist.
/// </summary>
[Serializable]
public class AlreadyExistException : Exception
{
    /// <summary>
    /// Initialize the exception.
    /// </summary>
    public AlreadyExistException()
    {
    }

    /// <summary>
    /// Initialize the exception with a message.
    /// </summary>
    /// <param name="message">Message that describes the exception.</param>
    public AlreadyExistException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initialize the exception with a message and inner exception.
    /// </summary>
    /// <param name="message">Message that describes the exception.</param>
    /// <param name="innerException">Inner exception.</param>
    public AlreadyExistException(string message, Exception innerException) : base(message, innerException)
    {
    }

    /// <summary>
    /// Initialize the exception with serialization info and streaming context.
    /// </summary>
    /// <param name="info">Serialization info.</param>
    /// <param name="context">Streaming context.</param>
    protected AlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
