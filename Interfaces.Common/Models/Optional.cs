using Microsoft.AspNetCore.Mvc;

namespace Interfaces.Common.Models;
public class Optional<T>
{
    public bool IsValid { get => _exception == null; }
    public bool IsInvalid { get => !IsValid; }

    private readonly T? _value;
    private readonly Exception? _exception;

    public Optional(T value)
    {
        _value = value;
        _exception = null;
    }

    public Optional(Exception exception)
    {
        _exception = exception;
        _value = default;
    }

    public Optional()
    {
        _exception = null;
        _value = default;
    }

    public static implicit operator Optional<T>(T input) => new(input);
    public static implicit operator Optional<T>(Exception ex) => new(ex);
    public static implicit operator Exception?(Optional<T> optional) => optional._exception;

#pragma warning disable CS8604 // Possible null reference argument.

    public void Handle(Action<T> success, Action<Exception> fail)
    {
        if (IsValid)
        {
            success(_value);
        }
        else
        {
            fail(_exception);
        }
    }

    public TOut Handle<TOut>(Func<T, TOut> success, Func<Exception, TOut> fail)
    {
        if (IsValid)
        {
            return success(_value);
        }
        else
        {
            return fail(_exception);
        }
    }

    public ActionResult ProduceResult(Func<T, ActionResult> success, Func<Exception, ActionResult> fail)
    {
        if (IsValid)
        {
            return success(_value);
        }
        else
        {
            return fail(_exception);
        }
    }

    public async Task HandleAsync(Func<T, Task> success, Action<Exception> fail)
    {
        if (IsValid)
        {
            await success(_value);
        }
        else
        {
            fail(_exception);
        }
    }

    public async Task<TOut> HandleAsync<TOut>(Func<T, Task<TOut>> success, Func<Exception, TOut> fail)
    {
        if (IsValid)
        {
            return await success(_value);
        }
        else
        {
            return fail(_exception);
        }
    }

    public async Task<ActionResult> ProduceResultAsync<TOut>(Func<T, Task<TOut>> success, Func<Exception, ActionResult> fail)
    {
        if (IsValid)
        {
            var result = await success(_value);
            if (result is ActionResult actionResult)
            {
                return actionResult;
            }

            return new OkObjectResult(result);
        }
        else
        {
            return fail(_exception);
        }
    }

    public async Task<ActionResult> ProduceResultAsync(Func<T, Task<ActionResult>> success, Func<Exception, Task<ActionResult>> fail)
    {
        if (IsValid)
        {
            return await success(_value);
        }
        else
        {
            return await fail(_exception);
        }
    }
#pragma warning restore CS8604 // Possible null reference argument.
}

public class Optional : Optional<None>
{
    public Optional() : base(new None()) { }

    public Optional(Exception exception) : base(exception) { }

    public static implicit operator Optional(Exception ex) => new(ex);
}