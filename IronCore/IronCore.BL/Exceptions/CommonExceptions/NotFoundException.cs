using IronCore.BL.Exceptions;
using Microsoft.AspNetCore.Http;
namespace IronCore.BL.Exceptions.CommonExceptions;
public class NotFoundException<T> : Exception, IBaseException
{
    public int Code => StatusCodes.Status404NotFound;

    public string ErrorMessage { get; }

    public NotFoundException() : base(typeof(T).Name + " is not found!")
    {
        ErrorMessage = typeof(T).Name + " is not found!";
    }

    public NotFoundException(string message) : base(message)
    {
        ErrorMessage = message;
    }
}