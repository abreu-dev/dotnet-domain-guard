namespace PropGuard.Tests
{
    public class PropGuardSkillsExecutedTests
    {
        #region AddSucceed
        [Fact]
        public void AddSucceed_ShouldCreateSkillExecuted()
        {
            // Arrange
            var skillsExecuted = new PropGuardSkillsExecuted();
            var skill = new Faker().PickRandom<PropGuardSkill>();

            // Act
            skillsExecuted.AddSucceed(skill);

            // Assert
            skillsExecuted.Should().HaveCount(1);
            skillsExecuted[0].Skill.Should().Be(skill);
            skillsExecuted[0].Succeeded.Should().BeTrue();
        }
        #endregion

        #region AddFail
        [Fact]
        public void AddFail_ShouldCreateSkillExecuted()
        {
            // Arrange
            var skillsExecuted = new PropGuardSkillsExecuted();
            var skill = new Faker().PickRandom<PropGuardSkill>();

            // Act
            skillsExecuted.AddFail(skill);

            // Assert
            skillsExecuted.Should().HaveCount(1);
            skillsExecuted[0].Skill.Should().Be(skill);
            skillsExecuted[0].Succeeded.Should().BeFalse();
        }
        #endregion
    }
}
