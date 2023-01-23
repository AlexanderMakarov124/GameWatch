﻿namespace GameWatch.Infrastructure.Common;

/// <summary>
/// Thrown when something does not exist.
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
