using Microsoft.AspNetCore.Identity;
using NarfuPresentations.Core.Application.Common.FileStorage;
using NarfuPresentations.Core.Infrastructure.Identity.Models;
using NarfuPresentations.Core.Infrastructure.Identity.Services;
using NarfuPresentations.Core.Infrastructure.Persistense.Context;
using NSubstitute;

namespace NarfuPresentations.Core.Application.Tests.Identity.Services;

[TestFixture]
public class UserServiceTests
{
    private SignInManager<ApplicationUser> subSignInManager;
    private UserManager<ApplicationUser> subUserManager;
    private RoleManager<ApplicationRole> subRoleManager;
    private ApplicationDbContext subApplicationDbContext;
    private IFileStorageService subFileStorageService;

    [SetUp]
    public void SetUp()
    {
        //this.subSignInManager = Substitute.For<SignInManager<ApplicationUser>>();
        //this.subUserManager = Substitute.For<Microsoft.AspNetCore.Identity.UserManager<ApplicationUser>>();
        //this.subRoleManager = Substitute.For<Microsoft.AspNetCore.Identity.RoleManager<ApplicationRole>>();
        //this.subApplicationDbContext = Substitute.For<ApplicationDbContext>();
        //this.subFileStorageService = Substitute.For<IFileStorageService>();
    }

    private UserService CreateService()
    {
        return new UserService(
            this.subSignInManager,
            this.subUserManager,
            this.subRoleManager,
            this.subApplicationDbContext,
            this.subFileStorageService);
    }

    [Test]
    public async Task ExistsWithEmailAsync_StateUnderTest_ExpectedBehavior()
    {
        Assert.Pass();
    }

    [Test]
    public async Task ExistsWithPhoneNumberAsync_StateUnderTest_ExpectedBehavior()
    {
        Assert.Pass();
    }

    [Test]
    public async Task ExistsWithUserNameAsync_StateUnderTest_ExpectedBehavior()
    {
        Assert.Pass();
    }

    [Test]
    public async Task HasPermissionAsync_StateUnderTest_ExpectedBehavior()
    {
        Assert.Pass();
    }

    [Test]
    public async Task SearchAsync_StateUnderTest_ExpectedBehavior()
    {
        Assert.Pass();
    }

    [Test]
    public async Task CreateAsync_StateUnderTest_ExpectedBehavior()
    {
        Assert.Pass();
    }

    [Test]
    public async Task UpdateAsync_StateUnderTest_ExpectedBehavior()
    {
        Assert.Pass();
    }

    [Test]
    public async Task GetAllAsync_StateUnderTest_ExpectedBehavior()
    {
        Assert.Pass();
    }

    [Test]
    public async Task GetAsync_StateUnderTest_ExpectedBehavior()
    {
        Assert.Pass();
    }

    [Test]
    public async Task GetPermissionsAsync_StateUnderTest_ExpectedBehavior()
    {
        Assert.Pass();
    }

    [Test]
    public async Task GetRolesAsync_StateUnderTest_ExpectedBehavior()
    {
        Assert.Pass();
    }

    [Test]
    public async Task AssignRolesAsync_StateUnderTest_ExpectedBehavior()
    {
        Assert.Pass();
    }

    [Test]
    public async Task ConfirmEmailAsync_StateUnderTest_ExpectedBehavior()
    {
        Assert.Pass();
    }
}
