namespace FasterQueue.Benchmark
{
    using BenchmarkDotNet.Attributes;
    using Problem01.FasterQueue;

    [LongRunJob]
    [MemoryDiagnoser]
    public class BenchmarkTest
    {
        private FastQueue<int> _fastQueue;
        private SlowQueue<int> _slowQueue;

        [Params(1, 10, 100, 1_000, 10_000, 100_000 /*, 1_000_000, 10_000_000, 100_000_000, 1_000_000_000 */)]
        public int RepetitionCount { get; set; }

        [IterationSetup]
        public void Setup()
        {
            this._fastQueue= new FastQueue<int>();
            this._slowQueue = new SlowQueue<int>();
        }

        [Benchmark]
        public void AddToFastQueue()
        {
            for (var i = 0; i < this.RepetitionCount; i++)
                this._fastQueue.Enqueue(i);
        }

        [Benchmark]
        public void AddToSlowQueue()
        {
            for (var i = 0; i < this.RepetitionCount; i++)
                this._slowQueue.Enqueue(i);
        }
    }
}