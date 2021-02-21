namespace RustyResults.Tags
{
    public struct ErrorTag { }

    public struct ErrorTag<TError> where TError : notnull
    {
        internal readonly TError Error;

        internal ErrorTag(TError error)
        {
            Error = error;
        }
    }
}
