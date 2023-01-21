namespace PropGuard.Tests
{
    public class PropGuardSkillExecutedTests
    {
        #region Ctor
        [Fact]
        public void Ctor_ShouldSetProperties()
        {
            // Arrange
            var skill = new Faker().PickRandom<PropGuardSkill>();
            var succeeded = new Faker().Random.Bool();

            // Act
            var skillExecuted = new PropGuardSkillExecuted(skill, succeeded);

            // Assert
            skillExecuted.Skill.Should().Be(skill);
            skillExecuted.Succeeded.Should().Be(succeeded);
        }
        #endregion
    }
}
