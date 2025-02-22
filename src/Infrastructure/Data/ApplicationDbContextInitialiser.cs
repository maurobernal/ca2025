﻿using ca.Domain.Constants;
using ca.Domain.Entities;
using ca.Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ca.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var administratorRole = new IdentityRole(Roles.Administrator);

        if (await _roleManager.Roles.AllAsync(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        // Default users
        var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

        if (await _userManager.Users.AllAsync(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Administrator1!");
            if (!string.IsNullOrWhiteSpace(administratorRole.Name))
            {
                await _userManager.AddToRolesAsync(administrator, new [] { administratorRole.Name });
            }
        }

        // Default data
        // Seed, if necessary
        if (!await _context.TodoLists.AnyAsync())
        {
            _context.TodoLists.Add(new TodoList
            {
                Title = "Todo List",
                Items =
                {
                    new TodoItem { Title = "Make a todo list 📃" },
                    new TodoItem { Title = "Check off the first item ✅" },
                    new TodoItem { Title = "Realise you've already done two things on the list! 🤯"},
                    new TodoItem { Title = "Reward yourself with a nice, long nap 🏆" },
                }
            });

            await _context.SaveChangesAsync();
        }
        if(!await _context.Countrys.AnyAsync())
        {
            _context.Countrys.Add(new Country { Id = 1, Name = "United States", Created=DateTime.UtcNow, Modified = DateTime.UtcNow });
            _context.Countrys.Add(new Country { Id = 2, Name = "Argentina", Created = DateTime.UtcNow, Modified = DateTime.UtcNow });
            await _context.SaveChangesAsync();
        }

        if (!await _context.Hobbies.AnyAsync())
        {
            _context.Hobbies.Add(new Hobbie { Id = 1, Name = "Futbol", Created = DateTime.UtcNow, Modified = DateTime.UtcNow });
            _context.Hobbies.Add(new Hobbie { Id = 2, Name = "Programacion", Created = DateTime.UtcNow, Modified = DateTime.UtcNow });
            await _context.SaveChangesAsync();
        }

        if (!await _context.Peoples.AnyAsync())
        {
            var hobbie = await _context.Hobbies.FirstOrDefaultAsync();
            var listHobbie = new List<Hobbie>();    
           listHobbie.Add(hobbie!);

            _context.Peoples.Add(new People { Id = 1, Name = "Juan", CountryId=1, Hobbies = listHobbie, Created = DateTime.UtcNow, Modified = DateTime.UtcNow });
            _context.Peoples.Add(new People { Id = 2, Name = "Diego", CountryId = 1, Hobbies = listHobbie, Created = DateTime.UtcNow, Modified = DateTime.UtcNow });
            _context.Peoples.Add(new People { Id = 3, Name = "Ana", CountryId = 2, Hobbies = listHobbie, Created = DateTime.UtcNow, Modified = DateTime.UtcNow });
            _context.Peoples.Add(new People { Id = 4, Name = "Laura", CountryId = 2, Hobbies = listHobbie, Created = DateTime.UtcNow, Modified = DateTime.UtcNow });
            await _context.SaveChangesAsync();
        }

    }
}
