namespace PropGuard.TestHelper
{
    public static class TestPropGuardExtensions
    {
        public static TestPropGuard<TPropType> TestGuard<TPropType>(this IPropGuard<TPropType> propGuard)
        {
            return new TestPropGuard<TPropType>(propGuard);
        }
    }
}
