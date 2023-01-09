namespace GameWatch.Infrastructure.Common;

/// <summary>
/// Not found exception.
/// </summary>
public class NotFoundException : Exception
{
    /// <summary>
    /// Parameterless constructor.
    /// </summary>
    public NotFoundException()
    {
    }

    /// <summary>
    /// Constructor with message.
    /// </summary>
    /// <param name="message">Message.</param>
    public NotFoundException(string message) : base(message)
    {

    }
}
