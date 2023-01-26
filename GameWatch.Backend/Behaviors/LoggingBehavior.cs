using MediatR;

namespace GameWatch.Backend.Behaviors;

/// <summary>
/// Logging behavior to mediator.
/// </summary>
/// <typeparam name="TRequest">Request.</typeparam>
/// <typeparam name="TResponse">Response.</typeparam>
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> logger;

    /// <summary>
    /// Constructor.
    /// </summary>
    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        this.logger = logger;
    }

    /// <inheritdoc />
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogTrace("Handling {Request}", typeof(TRequest).Name);

        TResponse response;

        try
        {
            response = await next();
        }
        catch (Exception ex)
        {
            logger.LogError("Error occurred while handling {Request}. Error message: {ErrorMessage}", typeof(TRequest).Name, ex.Message);

            throw;
        }

        logger.LogTrace("Handled {Request}", typeof(TRequest).Name);

        return response;
    }
}