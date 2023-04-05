using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using HiddenGarden.Models;
using System.Threading.Tasks;
using HiddenGarden.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace HiddenGarden.Controllers
{
  public class AccountController : Controller
  {
    private readonly HiddenGardenContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, HiddenGardenContext db)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _db = db;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    { 
      List<Backyard> userBackyards = new List<Backyard> { };
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      using (var httpClient = new HttpClient())
      {
        using (var response = await httpClient.GetAsync($"https://localhost:7225/api/Backyards?userId={userId}"))
        {
          string apiResponse = await response.Content.ReadAsStringAsync();
          JObject jsonResponse = JObject.Parse(apiResponse);
          JArray backyardArray = (JArray)jsonResponse["data"];
          userBackyards = backyardArray.ToObject<List<Backyard>>();
        }
      }
      return View(userBackyards);
    }

    public IActionResult Register()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Register (RegisterViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return View(model);
      }
      else
      {
        ApplicationUser user = new ApplicationUser { UserName = model.Email };
        IdentityResult result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
          return RedirectToAction("Index");
        }
        else
        {
          foreach (IdentityError error in result.Errors)
          {
            ModelState.AddModelError("", error.Description);
          }
          return View(model);
        }
      }
    }

    public ActionResult Login()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Login(LoginViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return View(model);
      }
      else
      {
        Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
        if (result.Succeeded)
        {
          return RedirectToAction("Index");
        }
        else
        {
          ModelState.AddModelError("", "There is something wrong with your email or username. Please try again.");
          return View(model);
        }
      }
    }

    [HttpPost]
    public async Task<ActionResult> LogOff()
    {
      await _signInManager.SignOutAsync();
      return RedirectToAction("Index");
    }
  }
}

// using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Mvc;
// using HiddenGarden.Models;
// using System.Collections.Generic;
// using System.Linq;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Identity;
// using System.Threading.Tasks;
// using System.Security.Claims;
// using HiddenGarden.ViewModels;

// namespace HiddenGarden.Controllers
// {
//   public class AccountController : Controller
//   {
//     private readonly HiddenGardenContext _db;
//     private readonly UserManager<ApplicationUser> _userManager;
//     private readonly SignInManager<ApplicationUser> _signInManager;

//     public AccountController (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, HiddenGardenContext db)
//     {
//       _userManager = userManager;
//       _signInManager = signInManager;
//       _db = db;
//     }

//     public ActionResult Index()
//     {
//       return View();
//     }

//     public IActionResult Register()
//     {
//       return View();
//     }

//     [HttpPost]
//     public async Task<ActionResult> Register (RegisterViewModel model)
//     {
//       if (!ModelState.IsValid)
//       {
//         return RedirectToAction("Index", "Account");
//       }
//       else
//       {
//         ApplicationUser user = new ApplicationUser { UserName = model.Email };
//         IdentityResult result = await _userManager.CreateAsync(user, model.Password);
//         if (result.Succeeded)
//         {
//           Microsoft.AspNetCore.Identity.SignInResult loginResult = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
//           if (loginResult.Succeeded)
//           {
//             return RedirectToAction("Index", "Home");
//           }
//           else
//           {
//             return RedirectToAction("Index", "Account");
//           }
//         }
//         else
//         {
//           foreach (IdentityError error in result.Errors)
//           {
//             ModelState.AddModelError("", error.Description);
//           }
//           return View();
//         }
//       }
//     }

//     public ActionResult Login()
//     {
//       return View();
//     }

//     [HttpPost]
//     public async Task<ActionResult> Login(LoginViewModel model)
//     {
//       if (!ModelState.IsValid)
//       {
//         return RedirectToAction("Index", "Account");
//       }
//       else
//       {
//         Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
//         if (result.Succeeded)
//         {
//           return RedirectToAction("Index", "Home");
//         }
//         else
//         {
//           ModelState.AddModelError("", "There is something wrong with your email or username. Please try again.");
//           return RedirectToAction("Index", "Account");
//         }
//       }
//     }

//     [HttpPost]
//     public async Task<ActionResult> LogOff()
//     {
//       await _signInManager.SignOutAsync();
//       return RedirectToAction("Index");
//     }
//   }
// }