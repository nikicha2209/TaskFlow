using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Services.Interfaces;
using TaskFlow.Web.ViewModels.Projects;

namespace TaskFlow.Web.Controllers;

[Authorize]
[Route("teams/{teamId:int}/projects")]
public class ProjectsController(IProjectService projectService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(int teamId, CancellationToken cancellationToken)
    {
        string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Challenge();
        }

        ProjectIndexViewModel? viewModel = await projectService
            .GetTeamProjectsAsync(teamId, userId, cancellationToken);

        return viewModel is null ? NotFound() : View(viewModel);
    }

    [HttpGet("create")]
    public async Task<IActionResult> Create(int teamId, CancellationToken cancellationToken)
    {
        string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Challenge();
        }

        if (!await projectService.CanManageProjectsAsync(teamId, userId, cancellationToken))
        {
            return NotFound();
        }

        ViewData["TeamId"] = teamId;
        return View(new ProjectCreateInputModel());
    }

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        int teamId,
        ProjectCreateInputModel inputModel,
        CancellationToken cancellationToken)
    {
        string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Challenge();
        }

        if (!await projectService.CanManageProjectsAsync(teamId, userId, cancellationToken))
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            ViewData["TeamId"] = teamId;
            return View(inputModel);
        }

        int? projectId = await projectService.CreateProjectAsync(
            teamId, userId, inputModel, cancellationToken);

        if (projectId is null)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index), new { teamId });
    }
}
