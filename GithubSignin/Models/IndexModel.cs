using Microsoft.AspNetCore.Mvc.RazorPages;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GithubSignin.Models
{
    public class IndexModel 
    {
        public string GitHubAvatar { get; set; }

        public string GitHubLogin { get; set; }

        public string GitHubName { get; set; }

        public string GitHubUrl { get; set; }

        public IReadOnlyList<Repository> Repositories { get; set; }

       
    }
}
