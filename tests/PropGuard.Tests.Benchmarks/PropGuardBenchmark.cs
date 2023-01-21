using BenchmarkDotNet.Attributes;

namespace PropGuard.Tests.Benchmark
{
    [MemoryDiagnoser]
    public class PropGuardBenchmark
    {
        private IReadOnlyDictionary<string, IReadOnlyList<FullEntity>> _dataSets;

        [Params("ManyErrors", "HalfErrors", "NoErrors")]
        public string DataSet { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
            _dataSets = Benchmark.DataSet.DataSets;
        }

        [Benchmark]
        public object Guard()
        {
            var entities = _dataSets[DataSet];

            var fullEntity = new FullEntity();

            for (var i = 0; i < entities.Count; ++i)
            {
                fullEntity = FullEntity.Guarded(entities[i]);
            }

            return fullEntity;
        }
    }
}
