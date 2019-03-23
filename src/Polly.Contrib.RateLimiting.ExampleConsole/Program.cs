using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polly.Contrib.RateLimiting.ExampleConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            clientState = ClientState.Failure;

            var taskBatches =
                Enumerable
                    .Range(0, 1000)
                    .Select(async c =>
                    {
                        var tasks =
                            Enumerable
                                .Range(0, 100)
                                .Select(MakeCall);

                        var res = await Task.WhenAll(tasks);
                        return new List<int>(res);
                    });
            var batches = await Task.WhenAll(taskBatches);

            var totalItems =
                batches
                    .SelectMany(t => t)
                    .Count();

            Console.WriteLine($"clientStats: totalClientCalls:{TotalClientCalls}, totalClientErrors: {TotalClientErrors}");
            Console.WriteLine($"callerStats: totalCallsMade:{TotalCallsMade}, totalErrorsReceived: {TotalErrorsReceived}");

            var tokenBucker = TokenBucket.Create(
                    tokens: 50,
                    timeWindow: TimeSpan.FromSeconds(1),
                    returnTokens: 50
                );

        }


        private static string CallerLock = "lock";
        private static int TotalCallsMade = 0;
        private static int TotalErrorsReceived = 0;
        private static int TotalCatastrophicFailures = 0;

        private static async Task<int> MakeCall(int i)
        {
            TotalCallsMade++;
            try
            {
                return await DoWork(i);
            }
            catch
            {
                lock (CallerLock)
                {
                    TotalErrorsReceived++;
                }
                return 0;
            }
        }


        private static int TotalClientCalls = 0;
        private static int TotalClientErrors = 0;

        private static string ClientLock = "lock";
        private static async Task<int> DoWork(int i)
        {
            await Task.Delay(Delay);

            lock (ClientLock)
            {
                TotalClientCalls++;
            }

            if (clientState == ClientState.Valid)
            {
                return i;
            }

            if (clientState == ClientState.Random)
            {
                return i;
            }

            if (clientState == ClientState.Failure)
            {
                lock (ClientLock)
                {
                    TotalClientErrors++;
                }
                throw new Exception("failure");
            }

            throw new ArgumentException("Client state is unknown");
        }

        private static TimeSpan Delay = TimeSpan.FromSeconds(1);

        private static ClientState clientState = ClientState.Valid;
    }
}