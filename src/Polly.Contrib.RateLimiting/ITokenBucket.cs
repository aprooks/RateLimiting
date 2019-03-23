namespace Polly.Contrib.RateLimiting
{
    /// <summary>
    /// Defines properties common to synchronous and asynchronous ProactiveFoo policies.
    /// </summary>
    public interface ITokenBucket : IsPolicy
    {
        /* Define properties (if any) or methods (if any) you may want to expose on TokenBucket.

         - Perhaps the custom policy takes configuration properties which you want to expose.
         - Perhaps the custom policy exposes methods for manual control.

        ... but it is equally common to have none.
         */
    }
}