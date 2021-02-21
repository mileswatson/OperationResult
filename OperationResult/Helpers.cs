using System;
using OperationResult.Tags;

namespace OperationResult
{
    public static class Helpers
    {
        private static SuccessTag SuccessTag = new SuccessTag();

        /// <summary>
        /// Create "Success" Status or Result
        /// </summary>
        public static SuccessTag Ok()
        {
            return SuccessTag;
        }

        /// <summary>
        /// Create "Success" Status or Result
        /// </summary>
        public static SuccessTag<TResult> Ok<TResult>(TResult result) where TResult : notnull
        {
            return new SuccessTag<TResult>(result);
        }

        private static ErrorTag ErrorTag = new ErrorTag();

        /// <summary>
        /// Create "Error" Status or Result
        /// </summary>
        public static ErrorTag Error()
        {
            return ErrorTag;
        }

        /// <summary>
        /// Create "Error" Status or Result
        /// </summary>
        public static ErrorTag<TError> Error<TError>(TError error) where TError : notnull
        {
            return new ErrorTag<TError>(error);
        }

        internal static T GuardNull<T>(T? item) {
            if (item is null) throw new NullReferenceException();
            return item;
        }

        internal static T ThrowException<T>() {
            throw new NullReferenceException();
        }
    }
}
