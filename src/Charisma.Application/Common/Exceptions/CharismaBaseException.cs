namespace Charisma.Application.Common.Exceptions;

public class CharismaBaseException : Exception
{
    public CharismaBaseException(string message)
        : base(message)
    {
    }
}