# RustyResults
__Rust-style error handling for C#__

[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://raw.githubusercontent.com/mileswatson/RustyResults/master/LICENSE)
[![NuGet version](https://img.shields.io/nuget/v/CSharp.RustyResults.svg)](https://www.nuget.org/packages/RustyResults)

```cs
using RustyResults;
using static RustyResults.Helpers;

public Result<double, string> SqrtOperation(double argument)
{
    if (argument < 0)
    {
        return Error("Argument must be greater than zero");
    }
    double result = Math.Sqrt(argument);
    return Ok(result);
}

public void Method()
{
    var result = SqrtOperation(123);

    if (result)
    {
        Console.WriteLine("Value is: {0}", result.Value);
    }
    else
    {
        Console.WriteLine("Error is: {0}", result.Error);
    }
}
```

---
* [__`Result<TResult>`__](#rusty-results-result)

* [__`Result<TResult, TError>`__](#rusty-results-result-error)

* [__`Result<TResult, TError1, TError2, ...>`__](#rusty-results-result-multiple-errors)

* [__`Status`__](#rusty-results-status)

* [__`Status<TError>`__](#rusty-results-status-error)

* [__`Status<TError, TError2, ...>`__](#rusty-results-status-multiple-errors)

* [__`Helpers`__](#rusty-results-helpers)
  - `Ok<>()`
  - `Error<>()`
  - `Ok<TResult>(TResult result)`
  - `Error<TError>(TError error)`

### <a name="rusty-results-result"></a>`Result<TResult>`
Result of some method when there is no `TError` type defined
```cs
public struct Result<TResult>
{
    public readonly TResult Value;

    public bool IsError { get; }
    public bool IsSuccess { get; }

    public static implicit operator bool(Result<TResult> result);
    public static implicit operator Result<TResult>(TResult result);
}
```

Example
```cs
public Result<uint> Square(uint argument)
{
    if (argument >= UInt16.MaxValue)
    {
        return Error();
    }
    return Ok(argument * argument);
}
```

### <a name="rusty-results-result-error"></a>`Result<TResult, TError>`
Either Result of some method or Error from this method
```cs
public struct Result<TResult, TError>
{
    public readonly TError Error;
    public readonly TResult Value;

    public bool IsError { get; }
    public bool IsSuccess { get; }

    public void Deconstruct(out TResult result, out TError error);

    public static implicit operator bool(Result<TResult, TError> result);
    public static implicit operator Result<TResult, TError>(TResult result);
}
```

Also `Result` has shorthand implicit conversion from `TResult` type
```cs
using RustyResults;
using static RustyResults.Helpers;

public async Task<Result<string, HttpStatusCode>> DownloadPage(string url)
{
    using (var client = new HttpClient())
    using (var response = await client.GetAsync(url))
    {
        if (response.IsSuccessStatusCode)
        {
            // return string as Result
            return await response.Content.ReadAsStringAsync();
        }
        return Error(response.StatusCode);
    }
}
```

### <a name="rusty-results-result-multiple-errors"></a>`Result<TResult, TError1, TError2, ...>`
Either Result of some method or multiple Errors from this method
```cs
public struct Result<TResult, TError1, TError2, ...>
{
    public readonly TError Error;
    public readonly TResult Value;

    public bool IsError { get; }
    public bool IsSuccess { get; }

    public void Deconstruct(out TResult result, out object error);
    
    public static implicit operator bool(Result<TResult, TError1, ...> result);
    public static implicit operator Result<TResult, TError1, ...>(TResult result);
}
```

Example
```cs
public Result<int, InnerError> Inner()
{
    return Error(new InnerError());
}

public Result<int, OuterError, InnerError> Outer()
{
    var result = Inner();
    if (!result)
    {
        return Error(result.Error);
    }
    return Error(new OuterError());
}

public void Method()
{
    var result = Outer();
    if (result)
    {
        // ...
    }
    else if (result.HasError<InnerError>())
    {
        Console.WriteLine("{0}", result.GetError<InnerError>());
    }
    else if (result.HasError<OuterError>())
    {
        Console.WriteLine("{0}", result.GetError<OuterError>());
    }
}
```

### <a name="rusty-results-status"></a>`Status`
Status of some operation without result when there is no `TError` type defined
```cs
public struct Status
{
    public bool IsError { get; }
    public bool IsSuccess { get; }

    public static implicit operator bool(Status status);
}
```

Example
```cs
public Status IsOdd(int value)
{
    if (value % 2 == 1)
    {
        return Ok();
    }
    return Error();
}
```

### <a name="rusty-results-status-error"></a>`Status<TError>`
Status of some operation without result
```cs
public struct Status<TError>
{
    public readonly TError Error;

    public bool IsError { get; }
    public bool IsSuccess { get; }

    public static implicit operator bool(Status<TError> status);
}
```

Example
```cs
public Status<string> Validate(string input)
{
    if (String.IsNullOrEmpty(input))
    {
        return Error("Input is empty");
    }
    if (input.Length > 100)
    {
        return Error("Input is too long");
    }
    return Ok();
}
```

### <a name="rusty-results-status-multiple-errors"></a>`Status<TError1, TError2, ...>`
Status of some operation without result but with multiple Errors from this method
```cs
public struct Status<TError1, TError2, ...>
{
    public readonly object Error;

    public bool IsError { get; }
    public bool IsSuccess { get; }

    public TError GetError<TError>();
    public bool HasError<TError>();

    public static implicit operator bool(Status<TError1, ...> status);
}
```

Example
```cs
public Status<InnerError> Inner()
{
    return Error(new InnerError());
}

public Status<OuterError, InnerError> Outer()
{
    var result = Inner();
    if (!result)
    {
        return Error(result.Error);
    }
    return Error(new OuterError());
}
```
<hr>

### <a name="rusty-results-helpers"></a>`Helpers`
```cs
public static class Helpers
{
    public static SuccessTag Ok();
    public static ErrorTag Error();
    public static SuccessTag<TResult> Ok<TResult>(TResult result);
    public static ErrorTag<TError> Error<TError>(TError error);
}
```

## Benchmarks
A performance comparsion with other error handling techniques.

|                                                Method | SuccessRate |         Mean |       Error |      StdDev |       Median |
|------------------------------------------------------ |------------ |-------------:|------------:|------------:|-------------:|
|                     `TResult Operation() + Exception` |          50 | 381,462.3 ns | 2,955.35 ns | 2,619.84 ns | 381,040.1 ns |
|             **`Result<TResult, TError> Operation()`** |          50 |     332.4 ns |     2.67 ns |     2.37 ns |     332.1 ns |
|                  `Tuple<TResult, TError> Operation()` |          50 |     599.2 ns |    31.03 ns |    91.48 ns |     632.1 ns |
| `bool Operation(out TResult value, out TError error)` |          50 |     277.7 ns |     1.92 ns |     1.70 ns |     277.6 ns |
|                                                       |             |              |             |             |              |
|                     `TResult Operation() + Exception` |          90 |  76,696.7 ns |   715.91 ns |   669.67 ns |  76,633.8 ns |
|             **`Result<TResult, TError> Operation()`** |          90 |     277.6 ns |     1.81 ns |     1.69 ns |     277.4 ns |
|                  `Tuple<TResult, TError> Operation()` |          90 |     663.5 ns |    13.15 ns |    30.74 ns |     677.3 ns |
| `bool Operation(out TResult value, out TError error)` |          90 |     232.5 ns |     1.05 ns |     0.94 ns |     232.4 ns |
|                                                       |             |              |             |             |              |
|                     `TResult Operation() + Exception` |          99 |   7,891.5 ns |    82.96 ns |    77.60 ns |   7,883.9 ns |
|             **`Result<TResult, TError> Operation()`** |          99 |     261.5 ns |     2.10 ns |     1.86 ns |     261.5 ns |
|                  `Tuple<TResult, TError> Operation()` |          99 |     647.5 ns |    12.81 ns |    15.25 ns |     651.5 ns |
| `bool Operation(out TResult value, out TError error)` |          99 |     208.4 ns |     1.22 ns |     1.02 ns |     208.4 ns |

