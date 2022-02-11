using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shopperior.Tests;
using Shopperior.WebApi.Shared.Services;
using Shopperior.WebApi.Users.Models;

namespace Shopperior.WebApi.Tests.Shared.Services;

[TestClass]
public class CurrentUserProvider_SetCurrentUserShould : BaseTest<CurrentUserProvider>
{
    private CurrentUser _expectedCurrentUser;

    public CurrentUserProvider_SetCurrentUserShould()
    {
        _expectedCurrentUser = new CurrentUser
        {
            Guid = Guid.NewGuid(),
            Username = RandomString(16),
            GivenName = RandomString(16),
            FamilyName = RandomString(16),
            EmailAddress = RandomString(16),
            Idp = RandomString(16),
            IdpSubject = RandomString(16)
        };
    }

    [TestMethod]
    public async Task SetTheCurrentUserProperty()
    {
        var sut = new CurrentUserProvider();
        await sut.SetCurrentUser(_expectedCurrentUser);

        Assert.AreEqual(sut.CurrentUser.Guid, _expectedCurrentUser.Guid);
        Assert.AreEqual(sut.CurrentUser.Username, _expectedCurrentUser.Username);
        Assert.AreEqual(sut.CurrentUser.GivenName, _expectedCurrentUser.GivenName);
        Assert.AreEqual(sut.CurrentUser.FamilyName, _expectedCurrentUser.FamilyName);
        Assert.AreEqual(sut.CurrentUser.EmailAddress, _expectedCurrentUser.EmailAddress);
        Assert.AreEqual(sut.CurrentUser.Idp, _expectedCurrentUser.Idp);
        Assert.AreEqual(sut.CurrentUser.IdpSubject, _expectedCurrentUser.IdpSubject);
    }
}