namespace PropGuard.Tests.TestHelper
{
    public class TestPropGuardTests
    {
        private readonly IPropGuard<string> _propGuard;
        private readonly PropGuardSkill _skillToLookFor;

        public TestPropGuardTests()
        {
            _propGuard = Substitute.For<IPropGuard<string>>();
            _propGuard.PropName.Returns(new Faker().Lorem.Word());
            _propGuard.PropValue.Returns(new Faker().Lorem.Word());
            _propGuard.SkillsExecuted.Returns(new PropGuardSkillsExecuted());
            _skillToLookFor = new Faker().PickRandom<PropGuardSkill>();
        }

        #region Ctor
        [Fact]
        public void Ctor_ShouldSetProperties()
        {
            // Act
            var testPropGuard = new TestPropGuard<string>(_propGuard);

            // Asert
            testPropGuard.PropName.Should().Be(_propGuard.PropName);
            testPropGuard.PropValue.Should().Be(_propGuard.PropValue);
            testPropGuard.SkillsExecuted.Should().BeEquivalentTo(_propGuard.SkillsExecuted);
        }
        #endregion

        #region ShouldHaveSucceedFor
        [Fact]
        public void ShouldHaveSucceedFor_WhenDontHaveExecutions_ShouldThrowException()
        {
            // Arrange
            var testPropGuard = new TestPropGuard<string>(_propGuard);

            // Act
            var exception = Record.Exception(() => testPropGuard.ShouldHaveSucceedFor(_skillToLookFor));

            // Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<TestPropGuardException>();
            exception.Message.Should().Be($"Expected a succeed for skill {_skillToLookFor}.\r\n");
        }

        [Fact]
        public void ShouldHaveSucceedFor_WhenHaveExecutionsForOtherSkill_ShouldThrowException()
        {
            // Arrange
            var testPropGuard = new TestPropGuard<string>(_propGuard);
            testPropGuard.SkillsExecuted.AddSucceed(new Faker().PickRandomWithout(_skillToLookFor));
            testPropGuard.SkillsExecuted.AddFail(new Faker().PickRandomWithout(_skillToLookFor));

            // Act
            var exception = Record.Exception(() => testPropGuard.ShouldHaveSucceedFor(_skillToLookFor));

            // Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<TestPropGuardException>();
            exception.Message.Should().Be($"Expected a succeed for skill {_skillToLookFor}." +
                $"\r\nSkills that succeeded: 1. ({testPropGuard.SkillsExecuted[0].Skill})." +
                $"\r\nSkills that failed: 1. ({testPropGuard.SkillsExecuted[1].Skill}).\r\n\r\n");
        }

        [Fact]
        public void ShouldHaveSucceedFor_WhenHaveFailedExecutionForSkill_ShouldThrowException()
        {
            // Arrange
            var testPropGuard = new TestPropGuard<string>(_propGuard);
            testPropGuard.SkillsExecuted.AddFail(_skillToLookFor);

            // Act
            var exception = Record.Exception(() => testPropGuard.ShouldHaveSucceedFor(_skillToLookFor));

            // Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<TestPropGuardException>();
            exception.Message.Should().Be($"Expected a succeed for skill {_skillToLookFor}." +
                $"\r\nSkills that succeeded: 0." +
                $"\r\nSkills that failed: 1. ({_skillToLookFor}).\r\n\r\n");
        }

        [Fact]
        public void ShouldHaveSucceedFor_WhenHaveSucceedExecutionForSkill_ShouldNotThrowException()
        {
            // Arrange
            var testPropGuard = new TestPropGuard<string>(_propGuard);
            testPropGuard.SkillsExecuted.AddSucceed(_skillToLookFor);

            // Act
            var exception = Record.Exception(() => testPropGuard.ShouldHaveSucceedFor(_skillToLookFor));

            // Assert
            exception.Should().BeNull();
        }
        #endregion

        #region ShouldHaveOnlySucceedFor
        [Fact]
        public void ShouldHaveOnlySucceedFor_WhenDontHaveExecutions_ShouldThrowException()
        {
            // Arrange
            var testPropGuard = new TestPropGuard<string>(_propGuard);

            // Act
            var exception = Record.Exception(() => testPropGuard.ShouldHaveOnlySucceedFor(_skillToLookFor));

            // Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<TestPropGuardException>();
            exception.Message.Should().Be($"Expected a single skill, but found 0.\r\n");
        }

        [Fact]
        public void ShouldHaveOnlySucceedFor_WhenHaveMoreThanOneExecution_ShouldThrowException()
        {
            // Arrange
            var testPropGuard = new TestPropGuard<string>(_propGuard);
            testPropGuard.SkillsExecuted.AddSucceed(_skillToLookFor);
            testPropGuard.SkillsExecuted.AddSucceed(_skillToLookFor);

            // Act
            var exception = Record.Exception(() => testPropGuard.ShouldHaveOnlySucceedFor(_skillToLookFor));

            // Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<TestPropGuardException>();
            exception.Message.Should().Be($"Expected a single skill, but found 2." +
                $"\r\nSkills that succeeded: 2. ({testPropGuard.SkillsExecuted[0].Skill}, {testPropGuard.SkillsExecuted[1].Skill})." +
                $"\r\nSkills that failed: 0.\r\n\r\n");
        }

        [Fact]
        public void ShouldHaveOnlySucceedFor_WhenHaveFailedExecutionForOtherSkill_ShouldThrowException()
        {
            // Arrange
            var testPropGuard = new TestPropGuard<string>(_propGuard);
            testPropGuard.SkillsExecuted.AddFail(new Faker().PickRandomWithout(_skillToLookFor));

            // Act
            var exception = Record.Exception(() => testPropGuard.ShouldHaveOnlySucceedFor(_skillToLookFor));

            // Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<TestPropGuardException>();
            exception.Message.Should().Be($"Expected a succeed for skill {_skillToLookFor}." +
                $"\r\nSkills that succeeded: 0." +
                $"\r\nSkills that failed: 1. ({testPropGuard.SkillsExecuted[0].Skill}).\r\n\r\n");
        }

        [Fact]
        public void ShouldHaveOnlySucceedFor_WhenHaveSucceedExecutionForOtherSkill_ShouldThrowException()
        {
            // Arrange
            var testPropGuard = new TestPropGuard<string>(_propGuard);
            testPropGuard.SkillsExecuted.AddSucceed(new Faker().PickRandomWithout(_skillToLookFor));

            // Act
            var exception = Record.Exception(() => testPropGuard.ShouldHaveOnlySucceedFor(_skillToLookFor));

            // Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<TestPropGuardException>();
            exception.Message.Should().Be($"Expected a succeed for skill {_skillToLookFor}." +
                $"\r\nSkills that succeeded: 1. ({testPropGuard.SkillsExecuted[0].Skill})." +
                $"\r\nSkills that failed: 0.\r\n\r\n");
        }

        [Fact]
        public void ShouldHaveOnlySucceedFor_WhenHaveFailedExecutionForSkill_ShouldThrowException()
        {
            // Arrange
            var testPropGuard = new TestPropGuard<string>(_propGuard);
            testPropGuard.SkillsExecuted.AddFail(_skillToLookFor);

            // Act
            var exception = Record.Exception(() => testPropGuard.ShouldHaveOnlySucceedFor(_skillToLookFor));

            // Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<TestPropGuardException>();
            exception.Message.Should().Be($"Expected a succeed for skill {_skillToLookFor}." +
                $"\r\nSkills that succeeded: 0." +
                $"\r\nSkills that failed: 1. ({_skillToLookFor}).\r\n\r\n");
        }

        [Fact]
        public void ShouldHaveOnlySucceedFor_WhenHaveSucceedExecutionForSkill_ShouldNotThrowException()
        {
            // Arrange
            var testPropGuard = new TestPropGuard<string>(_propGuard);
            testPropGuard.SkillsExecuted.AddSucceed(_skillToLookFor);

            // Act
            var exception = Record.Exception(() => testPropGuard.ShouldHaveOnlySucceedFor(_skillToLookFor));

            // Assert
            exception.Should().BeNull();
        }
        #endregion

        #region ShouldHaveFailFor
        [Fact]
        public void ShouldHaveFailFor_WhenDontHaveExecutions_ShouldThrowException()
        {
            // Arrange
            var testPropGuard = new TestPropGuard<string>(_propGuard);

            // Act
            var exception = Record.Exception(() => testPropGuard.ShouldHaveFailFor(_skillToLookFor));

            // Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<TestPropGuardException>();
            exception.Message.Should().Be($"Expected a fail for skill {_skillToLookFor}.\r\n");
        }

        [Fact]
        public void ShouldHaveFailFor_WhenHaveExecutionsForOtherSkill_ShouldThrowException()
        {
            // Arrange
            var testPropGuard = new TestPropGuard<string>(_propGuard);
            testPropGuard.SkillsExecuted.AddSucceed(new Faker().PickRandomWithout(_skillToLookFor));
            testPropGuard.SkillsExecuted.AddFail(new Faker().PickRandomWithout(_skillToLookFor));

            // Act
            var exception = Record.Exception(() => testPropGuard.ShouldHaveFailFor(_skillToLookFor));

            // Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<TestPropGuardException>();
            exception.Message.Should().Be($"Expected a fail for skill {_skillToLookFor}." +
                $"\r\nSkills that succeeded: 1. ({testPropGuard.SkillsExecuted[0].Skill})." +
                $"\r\nSkills that failed: 1. ({testPropGuard.SkillsExecuted[1].Skill}).\r\n\r\n");
        }

        [Fact]
        public void ShouldHaveFailFor_WhenHaveSucceedExecutionForSkill_ShouldThrowException()
        {
            // Arrange
            var testPropGuard = new TestPropGuard<string>(_propGuard);
            testPropGuard.SkillsExecuted.AddSucceed(_skillToLookFor);

            // Act
            var exception = Record.Exception(() => testPropGuard.ShouldHaveFailFor(_skillToLookFor));

            // Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<TestPropGuardException>();
            exception.Message.Should().Be($"Expected a fail for skill {_skillToLookFor}." +
                $"\r\nSkills that succeeded: 1. ({_skillToLookFor})." +
                $"\r\nSkills that failed: 0.\r\n\r\n");
        }

        [Fact]
        public void ShouldHaveFailFor_WhenHaveFailedExecutionForSkill_ShouldNotThrowException()
        {
            // Arrange
            var testPropGuard = new TestPropGuard<string>(_propGuard);
            testPropGuard.SkillsExecuted.AddFail(_skillToLookFor);

            // Act
            var exception = Record.Exception(() => testPropGuard.ShouldHaveFailFor(_skillToLookFor));

            // Assert
            exception.Should().BeNull();
        }
        #endregion

        #region ShouldHaveOnlyFailFor
        [Fact]
        public void ShouldHaveOnlyFailFor_WhenDontHaveExecutions_ShouldThrowException()
        {
            // Arrange
            var testPropGuard = new TestPropGuard<string>(_propGuard);

            // Act
            var exception = Record.Exception(() => testPropGuard.ShouldHaveOnlyFailFor(_skillToLookFor));

            // Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<TestPropGuardException>();
            exception.Message.Should().Be($"Expected a single skill, but found 0.\r\n");
        }

        [Fact]
        public void ShouldHaveOnlyFailFor_WhenHaveMoreThanOneExecution_ShouldThrowException()
        {
            // Arrange
            var testPropGuard = new TestPropGuard<string>(_propGuard);
            testPropGuard.SkillsExecuted.AddFail(_skillToLookFor);
            testPropGuard.SkillsExecuted.AddFail(_skillToLookFor);

            // Act
            var exception = Record.Exception(() => testPropGuard.ShouldHaveOnlyFailFor(_skillToLookFor));

            // Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<TestPropGuardException>();
            exception.Message.Should().Be($"Expected a single skill, but found 2." +
                $"\r\nSkills that succeeded: 0." +
                $"\r\nSkills that failed: 2. ({testPropGuard.SkillsExecuted[0].Skill}, {testPropGuard.SkillsExecuted[1].Skill}).\r\n\r\n");
        }

        [Fact]
        public void ShouldHaveOnlyFailFor_WhenHaveSucceedExecutionForOtherSkill_ShouldThrowException()
        {
            // Arrange
            var testPropGuard = new TestPropGuard<string>(_propGuard);
            testPropGuard.SkillsExecuted.AddSucceed(new Faker().PickRandomWithout(_skillToLookFor));

            // Act
            var exception = Record.Exception(() => testPropGuard.ShouldHaveOnlyFailFor(_skillToLookFor));

            // Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<TestPropGuardException>();
            exception.Message.Should().Be($"Expected a fail for skill {_skillToLookFor}." +
                $"\r\nSkills that succeeded: 1. ({testPropGuard.SkillsExecuted[0].Skill})." +
                $"\r\nSkills that failed: 0.\r\n\r\n");
        }

        [Fact]
        public void ShouldHaveOnlyFailFor_WhenHaveFailedExecutionForOtherSkill_ShouldThrowException()
        {
            // Arrange
            var testPropGuard = new TestPropGuard<string>(_propGuard);
            testPropGuard.SkillsExecuted.AddFail(new Faker().PickRandomWithout(_skillToLookFor));

            // Act
            var exception = Record.Exception(() => testPropGuard.ShouldHaveOnlyFailFor(_skillToLookFor));

            // Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<TestPropGuardException>();
            exception.Message.Should().Be($"Expected a fail for skill {_skillToLookFor}." +
                $"\r\nSkills that succeeded: 0." +
                $"\r\nSkills that failed: 1. ({testPropGuard.SkillsExecuted[0].Skill}).\r\n\r\n");
        }

        [Fact]
        public void ShouldHaveOnlyFailFor_WhenHaveSucceedExecutionForSkill_ShouldThrowException()
        {
            // Arrange
            var testPropGuard = new TestPropGuard<string>(_propGuard);
            testPropGuard.SkillsExecuted.AddSucceed(_skillToLookFor);

            // Act
            var exception = Record.Exception(() => testPropGuard.ShouldHaveOnlyFailFor(_skillToLookFor));

            // Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<TestPropGuardException>();
            exception.Message.Should().Be($"Expected a fail for skill {_skillToLookFor}." +
                $"\r\nSkills that succeeded: 1. ({_skillToLookFor})." +
                $"\r\nSkills that failed: 0.\r\n\r\n");
        }

        [Fact]
        public void ShouldHaveOnlyFailFor_WhenHaveFailedExecutionForSkill_ShouldNotThrowException()
        {
            // Arrange
            var testPropGuard = new TestPropGuard<string>(_propGuard);
            testPropGuard.SkillsExecuted.AddFail(_skillToLookFor);

            // Act
            var exception = Record.Exception(() => testPropGuard.ShouldHaveOnlyFailFor(_skillToLookFor));

            // Assert
            exception.Should().BeNull();
        }
        #endregion
    }
}
