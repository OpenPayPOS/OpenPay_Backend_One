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

#pragma warning disable CS8604 // Possible null reference argument.
    public ActionResult Handle(Func<T, ActionResult> success, Func<Exception, ActionResult> fail)
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

    public async Task<ActionResult> HandleAsync<TOut>(Func<T, Task<TOut>> success, Func<Exception, ActionResult> fail)
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

    public async Task<ActionResult> HandleAsync(Func<T, Task<ActionResult>> success, Func<Exception, Task<ActionResult>> fail)
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
}