using System;
using FluentAssertions;
using Xunit;

namespace Polly.Contrib.RateLimiting.Specs
{
    public class TokenBucketSpecs
    {
        [Fact]
        public void ReplaceMeWithRealTests()
        {
            /*
             * This test is for illustrative purposes, to show the interfaces a typical synchronous non-generic policy fulfills.
             * Real tests should check policy behaviour.
             */
            var policy = TokenBucket.Create(50, TimeSpan.FromMinutes(1), 2);

            policy.Should().BeAssignableTo<ISyncPolicy>();
            policy.Should().BeAssignableTo<ITokenBucket>();
        }

        [Fact]
        public void PolicyExecutesThePassedDelegate()
        {
            bool executed = false;
            var policy = TokenBucket.Create(50, TimeSpan.FromMinutes(1), 2);

            policy.Execute(() => executed = true);

            executed.Should().BeTrue();
        }

    }
}
