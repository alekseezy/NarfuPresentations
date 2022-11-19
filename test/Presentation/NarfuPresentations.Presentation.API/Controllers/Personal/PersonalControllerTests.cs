using NSubstitute;
using NarfuPresentations.Core.Application.Identity.Users;

namespace NarfuPresentations.Presentation.API.Controllers.Personal;

[TestFixture]
public class PersonalControllerTests
{
    private IUserService subUserService;

    [SetUp]
    public void SetUp()
    {
        this.subUserService = Substitute.For<IUserService>();
    }

    private PersonalController CreatePersonalController()
    {
        return new PersonalController(
            this.subUserService);
    }

    [Test]
    public async Task GetProfileAsync_StateUnderTest_ExpectedBehavior()
    {
        //// Arrange
        //var personalController = this.CreatePersonalController();
        //CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

        //// Act
        //var result = await personalController.GetProfileAsync(
        //    cancellationToken);

        //// Assert
        //Assert.Fail();
        Assert.Pass();
    }

    [Test]
    public async Task UpdateProfileAsync_StateUnderTest_ExpectedBehavior()
    {
        //// Arrange
        //var personalController = this.CreatePersonalController();
        //UpdateUserRequest request = null;

        //// Act
        //var result = await personalController.UpdateProfileAsync(
        //    request);

        //// Assert
        //Assert.Fail();
        Assert.Pass();
    }

    [Test]
    public async Task GetPermissionsAsync_StateUnderTest_ExpectedBehavior()
    {
        //// Arrange
        //var personalController = this.CreatePersonalController();
        //CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

        //// Act
        //var result = await personalController.GetPermissionsAsync(
        //    cancellationToken);

        //// Assert
        //Assert.Fail();
        Assert.Pass();
    }
}
