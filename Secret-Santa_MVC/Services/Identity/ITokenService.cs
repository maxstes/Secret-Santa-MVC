﻿using Microsoft.AspNetCore.Identity;
using Secret_Santa_MVC.Data.Entities;

namespace Secret_Santa_MVC.Services.Identity
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user, List<IdentityRole<long>> role);
    }
}
