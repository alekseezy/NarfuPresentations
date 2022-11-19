using NSubstitute;
using NarfuPresentations.Core.Application.Identity.Tokens;
using NarfuPresentations.Shared.Contracts.Identity.Tokens.Requests;

namespace NarfuPresentations.Presentation.API.Controllers.Identity;

[TestFixture]
public class TokensControllerTests
{
    private ITokenService subTokenService;

    [SetUp]
    public void SetUp()
    {
        this.subTokenService = Substitute.For<ITokenService>();
    }

    private TokensController CreateTokensController()
    {
        return new TokensController(
            this.subTokenService);
    }

    [Test]
    public async Task GetTokenAsync_StateUnderTest_ExpectedBehavior()
    {
        //// Arrange
        //var tokensController = this.CreateTokensController();
        //TokenRequest request = null;
        //CancellationToken cancellationToken = default;

        //// Act
        //var result = await tokensController.GetTokenAsync(
        //    request,
        //    cancellationToken);

        // Assert
        Assert.Pass();
    }

    [Test]
    public async Task RefreshTokenAsync_StateUnderTest_ExpectedBehavior()
    {
        //// Arrange
        //var tokensController = this.CreateTokensController();
        //RefreshTokenRequest request = null;

        //// Act
        //var result = await tokensController.RefreshTokenAsync(
        //    request);

        //// Assert
        //Assert.Fail();
        Assert.Pass();
    }

    [Test]
    public async Task RevokeTokenAsync_StateUnderTest_ExpectedBehavior()
    {
        //// Arrange
        //var tokensController = this.CreateTokensController();
        //RevokeTokenRequest request = null;

        //// Act
        //await tokensController.RevokeTokenAsync(
        //    request);

        //// Assert
        //Assert.Fail();
        Assert.Pass();
    }
}
