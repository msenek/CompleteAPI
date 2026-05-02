using Moq;
using TestAPI.DB.DTOs;
using TestAPI.DB.Entities;
using TestAPI.Repository.Interface;
using TestAPI.Services.Interface;
using TestAPI.Services;
namespace TestAPI.Test
{
    public class AuthServiceTest
    {
        [Fact]

        public async Task Register_DeveDevolverDto()
        {
            var mockRepository = new Mock<IAuthRepository>();
            var mockTokenService = new Mock<ITokenService>();
            var service = new AuthService(mockRepository.Object, mockTokenService.Object);

            var testUser = new UserRequestDto()
            {
                UserName = "Test Username",
                Password = "123",
                Email = "tesapi@test.com"
            };

            var entityTest = new User()
            {
                UserName = "Test Username",
                Password = "123",
                Email = "tesapi@test.com",
            };


            mockRepository.Setup(r => r.GetUserByEmail(It.IsAny<string>())).ReturnsAsync((User?)null);
            mockRepository.Setup(r => r.CreateUser(It.IsAny<User>())).ReturnsAsync(entityTest); 
            var result = await service.RegisterAsync(testUser);

            Assert.Equal(testUser.UserName, result.UserName);
            Assert.Equal(testUser.Password, result.Password);
            Assert.Equal(testUser.Email, result.Email);   
        }

        [Fact]
        public async Task Login_DebeDevolver_RequestDto()
        {
            var mockRepository = new Mock<IAuthRepository>();
            var mockTokenService = new Mock<ITokenService>();
            var service = new AuthService(mockRepository.Object, mockTokenService.Object);

            var testUser = new UserRequestDto()
            {
                UserName = "Test Username",
                Password = "123",
                Email = "test@email.com"
            };

            var entityTest = new User()
            {
                UserName = "entitytest",
                Email = "test@yahoo.com",
                Password = BCrypt.Net.BCrypt.HashPassword("123")
            };

            mockRepository.Setup(r => r.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(entityTest);
            mockTokenService.Setup(t => t.CreateToken(It.IsAny<User>())).Returns("token-falso");
            var result = await service.LoginAsync(testUser);

            Assert.Equal("token-falso", result);
            Assert.NotNull(result);
        }
               

    }
}
