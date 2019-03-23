namespace Polly.Contrib.RateLimiting /* Use a namespace broadly describing the topic, eg Polly.Contrib.Logging, Polly.Contrib.RateLimiting */
{
    /// <summary>
    /// Defines properties common to generic, synchronous and asynchronous ProactiveFoo policies.
    /// </summary>
    public interface ITokenBucket<TResult> : ITokenBucket
    {
        /* Define properties (if any) or methods (if any) you may want to expose on TokenBucket<TResult>.

           Typically, ITokenBucket<TResult> : ITokenBucket, so you would only expose here any
           extra properties/methods typed in <TResult> for TResult policies.
         */
    }
}
