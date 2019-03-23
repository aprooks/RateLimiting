using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Polly.Contrib.RateLimiting.Specs
{
    public class AsyncTokenBucketSpecs
    {
        [Fact]
        public void ReplaceMeWithRealTests()
        {
            /*
             * This test is for illustrative purposes, to show the interfaces a typical asynchronous non-generic policy fulfills.
             * Real tests should check policy behaviour.
             */
            AsyncTokenBucket policy = AsyncTokenBucket.Create();

            policy.Should().BeAssignableTo<IAsyncPolicy>();
            policy.Should().BeAssignableTo<ITokenBucket>();
        }

        [Fact]
        public async Task PolicyExecutesThePassedDelegate()
        {
            bool executed = false;
            AsyncTokenBucket policy = AsyncTokenBucket.Create();

            await policy.ExecuteAsync(() =>
            {
                executed = true;
                return Task.CompletedTask;
            });

            executed.Should().BeTrue();
        }

    }
}
