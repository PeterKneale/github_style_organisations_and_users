namespace Demo.Core.Domain.Organisations;

public class OrganisationNameChangedEvent : BaseEvent
{
    public Organisation Organisation { get; }

    public OrganisationNameChangedEvent(Organisation Organisation)
    {
        Organisation = Organisation;
    }
}