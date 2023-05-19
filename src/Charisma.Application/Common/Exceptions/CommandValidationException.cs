namespace Charisma.Application.Common.Exceptions;

public class CommandValidationException : CharismaBaseException
{
    public CommandValidationException(string message)
        : base(message)
    {
    }
}