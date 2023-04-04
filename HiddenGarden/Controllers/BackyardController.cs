using Microsoft.AspNetCore.Mvc;
using HiddenGarden.Models;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace HiddenGarden.Controllers;

[Authorize]
public class BackyardsController : Controller
{
  private readonly HiddenGardenContext _db;
  private readonly UserManager<ApplicationUser> _userManager;
  public BackyardsController(UserManager<ApplicationUser> userManager, HiddenGardenContext db)
  {
    _userManager = userManager;
    _db = db;
  }

  // private readonly ILogger<HomeController> _logger;
  public async Task<IActionResult> Index( int page = 1, int pageSize = 6)
  {
    Backyard backyard = new Backyard();
    List<Backyard> backyardList = new List<Backyard> { };
    using (var httpClient = new HttpClient())
    {
      using (var response = await httpClient.GetAsync($"https://localhost:7225/api/Backyards?page={page}&pageSize={pageSize}"))
      {
        string apiResponse = await response.Content.ReadAsStringAsync();
        JObject jsonResponse = JObject.Parse(apiResponse);
        JArray backyardArray = (JArray)jsonResponse["data"];
        backyardList = backyardArray.ToObject<List<Backyard>>();
      }
    }

    if(backyardList.Count == 0)
    {
      int returnPage = page -1;
      using (var httpClient = new HttpClient())
      {
      using (var response = await httpClient.GetAsync($"https://localhost:7225/api/Backyards?page={returnPage}&pageSize={pageSize}"))
        {
          string apiResponse = await response.Content.ReadAsStringAsync();
          JObject jsonResponse = JObject.Parse(apiResponse);
          JArray backyardArray = (JArray)jsonResponse["data"];
          backyardList = backyardArray.ToObject<List<Backyard>>();
        }
      }
      ViewBag.IsEnd = 1;
    }

    List<Backyard> totalBackyards = new List<Backyard> { };
    using (var httpClient = new HttpClient())
    {
      using (var response = await httpClient.GetAsync($"https://localhost:7225/api/Backyards?page={page}"))
      {
        string apiResponse = await response.Content.ReadAsStringAsync();
        JObject jsonResponse = JObject.Parse(apiResponse);
        JArray backyardArray = (JArray)jsonResponse["data"];
        totalBackyards = backyardArray.ToObject<List<Backyard>>();
      }
    }
    ViewBag.TotalPages = (totalBackyards.Count() / 6);
    ViewBag.CurrentPage = page;
    ViewBag.PageSize = pageSize;
    
    return View(backyardList);
  }

  public async Task<IActionResult> Details(int id)
  {
    List<Backyard> BackyardList = new List<Backyard> { };
    using (var httpClient = new HttpClient())
    {
      using (var response = await httpClient.GetAsync($"https://localhost:7225/api/Backyards?id={id}"))
      {
        string apiResponse = await response.Content.ReadAsStringAsync();
        JObject jsonResponse = JObject.Parse(apiResponse);
        JArray backyardArray = (JArray)jsonResponse["data"];
        BackyardList = backyardArray.ToObject<List<Backyard>>();
      }
    }
    Backyard backyard = BackyardList[0];
    return View(backyard);
  }

  public ActionResult Create()
  {
    return View();
  }

  [HttpPost]
  public async Task<IActionResult> Create(Backyard backyard)
  {
    string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
    backyard.UserId = userId;
    Backyard.Post(backyard);
    return RedirectToAction("Index");
  }

  public async Task<IActionResult> Edit(int id)
  {
    List<Backyard> BackyardList = new List<Backyard> { };
    using (var httpClient = new HttpClient())
    {
      using (var response = await httpClient.GetAsync($"https://localhost:7225/api/Backyards?id={id}"))
      {
        string apiResponse = await response.Content.ReadAsStringAsync();
        JObject jsonResponse = JObject.Parse(apiResponse);
        JArray backyardArray = (JArray)jsonResponse["data"];
        BackyardList = backyardArray.ToObject<List<Backyard>>();
      }
    }
    Backyard backyard = BackyardList[0];
    return View(backyard);
  }

  [HttpPost]
  public ActionResult Edit(Backyard backyard)
  {
    Backyard.Put(backyard);
    return RedirectToAction("Details", new { id = backyard.BackyardId });
  }

  public ActionResult Delete(int id)
  {
    Backyard backyard = Backyard.GetDetails(id);
    return View(backyard);
  }

  [HttpPost, ActionName("Delete")]
  public ActionResult DeleteConfirmed(int id)
  {
    Backyard.Delete(id);
    return RedirectToAction("Index");
  }


  [HttpPost, ActionName("Search")]
  public async Task<IActionResult> Search(string name)
  {
    if(name == null)
    {
      return RedirectToAction("Index");
    }
    List<Backyard> BackyardList = new List<Backyard> { };
    using (var httpClient = new HttpClient())
    {
      using (var response = await httpClient.GetAsync($"https://localhost:7225/api/Backyards?pageSize=1001"))
      {
        string apiResponse = await response.Content.ReadAsStringAsync();
        JObject jsonResponse = JObject.Parse(apiResponse);
        JArray backyardArray = (JArray)jsonResponse["data"];
        BackyardList = backyardArray.ToObject<List<Backyard>>();
      }
    }
    List<Backyard> result = new List<Backyard> { };
    foreach(Backyard backyard in BackyardList)
    {
      if (backyard.Description.ToLower().Contains(name.ToLower()))
      {
        result.Add(backyard);
      }
    }
    ViewBag.SearchResults = name;
    return View(result);
  }
}

