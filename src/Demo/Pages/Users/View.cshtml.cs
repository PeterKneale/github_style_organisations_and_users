using System.ComponentModel.DataAnnotations;
using Demo.Core.Application.Organisations.Queries;
using Demo.Core.Application.Users.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Demo.Pages.Users;

public class View : PageModel
{
    private readonly IMediator _mediator;

    public View(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        User = await _mediator.Send(new GetUser.Query(id));
        Organisations = await _mediator.Send(new ListOrganisationsByMember.Query(id));
        return Page();
    }
    
    public GetUser.Result User { get; set; }
    public IEnumerable<ListOrganisationsByMember.Result> Organisations { get; set; }
}