using RustyResults.Tags;
using static RustyResults.Helpers;

namespace RustyResults
{
    /// <summary>
    /// Status of operation (without Value and Error fields)
    /// </summary>
    public struct Status
    {
        private readonly bool _isSuccess;
        public bool IsSuccess => _isSuccess;
        public bool IsError => !_isSuccess;

        private Status(bool isSuccess)
        {
            _isSuccess = isSuccess;
        }

        public static implicit operator bool(Status status)
        {
            return status._isSuccess;
        }

        private static Status SuccessStatus = new Status(true);

        public static implicit operator Status(SuccessTag tag)
        {
            return SuccessStatus;
        }

        private static Status ErrorStatus = new Status(false);

        public static implicit operator Status(ErrorTag tag)
        {
            return ErrorStatus;
        }
    }

    /// <summary>
    /// Status of operation (without Value but with Error field)
    /// </summary>
    /// <typeparam name="TError">Type of Error field</typeparam>
    public struct Status<TError>
        where TError : notnull
    {
        private readonly bool _isSuccess;

        private readonly TError? _error;
        public TError Error => IsError ? GuardNull(_error) : ThrowException<TError>();

        public bool IsSuccess => _isSuccess;
        public bool IsError => !_isSuccess;

        private Status(bool isSuccess)
        {
            _isSuccess = isSuccess;
            _error = default(TError);
        }

        private Status(TError error)
        {
            _isSuccess = false;
            _error = error;
        }

        public static implicit operator bool(Status<TError> status)
        {
            return status._isSuccess;
        }

        private static Status<TError> SuccessStatus = new Status<TError>(true);

        public static implicit operator Status<TError>(SuccessTag tag)
        {
            return SuccessStatus;
        }

        public static implicit operator Status<TError>(ErrorTag<TError> tag)
        {
            return new Status<TError>(tag.Error);
        }
    }

    /// <summary>
    /// Status of operation (without Value but with different Errors)
    /// </summary>
    /// <typeparam name="TError1">Type of first Error</typeparam>
    /// <typeparam name="TError2">Type of second Error</typeparam>
    public struct Status<TError1, TError2>
        where TError1 : notnull
        where TError2 : notnull
    {
        private readonly bool _isSuccess;
        public bool IsSuccess => _isSuccess;
        public bool IsError => !_isSuccess;

        private readonly object? _error;
        public object Error => IsError ? GuardNull(_error) : ThrowException<object>();

        public bool HasError<TError>() => Error is TError;
        public TError GetError<TError>() => (TError)Error;

        private Status(bool isSuccess)
        {
            _isSuccess = isSuccess;
            _error = null;
        }

        private Status(object error)
        {
            _isSuccess = false;
            _error = error;
        }

        public static implicit operator bool(Status<TError1, TError2> status)
        {
            return status._isSuccess;
        }

        private static Status<TError1, TError2> SuccessStatus = new Status<TError1, TError2>(true);

        public static implicit operator Status<TError1, TError2>(SuccessTag tag)
        {
            return SuccessStatus;
        }

        public static implicit operator Status<TError1, TError2>(ErrorTag<TError1> tag)
        {
            return new Status<TError1, TError2>(tag.Error);
        }

        public static implicit operator Status<TError1, TError2>(ErrorTag<TError2> tag)
        {
            return new Status<TError1, TError2>(tag.Error);
        }
    }


    /// <summary>
    /// Status of operation (without Value but with different Errors)
    /// </summary>
    /// <typeparam name="TError1">Type of first Error</typeparam>
    /// <typeparam name="TError2">Type of second Error</typeparam>
    /// <typeparam name="TError3">Type of third Error</typeparam>
    public struct Status<TError1, TError2, TError3>
        where TError1 : notnull
        where TError2 : notnull
        where TError3 : notnull
    {
        private readonly bool _isSuccess;
        public bool IsSuccess => _isSuccess;
        public bool IsError => !_isSuccess;

        private readonly object? _error;
        public object Error => IsError ? GuardNull(_error) : ThrowException<object>();

        public bool HasError<TError>() => Error is TError;
        public TError GetError<TError>() => (TError)Error;

        private Status(bool isSuccess)
        {
            _isSuccess = isSuccess;
            _error = null;
        }

        private Status(object error)
        {
            _isSuccess = false;
            _error = error;
        }

        public static implicit operator bool(Status<TError1, TError2, TError3> status)
        {
            return status._isSuccess;
        }

        private static Status<TError1, TError2, TError3> SuccessStatus = new Status<TError1, TError2, TError3>(true);

        public static implicit operator Status<TError1, TError2, TError3>(SuccessTag tag)
        {
            return SuccessStatus;
        }

        public static implicit operator Status<TError1, TError2, TError3>(ErrorTag<TError1> tag)
        {
            return new Status<TError1, TError2, TError3>(tag.Error);
        }

        public static implicit operator Status<TError1, TError2, TError3>(ErrorTag<TError2> tag)
        {
            return new Status<TError1, TError2, TError3>(tag.Error);
        }

        public static implicit operator Status<TError1, TError2, TError3>(ErrorTag<TError3> tag)
        {
            return new Status<TError1, TError2, TError3>(tag.Error);
        }
    }
}
