using Microsoft.AspNetCore.Http;

namespace PcAsCloud.BL.Exceptions.ChannelExceptions;

public class IndirectChannelMustHaveChannelNameException : Exception, IBaseException
{
    public int Code => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }
    public IndirectChannelMustHaveChannelNameException() : base("A indirect channel must have a ChannelName!")
    {
        ErrorMessage = "A indirect channel must have a ChannelName!";
    }

    public IndirectChannelMustHaveChannelNameException(string message) : base(message)
    {
        ErrorMessage = message;
    }
}
