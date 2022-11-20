using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;
using NarfuPresentations.Core.Infrastructure.Authentication.JWT.Settings;
using NarfuPresentations.Core.Infrastructure.Identity.Models;
using NarfuPresentations.Core.Infrastructure.Identity.Services;
using System;
using System.Threading.Tasks;

namespace NarfuPresentations.Core.Application.Tests.Identity.Services;

[TestFixture]
public class TokenServiceTests
{
    private UserManager<ApplicationUser> subUserManager;
    private IOptions<JwtSettings> subOptions;

    [SetUp]
    public void SetUp()
    {
        //this.subUserManager = Substitute.For<UserManager<ApplicationUser>>();
        //this.subOptions = Substitute.For<IOptions<JwtSettings>>();
    }

    private TokenService CreateService()
    {
        return new TokenService(
            this.subUserManager,
            this.subOptions);
    }

    [Test]
    public async Task GetTokenAsync_StateUnderTest_ExpectedBehavior()
    {
        Assert.Pass();
    }

    [Test]
    public async Task RefreshTokenAsync_StateUnderTest_ExpectedBehavior()
    {
        Assert.Pass();
    }

    [Test]
    public async Task RevokeTokenAsync_StateUnderTest_ExpectedBehavior()
    {
        Assert.Pass();
    }
}
