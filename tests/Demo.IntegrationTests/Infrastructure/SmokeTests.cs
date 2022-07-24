using Demo.Core.Application.Contracts;
using Demo.Core.Domain.Organisations;
using Demo.Core.Domain.Users;
using Demo.Core.Infrastructure.Persistence;

namespace Demo.IntegrationTests.Infrastructure;

[Collection(nameof(DatabaseCollection))]
public class SmokeTests : IClassFixture<ServicesFixture>
{
    private readonly IServiceProvider _services;

    public SmokeTests(ServicesFixture services, ITestOutputHelper outputHelper)
    {
        services.OutputHelper = outputHelper;
        _services = services.ServiceProvider;
    }

    [Fact]
    public async Task Can_execute_basic_use_cases()
    {
        // arrange
        await using var scope1 = _services.CreateAsyncScope();
        var organisations = scope1.ServiceProvider.GetRequiredService<IOrganisationRepository>();
        var users = scope1.ServiceProvider.GetRequiredService<IUserRepository>();
        var db = scope1.ServiceProvider.GetRequiredService<DatabaseContext>();
        
        var organisationId = OrganisationId.CreateInstance(Guid.NewGuid());
        var organisationName = new OrganisationTitle("Timmmy One", "Timmy One Pty Ltd");
        var userId = UserId.CreateInstance(Guid.NewGuid());
        var userName = new UserName("tim", "smith");
        var email = "tim@example.com";
        var password = "password";

        // act
        var user = User.CreateInstance(userId, userName, email, password);
        await users.Add(user);
        await db.SaveChangesAsync();
        
        var organisation = Organisation.CreateInstance(organisationId, organisationName, userId);
        await organisations.Add(organisation);
        await db.SaveChangesAsync();

        // assert
        FakeCurrentContext.SetOrganisationId(organisationId);
        await using var scope2 = _services.CreateAsyncScope();
        var readOrganisations = scope2.ServiceProvider.GetRequiredService<IOrganisationRepository>();
        var readUsers = scope2.ServiceProvider.GetRequiredService<IUserRepository>();
        var organisation2 = await readOrganisations.Get(organisationId);
        organisation2.Should().BeEquivalentTo(organisation);
        var user2 = await readUsers.Get(userId);
        user2.Should().BeEquivalentTo(user);
        var list = await readUsers.List();
        list.Should().ContainEquivalentOf(user);
    }
}