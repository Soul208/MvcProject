using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Moodels.IdentityModel
{
    internal class ApplicatonUser : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
    }
}
