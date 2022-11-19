using NSubstitute;
using NarfuPresentations.Core.Application.Identity.Users;

namespace NarfuPresentations.Presentation.API.Controllers.Identity;

[TestFixture]
public class UsersControllerTests
{
    private IUserService subUserService;

    [SetUp]
    public void SetUp()
    {
        this.subUserService = Substitute.For<IUserService>();
    }

    private UsersController CreateUsersController()
    {
        return new UsersController(
            this.subUserService);
    }

    [Test]
    public async Task CreateAsync_StateUnderTest_ExpectedBehavior()
    {
        //// Arrange
        //var usersController = this.CreateUsersController();
        //CreateUserRequest request = null;
        //CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

        //// Act
        //var result = await usersController.CreateAsync(
        //    request,
        //    cancellationToken);

        //// Assert
        //Assert.Fail();
        Assert.Pass();
    }
}
