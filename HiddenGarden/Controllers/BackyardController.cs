using Microsoft.AspNetCore.Mvc;
using HiddenGarden.Models;
using Newtonsoft.Json.Linq;

namespace HiddenGarden.Controllers;

public class BackyardsController : Controller
{
  public async Task<IActionResult> Index(int page = 1, int pageSize = 6)
  {
    Backyard backyard = new Backyard();
    List<Backyard> backyardList = new List<Backyard> { };
    using (var httpClient = new HttpClient())
    {
      using (var response = await httpClient.GetAsync($"https://localhost:5001/api/Backyards?page={page}"))
      {
        var backyardContent = await response.Content.ReadAsStringAsync();
        JArray backyardArray = JArray.Parse(backyardContent);
        backyardList = backyardArray.ToObject<List<Backyard>>();
      }
    }

    ViewBag.TotalPages = backyardList.Count();
    ViewBag.CurrentPage = page;
    ViewBag.PageSize = pageSize;

    return View(backyardList);
  }

  public IActionResult Details(int id)
  {
    Backyard backyard = Backyard.GetDetails(id);
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

  public ActionResult Edit(int id)
  {
    Backyard backyard = Backyard.GetDetails(id);
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