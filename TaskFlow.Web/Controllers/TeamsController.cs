using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Services.Interfaces;
using TaskFlow.Web.ViewModels.Teams;

namespace TaskFlow.Web.Controllers;

[Authorize]
public class TeamsController(ITeamService teamService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Challenge();
        }

        TeamIndexViewModel viewModel = await teamService
            .GetMyTeamsAsync(userId, cancellationToken);

        return View(viewModel);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new TeamCreateInputModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        TeamCreateInputModel inputModel,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(inputModel);
        }

        string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Challenge();
        }

        await teamService.CreateTeamAsync(inputModel, userId, cancellationToken);

        return RedirectToAction(nameof(Index));
    }
}
