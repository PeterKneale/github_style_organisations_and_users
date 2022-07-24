using Demo.Core.Application.Organisations.Commands;
using Demo.Core.Application.Users.Commands;
using Demo.Core.Application.Users.Queries;
using FluentValidation;

namespace Demo.IntegrationTests.Application;

[Collection(nameof(DatabaseCollection))]
public class SmokeTests : IClassFixture<ServicesFixture>
{
    private readonly ServicesFixture _services;

    public SmokeTests(ServicesFixture services, ITestOutputHelper outputHelper)
    {
        services.OutputHelper = outputHelper;
        _services = services;
    }

    [Fact]
    public async Task Can_execute_basic_use_cases()
    {
        // arrange
        var userId = Guid.NewGuid();
        var firstName = "bill";
        var lastName = "bloggs";
        var organisationId = Guid.NewGuid();
        var title = "Billy Enterprise";
        var description = "A brilliant company";
        var email = "bill@example.com";
        var password = "password";

        // act
        await _services.Send(new Register.Command(userId, firstName, lastName, email, password));
        FakeCurrentContext.SetUserId(userId);
        await _services.Send(new Create.Command(organisationId, title, description));

        // assert
        FakeCurrentContext.SetOrganisationId(organisationId);
        var authResult = await _services.Send(new Authenticate.Command(email, password));
        authResult.Should().NotBeNull();
        authResult.Success.Should().BeTrue();
        authResult.User.Should().NotBeNull();
        authResult.User.Id.Value.Should().Be(userId);
        
        var getUserResult = await _services.Send(new GetUser.Query(userId));
        getUserResult.Id.Should().Be(userId);
        getUserResult.FirstName.Should().Be(firstName);
        getUserResult.LastName.Should().Be(lastName);

        var listUsersResult = await _services.Send(new ListUsers.Query());
        listUsersResult.Should().NotBeEmpty();
    }

    [Fact]
    public async Task InvalidRequestsThrow()
    {
        // arrange

        // act
        Func<Task> act = async () => {
            await _services.Send(new Register.Command(Guid.Empty, "", "", "", ""));
        };

        // assert
        await act.Should().ThrowAsync<ValidationException>();
    }
}