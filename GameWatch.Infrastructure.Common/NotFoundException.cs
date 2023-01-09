namespace GameWatch.Infrastructure.Common;
public class NotFoundException : Exception
{
    public NotFoundException()
    {

    }

    public NotFoundException(string message) : base(message)
    {

    }
}
