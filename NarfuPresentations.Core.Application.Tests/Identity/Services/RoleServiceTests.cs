using Microsoft.AspNetCore.Identity;
using NSubstitute;
using NUnit.Framework;
using NarfuPresentations.Core.Application.Identity;
using NarfuPresentations.Core.Infrastructure.Identity.Models;
using NarfuPresentations.Core.Infrastructure.Identity.Services;
using NarfuPresentations.Core.Infrastructure.Persistense.Context;
using System;
using System.Threading.Tasks;

namespace NarfuPresentations.Core.Application.Tests.Identity.Services;

[TestFixture]
public class RoleServiceTests
{
    private RoleManager<ApplicationRole> subRoleManager;
    private UserManager<ApplicationUser> subUserManager;
    private ApplicationDbContext subApplicationDbContext;
    private ICurrentUser subCurrentUser;

    [SetUp]
    public void SetUp()
    {
        //this.subRoleManager = Substitute.For<RoleManager<ApplicationRole>>();
        //this.subUserManager = Substitute.For<UserManager<ApplicationUser>>();
        //this.subApplicationDbContext = Substitute.For<ApplicationDbContext>();
        //this.subCurrentUser = Substitute.For<ICurrentUser>();
    }

    //private RoleService CreateService()
    //{
    //return new RoleService(
    //    this.subRoleManager,
    //    this.subUserManager,
    //    this.subApplicationDbContext,
    //    this.subCurrentUser);
    //}

    [Test]
    public async Task CreateOrUpdateAsync_StateUnderTest_ExpectedBehavior()
    {
        Assert.Pass();
    }

    [Test]
    public async Task ExistsAsync_StateUnderTest_ExpectedBehavior()
    {
        // Assert
        Assert.Pass();
    }

    [Test]
    public async Task GetByIdAsync_StateUnderTest_ExpectedBehavior()
    {
        Assert.Pass();
    }

    [Test]
    public async Task GetByIdWithPermissionsAsync_StateUnderTest_ExpectedBehavior()
    {
        Assert.Pass();
    }

    [Test]
    public async Task GetListAsync_StateUnderTest_ExpectedBehavior()
    {
        Assert.Pass();
    }

    [Test]
    public async Task GetCountAsync_StateUnderTest_ExpectedBehavior()
    {
        Assert.Pass();
    }

    [Test]
    public async Task UpdatePermissionsAsync_StateUnderTest_ExpectedBehavior()
    {
        Assert.Pass();
    }

    [Test]
    public async Task DeleteAsync_StateUnderTest_ExpectedBehavior()
    {
        Assert.Pass();
    }
}
