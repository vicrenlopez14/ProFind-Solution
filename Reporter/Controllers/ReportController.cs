using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using Microsoft.EntityFrameworkCore;
using Reporter.Data;
using RotativaCore;

namespace Reporter.Controllers;

public class ReportController : Controller
{
    private readonly ProFindContext _context;

    public ReportController(ProFindContext context)
    {
        _context = context;
    }


    // 
    // GET: /HelloWorld/
    public IActionResult Index()
    {
        return View();
    }

    // 
    // GET: /HelloWorld/Welcome/ 
    public string Welcome(string name, int ID = 1)
    {
        return HtmlEncoder.Default.Encode($"Hello {name}, ID: {ID}");
    }

    #region RegisteredAdmins

    public async Task<IActionResult> RegisteredAdmins()
    {
        ViewData["admins"] = await _context.Admins.Include(x => x.IdR1Navigation).ToListAsync();
        return View();
    }

    #endregion

    public async Task<IActionResult> RegisteredProfessionals()
    {
        ViewData["professionals"] = await _context.Professionals.Include(x => x.IdDp1Navigation)
            .Include(x => x.IdPfs1Navigation).ToListAsync();
        return View();
    }


    #region RegisteredClients

    public async Task<IActionResult> RegisteredClients()
    {
        ViewData["clients"] = await _context.Clients.Include(x=> x.Projects).ToListAsync();
        return View();
    }

    #endregion

    #region CreatedProjects

    public async Task<IActionResult> CreatedProjects()
    {
        var projects = await _context.Projects.Include(x => x.IdC1Navigation).Include(x => x.IdP1Navigation)
            .ToListAsync();

        ViewData["projects"] = projects;
        ViewData["totalprice"] = projects.Sum(p => p.TotalPricePj);

        return View();
    }

    #endregion CreatedProjects

    #region ProjectDetail

    // Project
    public async Task<IActionResult> ProjectDetail(string id)
    {
        ViewBag.id = "";

        var selectedProject = await _context.Projects.Include(x => x.IdC1Navigation).Include(x => x.IdP1Navigation)
            .FirstOrDefaultAsync(p => p.IdPj == id);
        ViewData["project"] = selectedProject;

        return View();
    }

    #endregion

    public async Task<ActionResult> AdminDetail(string id)
    {
        var selectedAdmin = await _context.Admins.Include(x => x.IdR1Navigation).FirstOrDefaultAsync(p => p.IdA == id);
        ViewData["admin"] = selectedAdmin;

        return View();
    }

    public async Task<ActionResult> ClientDetail(string id)
    {
        var selectedClient = await _context.Clients.Include(x => x.Projects).FirstOrDefaultAsync(p => p.IdC == id);
        ViewData["client"] = selectedClient;

        return View();
    }

    public async Task<ActionResult> ProfessionalDetail(string id)
    {
        var selectedProfessional = await _context.Professionals.Include(x => x.Projects)
            .Include(x => x.IdPfs1Navigation).Include(x => x.IdDp1Navigation).FirstOrDefaultAsync(p => p.IdP == id);
        ViewData["professional"] = selectedProfessional;

        return View();
    }
}