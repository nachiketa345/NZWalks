﻿using Microsoft.AspNetCore.Identity;

namespace NZWalks.Api.Repositories
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
