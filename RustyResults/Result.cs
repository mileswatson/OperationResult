using RustyResults.Tags;
using static RustyResults.Helpers;

namespace RustyResults
{
    /// <summary>
    /// Result of operation (without Error field)
    /// </summary>
    /// <typeparam name="TResult">Type of Value field</typeparam>
    public struct Result<TResult>
        where TResult : notnull
    {
        private readonly bool _isSuccess;
        public bool IsSuccess => _isSuccess;
        public bool IsError => !_isSuccess;

        private readonly TResult? _value;
        public TResult Value => IsSuccess ? GuardNull(_value) : ThrowException<TResult>();

        private Result(bool isSuccess)
        {
            _isSuccess = isSuccess;
            _value = default(TResult);
        }

        private Result(TResult result)
        {
            _isSuccess = true;
            _value = result;
        }

        public static implicit operator bool(Result<TResult> result)
        {
            return result._isSuccess;
        }

        public static implicit operator Result<TResult>(TResult result)
        {
            return new Result<TResult>(result);
        }

        public static implicit operator Result<TResult>(SuccessTag<TResult> tag)
        {
            return new Result<TResult>(tag.Value);
        }

        private static Result<TResult> ErrorResult = new Result<TResult>(false);

        public static implicit operator Result<TResult>(ErrorTag tag)
        {
            return ErrorResult;
        }
    }

    /// <summary>
    /// Result of operation (with Error field)
    /// </summary>
    /// <typeparam name="TResult">Type of Value field</typeparam>
    /// <typeparam name="TError">Type of Error field</typeparam>
    public struct Result<TResult, TError>
        where TResult : notnull
        where TError : notnull
    {
        private readonly bool _isSuccess;
        public bool IsSuccess => _isSuccess;
        public bool IsError => !_isSuccess;

        private readonly TResult? _value;
        public TResult Value => IsSuccess ? GuardNull(_value) : ThrowException<TResult>();

        private readonly TError? _error;
        public TError Error => IsError ? GuardNull(_error) : ThrowException<TError>();

        private Result(TResult result)
        {
            _isSuccess = true;
            _value = result;
            _error = default(TError);
        }

        private Result(TError error)
        {
            _isSuccess = false;
            _value = default(TResult);
            _error = error;
        }

        public void Deconstruct(out TResult? result, out TError? error)
        {
            result = _value;
            error = _error;
        }

        public static implicit operator bool(Result<TResult, TError> result)
        {
            return result._isSuccess;
        }

        public static implicit operator Result<TResult, TError>(TResult result)
        {
            return new Result<TResult, TError>(result);
        }

        public static implicit operator Result<TResult, TError>(SuccessTag<TResult> tag)
        {
            return new Result<TResult, TError>(tag.Value);
        }

        public static implicit operator Result<TResult, TError>(ErrorTag<TError> tag)
        {
            return new Result<TResult, TError>(tag.Error);
        }
    }

    /// <summary>
    /// Result of operation (with different Errors)
    /// </summary>
    /// <typeparam name="TResult">Type of Value field</typeparam>
    /// <typeparam name="TError1">Type of first Error</typeparam>
    /// <typeparam name="TError2">Type of second Error</typeparam>
    public struct Result<TResult, TError1, TError2>
        where TResult : notnull
        where TError1 : notnull
        where TError2 : notnull
    {
        private readonly bool _isSuccess;
        public bool IsSuccess => _isSuccess;
        public bool IsError => !_isSuccess;

        private readonly TResult? _value;
        public TResult Value => IsSuccess ? GuardNull(_value) : ThrowException<TResult>();

        private readonly object? _error;
        public object Error => IsError ? GuardNull(_error) : ThrowException<TError1>();

        public bool HasError<TError>() => Error is TError;
        public TError GetError<TError>() => (TError)Error;

        private Result(TResult result)
        {
            _isSuccess = true;
            _value = result;
            _error = null;
        }

        private Result(object error)
        {
            _isSuccess = false;
            _value = default(TResult);
            _error = error;
        }

        public void Deconstruct(out TResult? result, out object? error)
        {
            result = _value;
            error = _error;
        }

        public static implicit operator bool(Result<TResult, TError1, TError2> result)
        {
            return result._isSuccess;
        }

        public static implicit operator Result<TResult, TError1, TError2>(TResult result)
        {
            return new Result<TResult, TError1, TError2>(result);
        }

        public static implicit operator Result<TResult, TError1, TError2>(SuccessTag<TResult> tag)
        {
            return new Result<TResult, TError1, TError2>(tag.Value);
        }

        public static implicit operator Result<TResult, TError1, TError2>(ErrorTag<TError1> tag)
        {
            return new Result<TResult, TError1, TError2>(tag.Error);
        }

        public static implicit operator Result<TResult, TError1, TError2>(ErrorTag<TError2> tag)
        {
            return new Result<TResult, TError1, TError2>(tag.Error);
        }
    }

    /// <summary>
    /// Result of operation (with different Errors)
    /// </summary>
    /// <typeparam name="TResult">Type of Value field</typeparam>
    /// <typeparam name="TError1">Type of first Error</typeparam>
    /// <typeparam name="TError2">Type of second Error</typeparam>
    /// <typeparam name="TError3">Type of third Error</typeparam>
    public struct Result<TResult, TError1, TError2, TError3>
        where TResult : notnull
        where TError1 : notnull
        where TError2 : notnull
        where TError3 : notnull
    {
        private readonly bool _isSuccess;
        public bool IsSuccess => _isSuccess;
        public bool IsError => !_isSuccess;

        private readonly TResult? _value;
        public TResult Value => IsSuccess ? GuardNull(_value) : ThrowException<TResult>();

        private readonly object? _error;
        public object Error => IsError ? GuardNull(_error) : ThrowException<TError1>();

        public bool HasError<TError>() => Error is TError;
        public TError GetError<TError>() => (TError)Error;

        private Result(TResult result)
        {
            _isSuccess = true;
            _value = result;
            _error = null;
        }

        private Result(object error)
        {
            _isSuccess = false;
            _value = default(TResult);
            _error = error;
        }

        public void Deconstruct(out TResult? result, out object? error)
        {
            result = _value;
            error = _error;
        }

        public static implicit operator bool(Result<TResult, TError1, TError2, TError3> result)
        {
            return result._isSuccess;
        }

        public static implicit operator Result<TResult, TError1, TError2, TError3>(TResult result)
        {
            return new Result<TResult, TError1, TError2, TError3>(result);
        }

        public static implicit operator Result<TResult, TError1, TError2, TError3>(SuccessTag<TResult> tag)
        {
            return new Result<TResult, TError1, TError2, TError3>(tag.Value);
        }

        public static implicit operator Result<TResult, TError1, TError2, TError3>(ErrorTag<TError1> tag)
        {
            return new Result<TResult, TError1, TError2, TError3>(tag.Error);
        }

        public static implicit operator Result<TResult, TError1, TError2, TError3>(ErrorTag<TError2> tag)
        {
            return new Result<TResult, TError1, TError2, TError3>(tag.Error);
        }

        public static implicit operator Result<TResult, TError1, TError2, TError3>(ErrorTag<TError3> tag)
        {
            return new Result<TResult, TError1, TError2, TError3>(tag.Error);
        }
    }
}
