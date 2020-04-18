namespace AwesomeBank.Identity.Tests.Domain.Specifications
{
    using AutoFixture.Xunit2;
    using AwesomeBank.Identity.Domain.Specifications;
    using FluentAssertions;
    using Xunit;

    public class UserForEmailAddressSpecificationTests
    {
        [Theory]
        [AutoData]
        public void When_Given_Email_Is_Same_As_User_Email_Then_Specification_Is_Satisfied(string email)
        {
            // Arrange
            var user = IdentityTestsHelper.CreateUser(email);
            var sut = new UserForEmailAddressSpecification(email);

            // Act
            var result = sut.IsSatisfiedBy(user);

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [AutoData]
        public void When_Given_Email_Is_Different_Then_User_Email_Then_Specification_Is_Not_Satisfied(string givenEmail, string userEmail)
        {
            // Arrange
            var user = IdentityTestsHelper.CreateUser(userEmail);
            var sut = new UserForEmailAddressSpecification(givenEmail);

            // Act
            var result = sut.IsSatisfiedBy(user);

            // Assert
            result.Should().BeFalse();
        }
    }
}