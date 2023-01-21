using PropGuard.Skills;

namespace PropGuard.Tests.Skills
{
    public class EmptySkillTests
    {
        #region ForString
        [Fact]
        public void ForString_WhenValid_ShouldSucceed()
        {
            // Arrange
            var propName = new Faker().Lorem.Word();
            var propValue = new Faker().Lorem.Word();

            // Act
            var propGuard = new PropGuard<string>(propName, propValue).Empty().TestGuard();

            // Assert
            propGuard.ShouldHaveOnlySucceedFor(PropGuardSkill.Empty);
        }

        [Fact]
        public void ForString_WhenEmpty_ShouldFail()
        {
            // Arrange
            var propName = new Faker().Lorem.Word();
            var propValue = string.Empty;

            // Act
            var propGuard = new PropGuard<string>(propName, propValue).Empty().TestGuard();

            // Assert
            propGuard.ShouldHaveOnlyFailFor(PropGuardSkill.Empty);
        }

        [Fact]
        public void ForString_WhenNull_ShouldFail()
        {
            // Arrange
            var propName = new Faker().Lorem.Word();
            string propValue = null;

            // Act
            var propGuard = new PropGuard<string>(propName, propValue).Empty().TestGuard();

            // Assert
            propGuard.ShouldHaveOnlyFailFor(PropGuardSkill.Empty);
        }
        #endregion

        #region ForInt
        [Fact]
        public void ForInt_WhenValid_ShouldSucceed()
        {
            // Arrange
            var propName = new Faker().Lorem.Word();
            var propValue = new Faker().Random.Number(min: 1);

            // Act
            var propGuard = new PropGuard<int>(propName, propValue).Empty().TestGuard();

            // Assert
            propGuard.ShouldHaveOnlySucceedFor(PropGuardSkill.Empty);
        }

        [Fact]
        public void ForInt_WhenZero_ShouldFail()
        {
            // Arrange
            var propName = new Faker().Lorem.Word();
            var propValue = 0;

            // Act
            var propGuard = new PropGuard<int>(propName, propValue).Empty().TestGuard();

            // Assert
            propGuard.ShouldHaveOnlyFailFor(PropGuardSkill.Empty);
        }
        #endregion

        #region ForGuid
        [Fact]
        public void ForGuid_WhenValid_ShouldSucceed()
        {
            // Arrange
            var propName = new Faker().Lorem.Word();
            var propValue = new Faker().Random.Guid();

            // Act
            var propGuard = new PropGuard<Guid>(propName, propValue).Empty().TestGuard();

            // Assert
            propGuard.ShouldHaveOnlySucceedFor(PropGuardSkill.Empty);
        }

        [Fact]
        public void ForGuid_WhenEmpty_ShouldFail()
        {
            // Arrange
            var propName = new Faker().Lorem.Word();
            var propValue = Guid.Empty;

            // Act
            var propGuard = new PropGuard<Guid>(propName, propValue).Empty().TestGuard();

            // Assert
            propGuard.ShouldHaveOnlyFailFor(PropGuardSkill.Empty);
        }
        #endregion
    }
}
