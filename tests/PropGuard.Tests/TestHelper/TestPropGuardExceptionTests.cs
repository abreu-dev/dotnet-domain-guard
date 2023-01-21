namespace PropGuard.Tests.TestHelper
{
    public class TestPropGuardExceptionTests
    {
        #region Ctor
        [Fact]
        public void Ctor_ShouldSetProperties()
        {
            // Arrange
            var message = new Faker().Lorem.Sentence();

            // Act
            var exception = new TestPropGuardException(message);

            // Assert
            exception.Message.Should().Be(message);
        }
        #endregion
    }
}
