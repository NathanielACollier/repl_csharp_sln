using System;
using Microsoft.EntityFrameworkCore;

namespace aspnetCore_MicrosoftLoginTest.appDB;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }


}
