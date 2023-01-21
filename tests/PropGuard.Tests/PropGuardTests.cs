namespace PropGuard.Tests
{
    public class PropGuardTests
    {
        #region Ctor
        [Fact]
        public void Ctor_ShouldSetProperties()
        {
            // Arrange
            var propName = new Faker().Lorem.Word();
            var propValue = new Faker().Lorem.Word();

            // Act
            var propGuard = new PropGuard<string>(propName, propValue);

            // Assert
            propGuard.PropName.Should().Be(propName);
            propGuard.PropValue.Should().Be(propValue);
            propGuard.SkillsExecuted.Should().BeEmpty();
        }
        #endregion
    }
}
