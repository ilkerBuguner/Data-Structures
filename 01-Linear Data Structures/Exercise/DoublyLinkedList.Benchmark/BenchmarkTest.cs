namespace DoublyLinkedList.Benchmark
{
    using BenchmarkDotNet.Attributes;
    using Problem02.DoublyLinkedList;

    [LongRunJob]
    [MemoryDiagnoser]
    public class BenchmarkTest
    {
        private DoublyLinkedList<int> _fastList;
        private SinglyLinkedList<int> _slowList;

        [Params(1, 10, 100, 1_000, 10_000, 100_000 /*, 1_000_000, 10_000_000, 100_000_000, 1_000_000_000 */)]
        public int RepetitionCount { get; set; }

        [IterationSetup]
        public void Setup()
        {
            this._fastList= new DoublyLinkedList<int>();
            this._slowList = new SinglyLinkedList<int>();
        }

        [Benchmark]
        public void AddRemoveFirstToFastList()
        {
            for (var i = 0; i < this.RepetitionCount; i++)
                this._fastList.AddFirst(i);
            for (var i = 0; i < this.RepetitionCount; i++)
                this._fastList.RemoveFirst();
        }

        [Benchmark]
        public void AddRemoveFirstToSlowList()
        {
            for (var i = 0; i < this.RepetitionCount; i++)
                this._slowList.AddFirst(i);
            for (var i = 0; i < this.RepetitionCount; i++)
                this._slowList.RemoveFirst();
        }

        [Benchmark]
        public void AddRemoveLastToFastList()
        {
            for (var i = 0; i < this.RepetitionCount; i++)
                this._fastList.AddLast(i);
            for (var i = 0; i < this.RepetitionCount; i++)
                this._fastList.RemoveLast();
        }

        [Benchmark]
        public void AddRemoveLastToSlowList()
        {
            for (var i = 0; i < this.RepetitionCount; i++)
                this._slowList.AddLast(i);
            for (var i = 0; i < this.RepetitionCount; i++)
                this._slowList.RemoveLast();
        }
    }
}