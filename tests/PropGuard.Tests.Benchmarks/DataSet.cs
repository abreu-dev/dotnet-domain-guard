using Bogus;

namespace PropGuard.Tests.Benchmark
{
    public static class DataSet
    {
        public static int Size { get; }

        public static Faker<FullEntity> FullEntityNoErrorsFaker { get; private set; }
        public static Faker<FullEntity> FullEntityHalfErrorsFaker { get; private set; }
        public static Faker<FullEntity> FullEntityManyErrorsFaker { get; private set; }

        public static IReadOnlyList<FullEntity> NoErrorsDataSet { get; }
        public static IReadOnlyList<FullEntity> HalfErrorsDataSet { get; }
        public static IReadOnlyList<FullEntity> ManyErrorsDataSet { get; }

        public static IReadOnlyDictionary<string, IReadOnlyList<FullEntity>> DataSets { get; }

        static DataSet()
        {
            void SetupFullEntityNoErrorsFaker()
            {
                FullEntityNoErrorsFaker = new Faker<FullEntity>()
                    .RuleFor(p => p.Text1, p => p.Lorem.Word())
                    .RuleFor(p => p.Text2, p => p.Lorem.Word())
                    .RuleFor(p => p.Number1, p => p.Random.Int(min: 1))
                    .RuleFor(p => p.Number2, p => p.Random.Int(min: 1))
                    .RuleFor(p => p.Guid1, p => p.Random.Guid())
                    .RuleFor(p => p.Guid2, p => p.Random.Guid());
            }

            void SetupFullEntityHalfErrorsFaker()
            {
                FullEntityHalfErrorsFaker = new Faker<FullEntity>()
                    .RuleFor(p => p.Text1, p => p.Lorem.Word())
                    .RuleFor(p => p.Text2, p => p.Lorem.Word().OrNull(p))
                    .RuleFor(p => p.Number1, p => p.Random.Int(min: 1))
                    .RuleFor(p => p.Number2, p => p.Random.Int())
                    .RuleFor(p => p.Guid1, p => p.Random.Guid())
                    .RuleFor(p => p.Guid2, p => p.Random.Guid().OrDefault(p));
            }

            void SetupFullEntityManyErrorsFaker()
            {
                FullEntityManyErrorsFaker = new Faker<FullEntity>()
                    .RuleFor(p => p.Text1, p => p.Lorem.Word().OrNull(p))
                    .RuleFor(p => p.Text2, p => p.Lorem.Word().OrNull(p))
                    .RuleFor(p => p.Number1, p => p.Random.Int())
                    .RuleFor(p => p.Number2, p => p.Random.Int())
                    .RuleFor(p => p.Guid1, p => p.Random.Guid().OrDefault(p))
                    .RuleFor(p => p.Guid2, p => p.Random.Guid().OrDefault(p));
            }

            SetupFullEntityNoErrorsFaker();
            SetupFullEntityHalfErrorsFaker();
            SetupFullEntityManyErrorsFaker();

            Size = 10_000;

            Randomizer.Seed = new Random(666);

            ManyErrorsDataSet = FullEntityManyErrorsFaker.GenerateLazy(Size).ToList();
            HalfErrorsDataSet = FullEntityHalfErrorsFaker.GenerateLazy(Size).ToList();
            NoErrorsDataSet = FullEntityNoErrorsFaker.GenerateLazy(Size).ToList();

            DataSets = new Dictionary<string, IReadOnlyList<FullEntity>>(3)
            {
                ["ManyErrors"] = ManyErrorsDataSet,
                ["HalfErrors"] = HalfErrorsDataSet,
                ["NoErrors"] = NoErrorsDataSet
            };
        }
    }
}
