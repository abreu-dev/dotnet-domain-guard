namespace PropGuard.Tests.TestHelper
{
    public class TestPropGuardExtensionsTests
    {
        #region Ctor
        [Fact]
        public void TestGuard_ShouldCreateTestPropGuard()
        {
            // Arrange
            var propGuard = new PropGuard<string>(new Faker().Lorem.Word(), new Faker().Lorem.Word());

            // Act
            var testPropGuard = propGuard.TestGuard();

            // Assert
            testPropGuard.PropName.Should().Be(propGuard.PropName);
            testPropGuard.PropValue.Should().Be(testPropGuard.PropValue);
            testPropGuard.SkillsExecuted.Should().BeEquivalentTo(testPropGuard.SkillsExecuted);
        }
        #endregion
    }
}
