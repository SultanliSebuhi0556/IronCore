
using Microsoft.AspNetCore.Http;

namespace PcAsCloud.BL.Exceptions.ChannelExceptions;
public class DirectChannelMustHaveTargetUserException : Exception, IBaseException
{
    public int Code => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public DirectChannelMustHaveTargetUserException() : base("A direct channel must have a TargetUser!")
    {
        ErrorMessage = "A direct channel must have a TargetUser!";
    }

    public DirectChannelMustHaveTargetUserException(string message) : base(message)
    {
        ErrorMessage = message;
    }
}
