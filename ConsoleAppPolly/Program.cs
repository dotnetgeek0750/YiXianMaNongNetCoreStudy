using System;

namespace ConsoleAppPolly
{
    class Program
    {
        static void Main(string[] args)
        {
            //RetryTest.Run();
            //CircuitBreakerTest.Run();
            // TimeoutTest.Run();
            //BulkheadTest.Run();
            FallbackTest.Run();

            Console.ReadKey();
        }
    }
}
