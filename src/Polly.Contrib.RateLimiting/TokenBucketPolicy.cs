using System;
using System.Threading;

namespace Polly.Contrib.RateLimiting /* Use a namespace broadly describing the topic, eg Polly.Contrib.Logging, Polly.Contrib.RateLimiting */
{
    /// <summary>
    /// A ProactiveFoo policy that can be applied to delegates returning a value of type <typeparamref name="TResult" />
    /// </summary>
    /// <typeparam name="TResult">The type of return values this policy will handle.</typeparam>
    public class TokenBucketPolicy<TResult> : Policy<TResult>, ITokenBucket<TResult>
    {
        /* This is the generic TokenBucket for synchronous executions.
         * With this policy, users can execute TResult-returning Func<>s.
         */

        /* It is a syntax convention for proactive Polly policies to use static creation methods rather than use constructors directly. It makes the syntax more similar to reactive policy syntax. */

        /// <summary>
        /// Constructs a new instance of <see cref="TokenBucketPolicy{TResult}"/>.
        /// </summary>
        /// <returns><see cref="TokenBucketPolicy{TResult}"/></returns>
        public static TokenBucketPolicy<TResult> Create(
            /* If configuration should be passed when creating the policy, pass it here ... */
            )
        {
            return new TokenBucketPolicy<TResult>(/* ... and pass it on to the constructor ... */);
        }

        internal TokenBucketPolicy(/* configuration parameters */)
        {
            /* ... and the policy constructor can store configuration, for the implementation to use. */
        }

        /// <inheritdoc/>
        protected override TResult Implementation(Func<Context, CancellationToken, TResult> action, Context context, CancellationToken cancellationToken)
        {
            /* This method is intentionally a pass-through.
             Delegating to ProactiveFooEngine.Implementation<TResult>(...) allows the code to use that single synchronous implementation
             for both TokenBucket.Execute<TResult>() and TokenBucket<TResult>.Execute(...)
             */
            return TokenBucketEngine.Implementation(
                action,
                context,
                cancellationToken,
                50,
                TimeSpan.Zero,
                50
                /* The implementation should receive at least the above parameters,
                 * but more parameters can also be passed: eg anything the policy was configured with. */
                );
        }
    }
}