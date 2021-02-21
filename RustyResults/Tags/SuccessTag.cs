namespace RustyResults.Tags
{
    public struct SuccessTag { }

    public struct SuccessTag<TResult> where TResult : notnull
    {
        internal readonly TResult Value;

        internal SuccessTag(TResult result)
        {
            Value = result;
        }
    }
}
