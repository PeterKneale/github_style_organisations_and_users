using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Demo.Pages.Organisations;

public class Create : PageModel
{
    private readonly IMediator _mediator;

    public Create(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var organisationId = Guid.NewGuid();
        await _mediator.Send(new Core.Application.Organisations.Commands.Create.Command(organisationId, Title, Description));

        return RedirectToPage("View", new {Id = organisationId});
    }

    [Display(Name = "Title")]
    [Required]
    [BindProperty]
    public string Title { get; set; }

    [Display(Name = "Description")]
    [BindProperty]
    public string? Description { get; set; }
}