using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GithubSignin.Models;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Octokit;
using Octokit.Internal;

namespace GithubSignin.Controllers
{
    
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            IndexModel b = OnGet().Result;
            return View("Index",b);
        }

        public async Task<IndexModel> OnGet()
        {
            IndexModel a = new IndexModel();
            if (User.Identity.IsAuthenticated)
            {
                a.GitHubName = User.FindFirst(c => c.Type == ClaimTypes.Name)?.Value;
                a.GitHubLogin = User.FindFirst(c => c.Type == "urn:github:login")?.Value;
                a.GitHubUrl = User.FindFirst(c => c.Type == "urn:github:url")?.Value;
                a.GitHubAvatar = User.FindFirst(c => c.Type == "urn:github:avatar")?.Value;

                string accessToken = await HttpContext.GetTokenAsync("access_token");

                var github = new GitHubClient(new ProductHeaderValue("AspNetCoreGitHubAuth"),
                    new InMemoryCredentialStore(new Credentials(accessToken)));
                a.Repositories = await github.Repository.GetAllForCurrent();

            }
            return a;
        }
        //public IActionResult About()
        //{
        //    ViewData["Message"] = "Your application description page.";

        //    return View();
        //}

        //public IActionResult Contact()
        //{
        //    ViewData["Message"] = "Your contact page.";

        //    return View();
        //}

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
