using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Domain.Entities
{
    public class GitHubProfile : Entity
    {
        public int UserId { get; set; }
        public string GitHubUrl { get; set; }

        public virtual User? User { get; set; }

        public GitHubProfile()
        {

        }

        public GitHubProfile(int id, int userId, string gitHubUrl) : this()
        {
            Id = id;
            UserId = userId;
            GitHubUrl = gitHubUrl;
        }


    }
}
