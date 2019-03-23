using System;
using System.Threading;

namespace Polly.Contrib.RateLimiting /* Use a namespace broadly describing the topic, eg Polly.Contrib.Logging, Polly.Contrib.RateLimiting */
{
    /// <summary>
    /// A ProactiveFoo policy that can be applied to delegates.
    /// </summary>
    public class TokenBucket : Policy, ITokenBucket
    {
        private readonly int _tokens;

        private readonly TimeSpan _timeWindow;

        private readonly int _returnTokens;
        /* This is the non-generic TokenBucket for synchronous executions.
         * With this policy, users can execute void-returning Actions, using .Execute(...),
         * or TResult-returning Func<>s, using the generic base-class _method_ .Execute<TResult>(...).
         * So, although the policy is non-generic, the Implementation<TResult>(...) method is generic in TResult.
         */

        /* It is a syntax convention for proactive Polly policies to use static creation methods rather than use constructors directly. It makes the syntax more similar to reactive policy syntax. */

        /// <summary>
        /// Constructs a new instance of <see cref="TokenBucket"/>.
        /// </summary>
        /// <returns><see cref="TokenBucket"/></returns>
        public static TokenBucket Create(
            /* If configuration should be passed when creating the policy, pass it here ... */
            int tokens, TimeSpan timeWindow, int returnTokens
            )
        {
            return new TokenBucket(tokens, timeWindow, returnTokens);
        }

        internal TokenBucket(/* configuration parameters */
            int tokens, TimeSpan timeWindow, int returnTokens
            )
        {
            _tokens = tokens;
            _timeWindow = timeWindow;
            _returnTokens = returnTokens;
        }

        /// <inheritdoc/>
        protected override TResult Implementation<TResult>(Func<Context, CancellationToken, TResult> action, Context context, CancellationToken cancellationToken)
        {
            /* This method is intentionally a pass-through.
             Delegating to ProactiveFooEngine.Implementation<TResult>(...) allows the code to use that single synchronous implementation
             for both TokenBucket and TokenBucket<TResult>
             */
            return TokenBucketEngine.Implementation(
                action,
                context,
                cancellationToken,
                _tokens,
                _timeWindow,
                _returnTokens
                );
        }
    }
}