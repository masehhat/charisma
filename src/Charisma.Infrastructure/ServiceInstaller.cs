using Charisma.Application.Common.Interfaces;
using Charisma.Domain;
using Charisma.Infrastructure.Persistance.CharismaDb;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charisma.Infrastructure;

public static class ServiceInstaller
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddScoped<ICharismaDbContext, CharismaDbContext>();
        services.AddMemoryCache();

        services.AddDbContext<CharismaDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("CharismaDbConnection"))
            .LogTo(message => Debug.WriteLine(message), Microsoft.Extensions.Logging.LogLevel.Information);
            options.EnableSensitiveDataLogging(true);
        });

        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<CharismaDbContext>();

        return services;

	}
}
