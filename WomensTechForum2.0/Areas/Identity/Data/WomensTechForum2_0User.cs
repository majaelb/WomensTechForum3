using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WomensTechForum2._0.Areas.Identity.Data;

// Add profile data for application users by adding properties to the WomensTechForum2_0User class
public class WomensTechForum2_0User : IdentityUser
{
    [PersonalData]
    public string? FirstName { get; set; }
    [PersonalData]
    public string? LastName { get; set; }
    [PersonalData]
    public string? Gender { get; set; }
    [PersonalData]
    public string? ImageSrc { get; set; }
}

