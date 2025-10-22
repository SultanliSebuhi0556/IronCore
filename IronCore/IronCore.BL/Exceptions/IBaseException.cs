namespace IronCore.BL.Exceptions;

public interface IBaseException
{
    public int Code { get; }
    public string ErrorMessage { get; }
}