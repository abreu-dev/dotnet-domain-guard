using PropGuard.Skills;

namespace PropGuard.Tests.Benchmark
{
    public class FullEntity
    {
        public string Text1 { get; set; }
        public string Text2 { get; set; }

        public int Number1 { get; set; }
        public int Number2 { get; set; }

        public Guid Guid1 { get; set; }
        public Guid Guid2 { get; set; }

        public static FullEntity Guarded(FullEntity fullEntity)
        {
            return new FullEntity()
            {
                Text1 = new PropGuard<string>(nameof(Text1), fullEntity.Text1)
                    .Empty()
                    .PropValue,
                Text2 = new PropGuard<string>(nameof(Text2), fullEntity.Text2)
                    .Empty()
                    .PropValue,
                Number1 = new PropGuard<int>(nameof(Number1), fullEntity.Number1)
                    .Empty()
                    .PropValue,
                Number2 = new PropGuard<int>(nameof(Number2), fullEntity.Number2)
                    .Empty()
                    .PropValue,
                Guid1 = new PropGuard<Guid>(nameof(Guid1), fullEntity.Guid1)
                    .Empty()
                    .PropValue,
                Guid2 = new PropGuard<Guid>(nameof(Guid2), fullEntity.Guid2)
                    .Empty()
                    .PropValue
            };
        }
    }
}
