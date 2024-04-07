namespace Admission.Application.Common.Result;

public class Result
{
    protected Result(Exception exception)
    {
        IsSuccess = false;
        Exception = exception;
    }
    protected Result()
    {
        IsSuccess = true;
        Exception = null;
    }
    
    protected Exception? Exception { get; }
    public bool IsSuccess { get; }
    
    public bool IsFailure => !IsSuccess;

    public static Result Success() => new Result();
    
    public static implicit operator Result(Exception exception) => new (exception);

    public TResult Match<TResult>(Func<TResult> onSuccess, Func<Exception, TResult> onFailure) =>
        IsSuccess ? onSuccess() : onFailure(Exception);
}