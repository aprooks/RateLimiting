using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Polly.Contrib.RateLimiting.Specs
{
    public class AsyncTokenBucketTResultSpecs
    {
        [Fact]
        public void ReplaceMeWithRealTests()
        {
            /*
             * This test is for illustrative purposes, to show the interfaces a typical asynchronous generic policy fulfills.
             * Real tests should check policy behaviour.
             */
            AsyncTokenBucket<int> policy = AsyncTokenBucket<int>.Create();

            policy.Should().BeAssignableTo<IAsyncPolicy<int>>();
            policy.Should().BeAssignableTo<ITokenBucket<int>>();
        }

        [Fact]
        public async Task PolicyExecutesThePassedDelegate()
        {
            bool executed = false;
            AsyncTokenBucket<int> policy = AsyncTokenBucket<int>.Create();

            await policy.ExecuteAsync(() =>
            {
                executed = true;
                return Task.FromResult(0);
            });

            executed.Should().BeTrue();
        }

    }
}
