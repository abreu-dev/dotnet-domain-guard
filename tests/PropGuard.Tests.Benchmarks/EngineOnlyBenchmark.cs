using BenchmarkDotNet.Attributes;
using PropGuard.Skills;

namespace PropGuard.Tests.Benchmark
{
    [MemoryDiagnoser]
    public class EngineOnlyBenchmark
    {
        [Params(10000)]
        public int N { get; set; }

        private IReadOnlyList<VoidEntity> _noLogicEntities;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _noLogicEntities = Enumerable.Range(0, N).Select(m => new VoidEntity() { Member = "" }).ToList();
        }

        [Benchmark]
        public object Guard_SingleSkill()
        {
            var voidEntity = new VoidEntity();

            for (var i = 0; i < N; ++i)
            {
                voidEntity = VoidEntity.NoLogicEntitySingleSkill(_noLogicEntities[i]);
            }

            return voidEntity;
        }

        [Benchmark]
        public object Guard_TenSkills()
        {
            var voidEntity = new VoidEntity();

            for (var i = 0; i < N; ++i)
            {
                voidEntity = VoidEntity.NoLogicEntityTenSkills(_noLogicEntities[i]);
            }

            return voidEntity;
        }

        public class VoidEntity
        {
            public string Member { get; set; }

            public static VoidEntity NoLogicEntitySingleSkill(VoidEntity voidEntity)
            {
                return new VoidEntity()
                {
                    Member = new PropGuard<string>(nameof(Member), voidEntity.Member)
                        .Empty()
                        .PropValue
                };
            }

            public static VoidEntity NoLogicEntityTenSkills(VoidEntity voidEntity)
            {
                return new VoidEntity()
                {
                    Member = new PropGuard<string>(nameof(Member), voidEntity.Member)
                        .Empty()
                        .Empty()
                        .Empty()
                        .Empty()
                        .Empty()
                        .Empty()
                        .Empty()
                        .Empty()
                        .Empty()
                        .Empty()
                        .PropValue
                };
            }
        }
    }
}
