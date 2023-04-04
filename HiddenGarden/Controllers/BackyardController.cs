using Microsoft.AspNetCore.Mvc;
using HiddenGarden.Models;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;

namespace HiddenGarden.Controllers;

[Authorize]
public class BackyardsController : Controller
{
  // private readonly ILogger<HomeController> _logger;
  public async Task<IActionResult> Index(int page = 1, int pageSize = 6)
  {
    Backyard backyard = new Backyard();
    List<Backyard> backyardList = new List<Backyard> { };
    using (var httpClient = new HttpClient())
    {
      using (var response = await httpClient.GetAsync($"https://localhost:7225/api/Backyards?page={page}"))
      {
        string apiResponse = await response.Content.ReadAsStringAsync();
        JObject jsonResponse = JObject.Parse(apiResponse);
        JArray backyardArray = (JArray)jsonResponse["data"];
        backyardList = backyardArray.ToObject<List<Backyard>>();
      }
    }

    ViewBag.TotalPages = backyardList.Count();
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
  public ActionResult Create(Backyard backyard)
  {
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
}

// https://www.google.com/maps/place/13704+SE+Salmon+St,+Portland,+OR+97233/