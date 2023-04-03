// using Microsoft.AspNetCore.Mvc;
// using ShelterClient.Models;
// using Newtonsoft.Json.Linq;

// namespace ShelterClient.Controllers;

// public class AnimalsController : Controller
// {
//   public async Task<IActionResult> Index(int page = 1, int pageSize = 6)
//   {
//     Animal animal = new Animal();
//     List<Animal> animalList = new List<Animal> { };
//     using (var httpClient = new HttpClient())
//     {
//       using (var response = await httpClient.GetAsync($"https://localhost:5001/api/Animals?page={page}"))
//       {
//         var animalContent = await response.Content.ReadAsStringAsync();
//         JArray animalArray = JArray.Parse(animalContent);
//         animalList = animalArray.ToObject<List<Animal>>();
//       }
//     }

//     ViewBag.TotalPages = animalList.Count();
//     ViewBag.CurrentPage = page;
//     ViewBag.PageSize = pageSize;

//     return View(animalList);
//   }

//   public IActionResult Details(int id)
//   {
//     Animal animal = Animal.GetDetails(id);
//     return View(animal);
//   }

//   public ActionResult Create()
//   {
//     return View();
//   }

//   [HttpPost]
//   public ActionResult Create(Animal animal)
//   {
//     Animal.Post(animal);
//     return RedirectToAction("Index");
//   }

//   public ActionResult Edit(int id)
//   {
//     Animal animal = Animal.GetDetails(id);
//     return View(animal);
//   }

//   [HttpPost]
//   public ActionResult Edit(Animal animal)
//   {
//     Animal.Put(animal);
//     return RedirectToAction("Details", new { id = animal.AnimalId });
//   }

//   public ActionResult Delete(int id)
//   {
//     Animal animal = Animal.GetDetails(id);
//     return View(animal);
//   }

//   [HttpPost, ActionName("Delete")]
//   public ActionResult DeleteConfirmed(int id)
//   {
//     Animal.Delete(id);
//     return RedirectToAction("Index");
//   }
// }