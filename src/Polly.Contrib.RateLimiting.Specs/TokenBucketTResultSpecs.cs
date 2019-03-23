using FluentAssertions;
using Xunit;

namespace Polly.Contrib.RateLimiting.Specs
{
    public class TokenBucketTResultSpecs
    {
        [Fact]
        public void ReplaceMeWithRealTests()
        {
            /*
             * This test is for illustrative purposes, to show the interfaces a typical synchronous generic policy fulfills.
             * Real tests should check policy behaviour.
             */

            TokenBucketPolicy<int> policy = TokenBucketPolicy<int>.Create();

            policy.Should().BeAssignableTo<ISyncPolicy<int>>();
            policy.Should().BeAssignableTo<ITokenBucket<int>>();
        }

        [Fact]
        public void PolicyExecutesThePassedDelegate()
        {
            bool executed = false;
            TokenBucketPolicy<int> policy = TokenBucketPolicy<int>.Create();

            policy.Execute(() =>
            {
                executed = true;
                return default(int);
            });

            executed.Should().BeTrue();
        }
    }
}
