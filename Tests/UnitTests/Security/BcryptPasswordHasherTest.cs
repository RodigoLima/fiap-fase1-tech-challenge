using fiap_fase1_tech_challenge.Common.Services.Implementations;
using FluentAssertions;

namespace fiap_fase1_tech_challenge.Test.UnitTests.Security
{
    public class BcryptPasswordHasherTest
    {
        private readonly Hasher _hasher;
        public BcryptPasswordHasherTest()
        {
            _hasher = new Hasher();
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "BcryptPasswordHasher")]
        [Fact(DisplayName = "Hash_ShouldCreateAHashForThePassword")]
        public void Hash_ShouldCreateAHashForThePassword()
        {
            //Arrange
            var passwordToHash = "@Password1234";

            //Act
            var hashedPassword = _hasher.Hash(passwordToHash);

            //Assert
            hashedPassword.Should().NotBeNull();
            hashedPassword.Should().NotBe(passwordToHash);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "BcryptPasswordHasher")]
        [Fact(DisplayName = "Verify_ShouldPassTheVerification")]
        public void Verify_ShouldPassTheVerification()
        {
            //Arrange
            var passwordToHash = "@Password1234";
            var hashedPassword = _hasher.Hash(passwordToHash);

            //Act
            var result = _hasher.Verify(passwordToHash, hashedPassword);

            //Assert
            result.Should().BeTrue();
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "BcryptPasswordHasher")]
        [Fact(DisplayName = "Verify_ShouldNotPassTheVerification")]
        public void Verify_ShouldNotPassTheVerification()
        {
            //Arrange
            var passwordToHash = "@Password1234";
            var hashedPassword = _hasher.Hash(passwordToHash);

            //Act
            var result = _hasher.Verify("wrongPassword", hashedPassword);

            //Assert
            result.Should().BeFalse();
        }
    }
}
