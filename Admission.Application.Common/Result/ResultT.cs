﻿namespace Admission.Application.Common.Result;

public sealed class Result<TValue>: Result
{
    private readonly TValue? _value;

    private Result(TValue value): base()
    {
        _value = value;
    }

    private Result(Exception exception) : base(exception) {}
    
    public static implicit operator Result<TValue>(TValue value) => new (value);
    public static implicit operator Result<TValue>(Exception exception) => new (exception);
    
    public TResult Match<TResult>(Func<TValue, TResult> onSuccess, Func<Exception, TResult> onFailure) =>
        IsSuccess ? onSuccess(_value) : onFailure(_exception);
    
    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failure result can not be accessed.");
}