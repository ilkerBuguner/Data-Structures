namespace DoublyLinkedList.Benchmark
{
    using BenchmarkDotNet.Running;

    public class Program
    {
        public static void Main()
        {
            // NOTE: Benchmark tests should be always run in 'Release' mode.
            BenchmarkRunner.Run<BenchmarkTest>();
        }
    }
}
