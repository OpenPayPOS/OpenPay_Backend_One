namespace Interfaces.Common.Models;
public class Optional<T>
{
    public bool IsValid { get => _exception == null; }
    public bool IsInvalid { get => !IsValid; }

#pragma warning disable CS8603 // Possible null reference return.
    public T Value { get => _value; }
    public Exception Exception { get => _exception; }
#pragma warning restore CS8603 // Possible null reference return.

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

#pragma warning disable CS8603 // Possible null reference return.
    public static implicit operator T(Optional<T> optional) => optional._value;
    public static implicit operator Exception(Optional<T> optional) => optional._exception;

    public static implicit operator Optional<T>(T input) => new(input);
    public static implicit operator Optional<T>(Exception ex) => new(ex);
#pragma warning restore CS8603 // Possible null reference return.
}

public class Optional : Optional<None>
{
    public Optional() : base(new None()) { }

    public Optional(Exception exception) : base(exception) { }
}